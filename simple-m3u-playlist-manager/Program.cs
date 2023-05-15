using simple_m3u_playlist_manager;
using System.IO;

try {
    Console.WriteLine("This program will create m3u files in the selected directory and subdirectories containing all music files found.");
    var currentDir = Directory.GetCurrentDirectory();
    Console.WriteLine($"Starting directory [{currentDir}]:");
    var dir = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(dir)) {
        dir = currentDir;
    }
    var removeOld = "";
    while (removeOld != "y" && removeOld != "n") {
        Console.WriteLine($"Remove existing m3u files (Y/N) [Y]:");
        removeOld = Console.ReadLine().ToLower();
        if (string.IsNullOrWhiteSpace(removeOld)) {
            removeOld = "y";
        }
    }
    var overWriteM3u = removeOld.Equals("y");
    var basefileFilter = "mp3,flac,wav,wma,aac,ogg";
    Console.WriteLine($"Files extentions to add, comma seperated [{basefileFilter}]:");
    string fileFilter = Console.ReadLine().ToLower();
    if (string.IsNullOrWhiteSpace(fileFilter)) {
        fileFilter = basefileFilter;
    }
    string[] fileTypes = fileFilter.Split(',');

    var testRun = "";
    while (testRun != "y" && testRun != "n") {
        Console.WriteLine($"Log only (Don't write m3u files) (Y/N) [N]:");
        testRun = Console.ReadLine().ToLower();
        if (string.IsNullOrWhiteSpace(testRun)) {
            testRun = "n";
        }
    }
    Scanner.Instance.TestRun = testRun.Equals("y");

    if (Directory.Exists(dir)) {
        Console.Write("\rScanning...");
        Logger.StartLoging(dir, fileFilter, overWriteM3u);
        await Scanner.Instance.Scan(dir, fileTypes, overWriteM3u);
        Console.WriteLine($"\rrScanning...Done");
    } else {
        Console.WriteLine($"'{dir}' is not a valid directory");
        return 1;
    }
    Console.WriteLine($"Press any key to exit");
    Console.ReadKey();
    return 0;
} catch (Exception ex) {
    Console.WriteLine($"It seems like something went wrong :(");
    Console.WriteLine($"Perhaps this can help:");
    Console.WriteLine(ex.Message);
    return 1;
}