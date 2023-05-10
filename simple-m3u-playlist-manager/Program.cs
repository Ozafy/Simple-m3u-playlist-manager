using simple_m3u_playlist_manager;
using System.IO;

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
    if (string.IsNullOrWhiteSpace(dir)) {
        removeOld = "y";
    }
}
var basefileFilter = "mp3,flac,wav,wma,aac,ogg";
Console.WriteLine($"Files extentions to add, comma seperated [{basefileFilter}]:");
string fileFilter = Console.ReadLine().ToLower();
if (string.IsNullOrWhiteSpace(fileFilter)) {
    fileFilter = basefileFilter;
}
string[] fileTypes = fileFilter.Split(',');
if (Directory.Exists(dir)) {
    Scanner.Scan(dir, fileTypes);
    // scan directories for music and m3u files
    // create new m3u files with directory name as playlist name in each directory (incl top)
} else {
    Console.WriteLine($"'{dir}' is not a valid directory");
}
Console.WriteLine($"Press any key to exit");
var tmp = Console.ReadLine();