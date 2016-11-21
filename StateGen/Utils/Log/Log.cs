using System;
using System.Diagnostics;
using System.IO;

namespace StateGen.Utils.Logger
{
    class Log
    {
        private static readonly Lazy<Log> Lazy = new Lazy<Log>(() => new Log());

        public static Log Instance { get { return Lazy.Value; } }

        internal Log()
        {
            LogFileName = "Example";
            LogFileExtension = ".log";
        }
        
        public StreamWriter Writer { get; set; }

        public string LogPath
        {
            get { return _LogPath ?? (_LogPath = AppDomain.CurrentDomain.BaseDirectory); }
            set { _LogPath = value; }
        }
        private string _LogPath;

        private string LogFileName { get; set; }

        private string LogFileExtension { get; set; }

        private string LogFile { get { return LogFileName + LogFileExtension; } }

        private string LogFullPath { get { return Path.Combine(LogPath, LogFile); } }

        private bool LogExists { get { return File.Exists(LogFullPath); } }

        private void WriteLineToLog(string inLogMessage)
        {
            WriteToLog(inLogMessage + Environment.NewLine);
        }

        private void WriteToLog(string inLogMessage)
        {
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }
            if (Writer == null)
            {
                Writer = new StreamWriter(LogFullPath, true);
            }

            Writer.Write(inLogMessage);
            Writer.Flush();
        }

        private static void WriteLine(string inLogMessage)
        {
            Instance.WriteLineToLog(inLogMessage);
        }

        private static void Write(string inLogMessage)
        {
            Instance.WriteToLog(inLogMessage);
        }

        private static void WriteLevel(string level, string inLogMessage)
        {
            StackTrace st = new StackTrace();

            string classMethod = "";

            if (st.GetFrames().Length > 2)
            {
               classMethod += st.GetFrame(1).GetMethod().ReflectedType.ToString() + " ";
               classMethod += st.GetFrame(1).GetMethod().ToString() + " ";
            }
            Write(DateTime.Now.ToString("HH:mm::ss tt") + level + classMethod + inLogMessage + Environment.NewLine);
        }

        public static void Info(string inLogMessage)
        {
            WriteLevel(" [Info] ", inLogMessage);
        }

        public static void Error(string inLogMessage)
        {
            WriteLevel(" [Error] ", inLogMessage);
        }
    }
}
