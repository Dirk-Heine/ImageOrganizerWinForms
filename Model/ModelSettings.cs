using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Security.Cryptography;
using ImageOrganizerWinForms.Properties;
using ImageOrganizerWinForms.Common;

namespace ImageOrganizerWinForms.Model
{
    /// <summary>
    /// stores all settings
    /// </summary>
     static class ModelSettings //: ModelBase
    {
        // constants
        public const string LOG_NAME = "LogFile.txt";
        public static string Version { get; /*set;*/ } = "V0.1";
        public static string Title { get; /*set;*/ } = "Image organizer" + Version;
        public static string TitleShort { get; /*set;*/ } = "IO " + Version;
        public static string LogFileName { get; set; } = LOG_NAME;
        internal static string SettingsFileName { get; set; } = "Settings.xml";
        public static string ExeFilePath { get; set; } = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static string FolderSeparator { get; set; } = (ExeFilePath.Substring(1, 2) == ":\\") ? "\\" : "/";


        public static string FolderPathInput { get; set; }
        public static string FolderPathOutput { get; set; }
        public static string NewNameFolder { get; set; }
        public static string NewNameFile { get; set; }
        public static string OldNameFolder { get; set; }
        public static string OldNameFile { get; set; }

        public static bool AnalyzeWithSubfolders { get; set; }
        public static bool JustRenameFiles { get; set; }
        public static bool FolderForCameraType { get; set; }
        public static bool DeleteEmptyFolders { get; set; }
        public static bool UseTrashFolder { get; set; }

        /// <summary>
        /// Writes the Settings.xml file
        /// </summary>
        public static void WriteSettings()
        {
            // write xml
            XDocument xmlDoc = new XDocument();

            // root element
            XElement root = new XElement("Settings");

            // defaults
            XElement defaults = new XElement("Defaults");

            // new names
            XElement newNames = new XElement("NewNames");
            newNames.Add(new XElement("NewNameFolder", NewNameFolder));
            newNames.Add(new XElement("NewNameFile", NewNameFile));
            newNames.Add(new XElement("OldNameFolder", OldNameFolder));
            newNames.Add(new XElement("OldNewNameFile", OldNameFile));
            defaults.Add(newNames);

            // Bools
            XElement checks = new XElement("Checks");
            checks.Add(new XElement("AnalyzeWithSubfolders", AnalyzeWithSubfolders));
            checks.Add(new XElement("JustRenameFiles", JustRenameFiles));
            checks.Add(new XElement("FolderForCameraType", FolderForCameraType));
            checks.Add(new XElement("DeleteEmptyFolders", DeleteEmptyFolders));
            checks.Add(new XElement("UseTrashFolder", UseTrashFolder));
            defaults.Add(checks);

            // FilePaths
            XElement paths = new XElement("Paths");
            XElement pathInput = new XElement("Path");
            pathInput.Add(new XElement("Type", "Input"));
            pathInput.Add(new XElement("FolderPath", FolderPathInput));
            paths.Add(pathInput);
            XElement pathOutput = new XElement("Path");
            pathOutput.Add(new XElement("Type", "Output"));
            pathOutput.Add(new XElement("FolderPath", FolderPathOutput));
            paths.Add(pathOutput);

            // add elements to root document
            root.Add(defaults);
            root.Add(paths);

            xmlDoc.Add(root);         

            string sPath = Toolbox.CombinePathAndFileName(ExeFilePath, SettingsFileName);
            xmlDoc.Save(sPath);
        }

