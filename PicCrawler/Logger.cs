using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicCrawler
{
    public static class Logger
    {
        private static readonly object WriteLocker = new object();

        public static string LogDir { get; private set; }

        public static string LogPath { get; private set; }

        public static string ErrorLogPath { get; private set; }

        public static Encoding LogEncoding { get; private set; }

        public static void CreateLogFile()
        {
            if (!File.Exists(LogPath))
            {
                Sanity.Requires(Directory.Exists(LogDir), 
                    new DirectoryNotFoundException(string.Format(GlobalMessages.FILE_OR_DIRECTORY_NOT_EXISTS, "LogDir", LogDir)));
                LogPath = Path.Combine(LogDir, $"log_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.txt");
                File.Create(LogPath);
                WriteLine(string.Format(GlobalMessages.LOG_CREATED, LogPath));
            }
        }

        public static void CreateErrorLogFile()
        {
            CreateLogFile();
            if (!File.Exists(ErrorLogPath))
            {
                ErrorLogPath = Path.Combine(LogDir, $"{LogPath.Split('\\').Last().Replace("log", "log_error")}");
                File.Create(ErrorLogPath);
                WriteLine(string.Format(GlobalMessages.ERROR_LOG_CREATED, ErrorLogPath));
            }
        }

        public static void WriteLine(string msg)
        {
            File.WriteAllLines(LogPath, new string[] { $"{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}\t|{msg}" });
        }

        public static void SafeWriteLine(string msg)
        {
            lock (WriteLocker)
            {
                WriteLine(msg);
            }
        }

        public static void WriteError(string errMsg)
        {
            CreateErrorLogFile();
            WriteLine(errMsg);
            File.WriteAllLines(ErrorLogPath, new string[] { errMsg });
        }

        public static void SafeWriteError(string errMsg)
        {
            lock (WriteLocker)
            {
                WriteError(errMsg);
            }
        }
    }
}
