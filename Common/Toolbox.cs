﻿using System.IO;
using System.Windows.Forms;
using System.Threading;
using ImageOrganizerWinForms.Model;
using System;
using System.Linq;

namespace ImageOrganizerWinForms
{
    public static class Toolbox
    {
        public static string PathExitSpaces(string path)
        {
            if (ModelSettings.FolderSeparator == "/")
            {
                //return path.Replace(" ", "\\ ");
                //return "\"" + path + "\"";
                return path;
            }
            else
            {
                return path;
            }
        }

        /// <summary>
        /// Returns full folder path (related to FilePath)
        /// </summary>
        public static string EvaluateFolderPath(string path, string basePath)
        {
            string fullPath;
            if ((path.Length >= 3 && path.Substring(1, 2) == ":\\"
                || path.Length >= 2 && path.StartsWith("\\\\")) && ModelSettings.FolderSeparator == "\\"
                || Directory.Exists(path) && ModelSettings.FolderSeparator == "/")
            {
                fullPath = path;
            }
            else if (path.StartsWith(ModelSettings.FolderSeparator))
            {
                fullPath = basePath + path;
            }
            else
            {
                fullPath = basePath + ModelSettings.FolderSeparator + path;
            }
            if (fullPath.EndsWith(ModelSettings.FolderSeparator))
            {
                fullPath = fullPath.Substring(0, fullPath.LastIndexOf(ModelSettings.FolderSeparator));
            }
            return fullPath.Replace("\\",ModelSettings.FolderSeparator).Replace("/", ModelSettings.FolderSeparator);
        }

        /// <summary>
        /// Combines path and fileName properly
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string CombinePathAndFileName(string path, string fileName = "")
        {
            string newPath;
            path = EvaluateFolderPath(path, ModelSettings.ExeFilePath);
            if (path.EndsWith(ModelSettings.FolderSeparator))
            {
                newPath = path + fileName;
            }
            else
            {
                newPath = path + ModelSettings.FolderSeparator + fileName;
            }
            return newPath.Replace("\\", ModelSettings.FolderSeparator).Replace("/", ModelSettings.FolderSeparator);
        }