        /// <summary>
        /// Reads the Settings.xml file
        /// </summary>
        public static void ReadSettings()
        {
            // update log file name
            if (LogFileName == LOG_NAME)
            {
                string dirLogFile = Toolbox.CombinePathAndFileName("LogFiles");
                Directory.CreateDirectory(dirLogFile);
                string date = DateTime.Now.Year.ToString("d4") + DateTime.Now.Month.ToString("d2") + DateTime.Now.Day.ToString("d2");
                string time = DateTime.Now.Hour.ToString("d2") + DateTime.Now.Minute.ToString("d2") + DateTime.Now.Second.ToString("d2");
                string pathLogFile = Toolbox.CombinePathAndFileName(dirLogFile, $"LogFile_{date}_{time}.txt");
                LogFileName = pathLogFile;
            }

            // read settings from xml
            try
            { 
                if (!File.Exists(SettingsFileName))
                {
                    // create empty xml
                    XDocument newXml = new XDocument();
                    newXml.Add(new XElement("Settings"));
                    string sPath = Toolbox.CombinePathAndFileName(ExeFilePath, SettingsFileName);
                    newXml.Save(sPath);
                }

                // read xml
                var xmlDoc = XDocument.Load(SettingsFileName);
                
                // defaults
                XElement savedDefaults = xmlDoc.Root.Element("Defaults");
                
                try
                {
                    XElement newNames = savedDefaults.Element("NewNames");
                    NewNameFolder = newNames.Element("NewNameFolder").Value.ToString();
                    NewNameFile = newNames.Element("NewNameFile").Value.ToString();
                    OldNameFolder = newNames.Element("OldNameFolder").Value.ToString();
                    OldNameFile = newNames.Element("OldNameFile").Value.ToString();
                }
                catch { }

                // Bools
                try
                {
                    XElement checks = savedDefaults.Element("Checks");
                    AnalyzeWithSubfolders = Convert.ToBoolean(
                        checks.Element("AnalyzeWithSubfolders").Value.ToString());
                    JustRenameFiles = Convert.ToBoolean(
                        checks.Element("JustRenameFiles").Value.ToString());
                    FolderForCameraType = Convert.ToBoolean(
                        checks.Element("FolderForCameraType").Value.ToString());
                    DeleteEmptyFolders = Convert.ToBoolean(
                        checks.Element("DeleteEmptyFolders").Value.ToString());
                    UseTrashFolder = Convert.ToBoolean(
                        checks.Element("UseTrashFolder").Value.ToString());
                }
                catch { }

                // paths
                XElement savedPaths = xmlDoc.Root.Element("Paths");

                var PathElements = from el in xmlDoc.Descendants().Elements("Path") select el;

                foreach (XElement singlePath in PathElements)
                {
                    try
                    {
                        string typeofPath = (string)singlePath.Element("Type");
                        if (typeofPath == "Input")
                        {
                            FolderPathInput= (string)singlePath.Element("FolderPath").Value;
                        }
                        if (typeofPath == "Output")
                        {
                            FolderPathOutput = (string)singlePath.Element("FolderPath").Value;
                        }
                    }
                    catch { }
                }

            }
            catch { }

            // Default values
            if (string.IsNullOrEmpty(FolderPathInput))
            {
                FolderPathInput = "";
                Directory.CreateDirectory(Toolbox.EvaluateFolderPath(FolderPathInput, ExeFilePath));
            }
            if (string.IsNullOrEmpty(FolderPathOutput))
            {
                FolderPathOutput = "";
                Directory.CreateDirectory(Toolbox.EvaluateFolderPath(FolderPathOutput, ExeFilePath));
            }
            if (string.IsNullOrEmpty(NewNameFolder))
            {
                NewNameFolder = $"<YEAR>{FolderSeparator}<CAM>";
            }
            if (string.IsNullOrEmpty(NewNameFile))
            {
                NewNameFile = "<YEAR><MONTH><DAY>-<HOUR><MIN><SEC>-<TAG>";
            }
            if (string.IsNullOrEmpty(OldNameFolder))
            {
                OldNameFolder = $"<YEAR>{FolderSeparator}<MONTH><MONTHNAME>_<TAG>";
            }
            if (string.IsNullOrEmpty(OldNameFile))
            {
                OldNameFile = "<YEAR>_<MONTH>_<DAY>-<HOUR>h_<MIN>min_<SEC>s";
            }
        }
    }
}
