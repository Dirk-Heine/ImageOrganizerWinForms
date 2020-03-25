using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ImageOrganizerWinForms.Common;

namespace ImageOrganizerWinForms.Model
{
    class ModelLogFile
    {
        #region Properties
        
        public string FilePath;
        string Project;
        StreamWriter FileStream;
        //public static string Separator { get; } = ".....................................................................................";
        
        #endregion


        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelLogFile(string path, string projectName = "")
        {
            Project = projectName;
            FilePath = path;
            SetFile();
        }
        #endregion


        #region Methods

        /// <summary>
        /// Creates Logfile if File does not exist
        /// </summary>
        private void SetFile()
        {
            if (!File.Exists(FilePath))
            {
                FileStream fs = File.Create(FilePath);
                fs.Dispose();
            }
            else if (!string.IsNullOrEmpty(Project))
            {
                File.WriteAllText(FilePath, String.Empty);
            }

            if (!string.IsNullOrEmpty(Project))
            {
                FileStream = new StreamWriter(FilePath);
                if (Project != "")
                {
                    FileStream.Write("\r\nLogFile Project: " + Project + "\r\n");
                }
                FileStream.Dispose();
            }
        }

        /// <summary>
        /// Saves Log entries to Logfile set in constructor
        /// </summary>
        public void AddLogList(List<LogEntry> Logs)
        {
            FileStream = File.AppendText(FilePath);
            foreach (LogEntry Log in Logs)
            {
                FileStream.WriteLine(Log2String(Log));
            }
            FileStream.Dispose();
        }
        public void AddLogEntry(LogEntry Log)
        {
            try
            {
                FileStream = File.AppendText(FilePath);
                FileStream.WriteLine(Log2String(Log));
                FileStream.Dispose();
            }
            catch //(Exception e)
            { }
        }
        public static string Log2String(LogEntry Log)
        {
            string level = "";
            if (Log.ImageSource == LogImage.Error)
            {
                level = "Error";
            }
            else if (Log.ImageSource == LogImage.Warning)
            {
                level = "Warning";
            }
            else //if (Log.ImageSource == Helpers.LogLevel.Info)
            {
                level = "Info";
            }
            string msg = "";
            msg += DateTime.Now.ToShortDateString();
            msg += " ";
            msg += DateTime.Now.ToLongTimeString();
            msg += " | ";
            msg += level;
            msg += " | ";
            msg += Log.Message;
            return msg;
        }

        #endregion
    }

    /// <summary>
    /// Used to stare data of Log messages
    /// </summary>
    class LogEntry
    {
        public string ImageSource { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }

        public LogEntry(string message, string imageSource = LogImage.Info)// ,string cTime)
        {
            ImageSource = imageSource;
            Message = message;
            Time = DateTime.Now.ToLongTimeString();
        }
        public LogEntry()
        {
            ImageSource = string.Empty;
            Message = string.Empty;
            Time = DateTime.Now.ToLongTimeString();
        }
    }

    public enum LogType
    {
        Info,
        Warning,
        Error
    }

    /// <summary>
    /// Conains paths of images shown in log messages
    /// </summary>
    static class LogImage
    {
        public const string Info = "/ImageOrganizer;component/Resources/Info.png";
        public const string Warning = "/ImageOrganizer;component/Resources/Warning.png";
        public const string Error = "/ImageOrganizer;component/Resources/Error.png";
    }
}
