using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_m3u_playlist_manager {

    internal class Scanner {

        public static void Scan(string dir, string[] fileTypes) {
            var musicFiles = Directory.EnumerateFiles(dir, "*.*").Where(x => fileTypes.Contains(Path.GetExtension(x).ToLower().Substring(1)));

            foreach (string d in Directory.GetDirectories(dir)) {
                Scan(d, fileTypes);
            }
        }
    }
}