        /// <summary>
        /// Opens FileDialog to choose project through filepath
        /// </summary>
        public static string OpenFolder(string defaultPath, string title = "Select folder")
        {
            //Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog brwsr =
            //    new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
            //brwsr.InitialDirectory = EvaluateFolderPath(defaultPath, ModelSettings.ExeFilePath);
            //brwsr.IsFolderPicker = true;
            //brwsr.EnsureFileExists = true;
            //brwsr.EnsurePathExists = true;
            //brwsr.Title = title;
            //if (brwsr.ShowDialog() == CommonFileDialogResult.Ok)
            //{
            //    return brwsr.FileName;
            //}

            var brwsr = new FolderBrowserDialog();
            brwsr.SelectedPath = EvaluateFolderPath(defaultPath, ModelSettings.ExeFilePath);
            brwsr.ShowNewFolderButton = true;
            brwsr.Description = title;

            if (brwsr.ShowDialog() == DialogResult.OK)
            {
                return brwsr.SelectedPath;
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// Opens FileDialog to choose file with specific fileExtensions
        /// </summary>
        public static string OpenFile(string defaultPath, string fileExtensions, string title = "Select file name")
        {
            // check if defaultPath exists
            if (!File.Exists(defaultPath) && !defaultPath.Equals(""))
            {
                Directory.CreateDirectory(defaultPath);
            }

            OpenFileDialog brwsr = new OpenFileDialog();
            brwsr.Filter = fileExtensions;
            brwsr.CheckFileExists = false;
            brwsr.CheckPathExists = false;
            brwsr.InitialDirectory = defaultPath;
            brwsr.Title = title;
            brwsr.FileName = "";

            if (brwsr.ShowDialog() == DialogResult.OK)
            {
                return brwsr.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Save FileDialog to choose file with specific fileExtensions
        /// </summary>
        public static string SaveFile(string defaultPath, string fileExtensions, string title = "Select file name")
        {
            // check if defaultPath exists
            if (!File.Exists(defaultPath))
            {
                Directory.CreateDirectory(defaultPath);
            }

            SaveFileDialog brwsr = new SaveFileDialog();
            brwsr.Filter = fileExtensions;
            //brwsr.AddExtension = true;
            //brwsr.OverwritePrompt = true;
            //brwsr.ValidateNames = true;
            brwsr.InitialDirectory = defaultPath;
            brwsr.Title = title;
            brwsr.FileName = "";

            if (brwsr.ShowDialog() == DialogResult.OK)
            {
                return brwsr.FileName;
            }
            else
            {
                return string.Empty;
            }
        }


        internal static bool _DialogPositive, _DialogNoButton;
        internal static string _DialogText;

        /// <summary>
        /// Displays a warning prompt with "text" in a textbox. "noButton" decides, if there is a "no"-Button.
        /// Otherwise there is only an "ok"-Button.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="noButton"></param>
        /// <returns></returns>
        public static bool ShowWarning(string text, bool noButton = false)
        {
            _DialogText = text;
            _DialogNoButton = noButton;

            ThreadStart childref = new ThreadStart(ThreadShowWarning);
            Thread thread = new Thread(childref);
            thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
            thread.Start(); // starts thread
                            //thread.Join(); // wait for finishing thread

            // wait
            while (thread.IsAlive)
            {
                Thread.Sleep(100); //so processor can rest for a while
            }

            return _DialogPositive;
        }
        private static void ThreadShowWarning()
        {
            // Question
            //ViewWarning warnView = new ViewWarning();
            //ViewModelWarning warnModel = new ViewModelWarning(_DialogText, _DialogNoButton);
            ////ViewModelWarning warnModel = new ViewModelWarning(text, noButton);
            //warnView.DataContext = warnModel;
            //_DialogPositive = (warnView.ShowDialog() == true);
            ////return (warnView.ShowDialog() == true);

            MessageBoxButtons yesNo = _DialogNoButton ? MessageBoxButtons.YesNo : MessageBoxButtons.OK;

            DialogResult result1 = MessageBox.Show(_DialogText,"Warning", yesNo);
            _DialogPositive = result1 == DialogResult.Yes;
        }

        /// <summary>
        /// Evaluates whether "s" is a float
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumeric(string s)
        {
            float f;
            return float.TryParse(s, out f) && (CountChars(s,'.') <= 1);
        }

        /// <summary>
        /// Evaluates whether "s" is a float
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsInteger(string s)
        {
            Int16 f;
            return Int16.TryParse(s, out f);
        }

        /// <summary>
        /// Counts chars in string  
        /// </summary>
        /// <param name="input"></param>
        /// <param name="charToCount"></param>
        /// <returns></returns>
        public static int CountChars(this string input, char charToCount)
        {
            return input.Count(x => x.Equals(charToCount));
        }


        /// <summary>
        /// Converts time in ms to a string with either hours and minutes or minutes and seconds
        /// </summary>
        /// <param name="timeInMs"></param>
        /// <returns></returns>
        public static string MillisecondsToString(double timeInMs)
        {
            double h = Math.Truncate(timeInMs / 3600000); // hours = ms / 1000 / 60 / 60
            if (h > 0)
            {
                double m = Math.Truncate((timeInMs - h * 60 * 60 * 1000) / 600000); //  min2Round = ms / 1000 / 60
                return $"{h.ToString()}h {m.ToString()}min";
            }
            else
            {
                double m = Math.Truncate(timeInMs / 60000); //  min2Round = ms / 1000 / 60 * 100
                double s = Math.Truncate((timeInMs - m * 60 * 1000) / 1000); //  min2Round = ms / 1000
                return $"{m.ToString()}min {s.ToString()}s";
            }
        }

        /// <summary>
        /// Converts month number to german string
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GetMonthName(int i)
        {
            switch (i)
            {
                case 1: return "Januar";
                case 2: return "Februar";
                case 3: return "März";
                case 4: return "April";
                case 5: return "Mai";
                case 6: return "Juni";
                case 7: return "Juli";
                case 8: return "August";
                case 9: return "September";
                case 10: return "Oktober";
                case 11: return "November";
                case 12: return "Dezember";
                default:
                    return null;
            }
        }
    }
}
