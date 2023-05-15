using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_m3u_playlist_manager {

    internal class Logger {
        private static string logFileName = $"{System.Diagnostics.Process.GetCurrentProcess().ProcessName}.log";

        public static void StartLoging(string dir, string filter, bool overwrite) {
            var logFile = Path.Combine(Directory.GetCurrentDirectory(), logFileName);
            File.WriteAllLines(logFile, new string[] { $"Starting playlist creation in '{dir}'", $"Looking for {filter}", $"Overwriting old playlists: {overwrite}" });
        }

        public static void Log(string message) {
            try {
                var logFile = Path.Combine(Directory.GetCurrentDirectory(), logFileName);
                using (StreamWriter sw = File.AppendText(logFile)) {
                    sw.WriteLine(message);
                }
            } catch (Exception ex) {
                //Console.WriteLine($"Error writing to log file: {ex.Message}: {message}");
            }
        }
    }
}