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

        public static void ConfigureLogger(string logDir)
        {
            LogDir = logDir;
            Directory.CreateDirectory(LogDir);
            CreateLogFile();
        }

        private static void CreateLogFile()
        {
            if (!File.Exists(LogPath))
            {
                Sanity.Requires(Directory.Exists(LogDir), 
                    new DirectoryNotFoundException(string.Format(GlobalMessages.FILE_OR_DIRECTORY_NOT_EXISTS, "LogDir", LogDir)));
                LogPath = Path.Combine(LogDir, $"log_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.txt");
                //File.Create(LogPath);
                StreamWriter sw = new StreamWriter(File.Open(LogPath, FileMode.OpenOrCreate, FileAccess.Write), Encoding.UTF8);
                sw.Close();
                SafeWriteLine(string.Format(GlobalMessages.LOG_CREATED, LogPath));
            }
        }

        private static void CreateErrorLogFile()
        {
            CreateLogFile();
            if (!File.Exists(ErrorLogPath))
            {
                ErrorLogPath = Path.Combine(LogDir, $"{LogPath.Split('\\').Last().Replace("log", "log_error")}");
                StreamWriter sw = new StreamWriter(File.Open(ErrorLogPath, FileMode.OpenOrCreate, FileAccess.Write), Encoding.UTF8);
                sw.Close();
                SafeWriteLine(string.Format(GlobalMessages.ERROR_LOG_CREATED, ErrorLogPath));
            }
        }

        public static void WriteLine(string msg, params string[] args)
        {
            if (args.Length > 0)
            {
                msg = string.Format(msg, args);
            }
            msg = $"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")}\t|{msg}";
            Console.WriteLine(msg);
            File.AppendAllLines(LogPath, new string[] { msg });
        }

        public static void SafeWriteLine(string msg, params string[] args)
        {
            if (args.Length > 0)
            {
                msg = string.Format(msg, args);
            }
            lock (WriteLocker)
            {
                WriteLine(msg);
            }
        }

        public static void WriteError(string errMsg, params string[] args)
        {
            CreateErrorLogFile();
            if (args.Length > 0)
            {
                errMsg = string.Format(errMsg, args);
            }
            errMsg = $"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")}\t|{errMsg}";
            WriteLine(errMsg);
            File.AppendAllLines(ErrorLogPath, new string[] { errMsg });
        }

        public static void SafeWriteError(string errMsg, params string[] args)
        {
            if (args.Length > 0)
            {
                errMsg = string.Format(errMsg, args);
            }
            lock (WriteLocker)
            {
                WriteError(errMsg);
            }
        }
    }
}
