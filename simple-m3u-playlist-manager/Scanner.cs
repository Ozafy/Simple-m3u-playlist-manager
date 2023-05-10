using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_m3u_playlist_manager {

    public sealed class Scanner {
        private static readonly Lazy<Scanner> lazy = new Lazy<Scanner>(() => new Scanner());

        public static Scanner Instance { get { return lazy.Value; } }

        private Scanner() {
        }

        public bool TestRun = true;

        public async Task<(string DirName, List<string> MusicFiles)> Scan(string dir, string[] fileTypes, bool overWriteM3u) {
            Thread.Sleep(1000);
            var deeperMusicFiles = new List<string>();
            foreach (string d in Directory.GetDirectories(dir)) {
                var scan = await Scan(d, fileTypes, overWriteM3u);
                deeperMusicFiles.AddRange(scan.MusicFiles.Select(x => Path.Combine(scan.DirName, x)).ToList());
            }
            Logger.Log($"Looking in {dir}{Path.DirectorySeparatorChar}");
            var musicFiles = Directory.EnumerateFiles(dir, "*.*").Where(x => fileTypes.Contains(Path.GetExtension(x).ToLower().Substring(1))).Select(x => x.Replace($"{dir}{Path.DirectorySeparatorChar}", "")).ToList(); ;
            var dirName = new DirectoryInfo(dir).Name;
            var m3uFile = Path.Combine(dir, $"{dirName}.m3u");
            if (File.Exists(m3uFile) && !overWriteM3u) {
                if (!TestRun) {
                    File.Copy(m3uFile, $"{m3uFile}.bak", true);
                }
                Logger.Log($"Copied {m3uFile} to {m3uFile}.bak");
            }

            deeperMusicFiles.AddRange(musicFiles);

            if (deeperMusicFiles.Count > 0) {
                if (!TestRun) {
                    File.WriteAllLines(m3uFile, deeperMusicFiles);
                }
                Logger.Log($"Found:");
                Logger.Log(string.Join(Environment.NewLine, deeperMusicFiles));
                Logger.Log($"Written {m3uFile}");
            }

            return (dirName, deeperMusicFiles);
        }
    }
}