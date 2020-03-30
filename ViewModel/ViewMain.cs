using ImageOrganizerWinForms.Common;
using ImageOrganizerWinForms.Model;
//using MetadataExtractor;
//using MetadataExtractor.Formats.Exif;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ImageOrganizerWinForms.ViewModel
{
    public partial class ViewModelMain : Form
    {
        #region Syntax-Conventions
        // Using Microsoft Coding Conventions: https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/inside-a-program/coding-conventions
        // - CamelCodeSyntax
        // - PublicPropertyOrMethodOrClass
        // - _PrivatePropertyOrMethodOrClass
        // - CONSTANT_PROPERTIES
        // - localVariables (e.g. in methods)
        // 
        // - Comments start with big letter and end with dot.
        #endregion

        #region Properties
        private AbortableBackgroundWorker _WorkerUi;
        private int _FileCounter = 0;
        private int _OtherFilesCounter = 0;
        //private int FilesToMove.Value = 0;
        //private int FilesMoved.Value = 0;
        private int _CountFilesDeleted = 0;
        private int _CountFoldersDeleted = 0;
        Stopwatch TimeLeftToMove = new Stopwatch();
        Dictionary<string, string> DirectoriesRenamed = new Dictionary<string, string>();

        //we init this once so that if the function is repeatedly called
        //it isn't stressing the garbage man
        private static Regex r = new Regex(":");

        public BindingList<FileData> FilesInFolder;
        public BindingList<FileData> OtherFilesInFolder;
        public BindingSource FilesInFolderSource;
        public string FolderTrash { get { return Toolbox.CombinePathAndFileName(FolderPathOutput.Text, "TRASH"); } }

        public class FileData
        {
            public string FileName { get { return _FileName; } set { _FileName = value;} }
            private string _FileName;
            public string FilePath { get { return _FilePath; } set { _FilePath = value; } }
            private string _FilePath;
            public string DirectoryName { get { return _DirectoryName; } set { _DirectoryName = value; } }
            private string _DirectoryName;
            public string FileNameNew { get { return _FileNameNew; } set { _FileNameNew = value; } }
            private string _FileNameNew;
            public string FilePathNew { get { return _FilePathNew; } set { _FilePathNew = value; } }
            private string _FilePathNew;
            public string DirectoryNameNew { get { return _DirectoryNameNew; } set { _DirectoryNameNew = value; } }
            private string _DirectoryNameNew;
            public string FileNameOld { get { return _FileNameOld; } set { _FileNameOld = value; } }
            private string _FileNameOld;
            public string FilePathOld { get { return _FilePathOld; } set { _FilePathOld = value; } }
            private string _FilePathOld;
            public string DirectoryNameOld { get { return _DirectoryNameOld; } set { _DirectoryNameOld = value; } }
            private string _DirectoryNameOld;
            public string FileType { get { return _FileType; } set { _FileType = value; } }
            private string _FileType;
            public long FileSize { get { return _FileSize; } set { _FileSize = value; } }
            private long _FileSize;
            public string CameraType { get { return _CameraType; } set { _CameraType = value; } }
            private string _CameraType;
            public DateTime DateTaken { get { return _DateTaken; } set { _DateTaken = value; } }
            private DateTime _DateTaken;
            public string Tag { get { return _Tag; } set { _Tag = value; } }
            private string _Tag;

            public FileData(string fileName = "", string fileType = "", string filePath = "")
            {
                FileName = fileName;
                FileType = fileType;
                FilePath = filePath;
            }
        }

        string[] SupportedImages = new string[] {
            ".jpg",
            ".png",
            ".jpeg"
        };

        string[] SupportedVideos = new string[] {
            ".wmv",
            ".mp4",
            ".avi",
            ".3gp",
            ".mpo",
            ".wav",
            ".mts",
            ".mov"
        };

        #endregion

        public ViewModelMain()
        {
            InitializeComponent();
            ModelSettings.ReadSettings();
            FolderPathInput.Text = ModelSettings.FolderPathInput;
            FolderPathOutput.Text = ModelSettings.FolderPathOutput;
            NewNameFolder.Text = ModelSettings.NewNameFolder;
            NewNameFile.Text = ModelSettings.NewNameFile;
            OldNameFolder.Text = ModelSettings.OldNameFolder;
            OldNameFile.Text = ModelSettings.OldNameFile;

            AnalyzeWithSubFolders.Checked = ModelSettings.AnalyzeWithSubfolders;
            CheckedJustRename.Checked = ModelSettings.JustRenameFiles;
            CheckedCameraType.Checked = ModelSettings.FolderForCameraType;
            DeleteEmptyFolders.Checked = ModelSettings.DeleteEmptyFolders;
            UseTrashFolder.Checked = ModelSettings.UseTrashFolder;

            // initialize data grid view
            FilesInFolderSource = new BindingSource(FilesInFolder, null);
            FilesInFolderDataGrid.AutoGenerateColumns = false;
            FilesInFolderDataGrid.AutoSize = true;

            // Initialize and add a text box column.
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "FilePath";
            column.CellTemplate = cell;
            column.Name = column.DataPropertyName;
            column.HeaderText = "File";
            FilesInFolderDataGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "FilePathNew";
            column.CellTemplate = cell;
            column.Name = column.DataPropertyName;
            column.HeaderText = "New file";
            FilesInFolderDataGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "CameraType";
            column.CellTemplate = cell;
            column.Name = column.DataPropertyName;
            column.HeaderText = "Camera";
            FilesInFolderDataGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "DateTaken";
            column.CellTemplate = cell;
            column.Name = column.DataPropertyName;
            column.HeaderText = "Date taken";
            FilesInFolderDataGrid.Columns.Add(column);
            
            FilesInFolderDataGrid.DataSource = FilesInFolderSource;
        }
        private void CancelWorker_Click(object sender, EventArgs e)
        {
            _CancelWorker(null);
        }

        private void SaveSettings_Click(object sender, EventArgs e)
        {
            //_OpenSettings(null);
            ModelSettings.FolderPathInput = FolderPathInput.Text;
            ModelSettings.FolderPathOutput = FolderPathOutput.Text;
            ModelSettings.NewNameFile = NewNameFile.Text;
            ModelSettings.NewNameFolder = NewNameFolder.Text;
            ModelSettings.OldNameFile = OldNameFile.Text;
            ModelSettings.OldNameFolder = OldNameFolder.Text;
            ModelSettings.AnalyzeWithSubfolders = AnalyzeWithSubFolders.Checked;
            ModelSettings.JustRenameFiles = CheckedJustRename.Checked;
            ModelSettings.FolderForCameraType = CheckedCameraType.Checked;
            ModelSettings.DeleteEmptyFolders = DeleteEmptyFolders.Checked;
            ModelSettings.UseTrashFolder = UseTrashFolder.Checked;

            ModelSettings.WriteSettings();

            ShowMessage("Settings saved.");
        }

        private void SelectFolderInput_Click(object sender, EventArgs e)
        {
            string path = Toolbox.OpenFolder(FolderPathInput.Text);
            if (!string.IsNullOrEmpty(path)) FolderPathInput.Text = path;
        }

        private void SelectFolderOutput_Click(object sender, EventArgs e)
        {
            string path = Toolbox.OpenFolder(FolderPathOutput.Text);
            if (!string.IsNullOrEmpty(path)) FolderPathOutput.Text = path;
        }

        private void AnalyzeFolderInput_Click(object sender, EventArgs e)
        {
            _AnalyzeFolder(null);
        }

        private void OrganizeFiles_Click(object sender, EventArgs e)
        {
            _OrganizeFolder(null);
        }

        private void RenewFileNames_Click(object sender, EventArgs e)
        {
            _GetNewNames(null);
        }


        #region Methods

        #region UI
        /// <summary>
        /// Closes TIA portal when closing the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            // waiting for BackgroundWorker
            if (_WorkerUi != null && _WorkerUi.IsBusy)
            {
                if (!Toolbox.ShowWarning("Worker is busy. Abort worker and close window?", true))
                {
                    e.Cancel = true;
                    return;
                }

                if (_WorkerUi != null) _WorkerUi.Abort();
            }
        }

        
        private void _AnalyzeFolder(object parameter)
        {
            if (!System.IO.Directory.Exists(FolderPathInput.Text))
            {
                ShowMessage($"Input folder does not exist: \"{FolderPathInput.Text}\"");
                return;
            }

            _WorkerInit(ref _WorkerUi);
            _WorkerUi.DoWork += _DoWorkAnalyzeFolder;
            _WorkerUi.RunWorkerAsync();
        }
        private void _DoWorkAnalyzeFolder(object sender, DoWorkEventArgs wEvent)
        {
            FilesInFolder = new BindingList<FileData>();
            OtherFilesInFolder = new BindingList<FileData>();
            _FileCounter = 0;
            _OtherFilesCounter = 0;
            TimeLeftToMove.Start();
            Invoke((Action)delegate { FilesToMove.Value = 0; });

            string[] filePaths = _GetAllInputFiles();
            for (int i = 0; i < filePaths.Length; i++)
            {
                // Analyze
                FileData f = new FileData();
                f.FilePath = filePaths[i];
                _AnalyzeFile(ref f);

                // canceling
                if (_WorkerUi.CancellationPending)
                {
                    wEvent.Cancel = true;
                    break;
                }

                // progress
                _WorkerUi.ReportProgress((int)((double)(i) / filePaths.Length * 100), $"Analyzing {f.FilePath.Replace(FolderPathInput.Text, "")}");
            }
            ShowMessage($"Valid files found: {_FileCounter}");
            ShowMessage($"Files to move: {FilesToMove.Value}");
        }
        private bool _AnalyzeFile(ref FileData f)
        {
            bool retVal = false;
            f.FileType = Path.GetExtension(f.FilePath);
            f.FileName = Path.GetFileName(f.FilePath);

            // Create file data
            try
            {
                // File infos
                FileInfo fi = new FileInfo(f.FilePath);
                f.DirectoryName = fi.DirectoryName;
                f.FileSize = fi.Length;
                f.DateTaken = fi.LastWriteTime; //fi.CreationTime;
                f.CameraType = "Unknown";

                // Get image meta data
                if (_IsImage(f) || _IsVideo(f))
                {
                    if (_IsImage(f))
                    {
                        using (FileStream fs = new FileStream(f.FilePath, FileMode.Open, FileAccess.Read))
                        using (Image myImage = Image.FromStream(fs, false, false))
                        {
                            PropertyItem propItem;
                            string dateTaken = string.Empty;
                            DateTime dateTakenFormat = new DateTime();
                            try
                            {
                                propItem = myImage.GetPropertyItem(0x9003);//36867); //PropertyTagExifDTOrig
                                dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                                dateTakenFormat = DateTime.Parse(dateTaken);
                            }
                            catch
                            {
                            }
                            if (dateTaken != null && dateTakenFormat.Year > 1970) f.DateTaken = dateTakenFormat;

                            ASCIIEncoding encodings = new ASCIIEncoding();
                            string cameraType = string.Empty;
                            try
                            {
                                propItem = myImage.GetPropertyItem(0x0110);//PropertyTagEquipModel;
                                cameraType = encodings.GetString(propItem.Value);
                                //cameraType = cameraType.Substring(0,cameraType.Length);
                            }
                            catch
                            {
                            }
                            if (!string.IsNullOrEmpty(cameraType)) f.CameraType = cameraType;
                        }
                    }

                    // Create new name
                    _GetNewFileName(ref f);

                    if (!f.FilePath.Equals(f.FilePathNew))
                    {
                        Invoke((Action)delegate { FilesToMove.Value++; });

                        ShowMessage($"New name {f.FilePathNew.Replace(FolderPathOutput.Text, "")}");

                        // Add file to collection
                        FilesInFolder.Add(f);
                        retVal = true;
                    }
                    _FileCounter++;
                }
                else // other files
                {
                    // Add file to collection
                    _OtherFilesCounter++;
                    OtherFilesInFolder.Add(f);
                }
            }
            catch (Exception e)
            {
                ShowMessage($"File access failed. {f.FilePath.Replace(FolderPathInput.Text, "")}", LogImage.Error, e);
            }
            return retVal;
        }
        private string[] _GetAllInputFiles()
        {
            // find all projects in default folder and subfolders
            ShowMessage($"Getting all file names in \"{FolderPathInput.Text}\"");
            SearchOption searchOption = AnalyzeWithSubFolders.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            return System.IO.Directory.GetFiles(FolderPathInput.Text, "*.*", searchOption);
        }

        private bool _IsImage(FileData f)
        {
            foreach (var ext in SupportedImages)
            {
                if (f.FileType.ToLower().Equals(ext))
                {
                    return true;
                }
            }
            return false;
        }
        private bool _IsVideo(FileData f)
        {
            foreach (var ext in SupportedVideos)
            {
                if (f.FileType.ToLower().Equals(ext))
                {
                    return true;
                }
            }
            return false;
        }

        private void _GetNewFileName(ref FileData f)
        {
            string folderOutput = CheckedJustRename.Checked ? FolderPathInput.Text : FolderPathOutput.Text;
            int folderType = 0;

            // Generate old folder
            string folderStructure = OldNameFolder.Text;
            f.DirectoryNameOld = _GetFolderName(folderOutput, folderStructure, f, ref folderType);
            f.Tag = _GetFolderTag(f.DirectoryNameOld, f);
            if (f.Tag != string.Empty)
            {
                f.DirectoryNameOld = _GetFolderName(folderOutput, folderStructure, f, ref folderType);
            }
            f.FileNameOld = _ReplaceTags(OldNameFile.Text, f) + f.FileType.ToLower();
            f.FilePathOld = Toolbox.CombinePathAndFileName(f.DirectoryNameOld, f.FileNameOld);

            // Generate new name
            folderStructure = NewNameFolder.Text;
            f.DirectoryNameNew = _GetFolderName(folderOutput, folderStructure, f, ref folderType);
            // Try to get tag name out of new folder structure
            if (f.Tag == string.Empty)
            {
                f.Tag = _GetFolderTag(f.DirectoryNameNew, f);
                if (f.Tag != string.Empty)
                {
                    f.DirectoryNameNew = _GetFolderName(folderOutput, NewNameFolder.Text, f, ref folderType);
                }
            }
            f.FileNameNew = _ReplaceTags(NewNameFile.Text, f) + f.FileType.ToLower();

            // if possible: rename old directory
            string dirNew = f.DirectoryName.Contains(f.DirectoryNameOld) ? f.DirectoryName.Replace(f.DirectoryNameOld, f.DirectoryNameNew) : f.DirectoryNameNew;
            f.FilePathNew = Toolbox.CombinePathAndFileName(dirNew, f.FileNameNew);
        }
        private string _GetFolderName(string folderToCheck, string folderStructure, FileData f, ref int folderType)
        {
            string prevStructure, nextStructure;
            if (folderStructure.Contains("<MONTH><MONTHNAME>"))
            {
                prevStructure = folderStructure.Replace("<MONTH><MONTHNAME>", "<MONTHPREV><MONTHNAMEPREV>-<MONTH><MONTHNAME>");
                nextStructure = folderStructure.Replace("<MONTH><MONTHNAME>", "<MONTH><MONTHNAME>-<MONTHNEXT><MONTHNAMENEXT>");
            }
            else
            {
                prevStructure = folderStructure.Replace("<MONTH>", "<MONTHPREV>-<MONTH>");
                nextStructure = folderStructure.Replace("<MONTH>", "<MONTH>-<MONTHNEXT>");
            }

            // Generate old folder
            string justTagsFolder = Toolbox.CombinePathAndFileName(folderToCheck, _ReplaceTags(folderStructure, f));
            string prevMonthFolder = Toolbox.CombinePathAndFileName(folderToCheck,
                _ReplaceTags(prevStructure, f));
            string nextMonthFolder = Toolbox.CombinePathAndFileName(folderToCheck,
                _ReplaceTags(nextStructure, f));

            // Does it match?
            if (f.DirectoryName.StartsWith(prevMonthFolder) || folderType == 1)
            {
                folderType = 1;
                return prevMonthFolder;
            }
            if (f.DirectoryName.StartsWith(nextMonthFolder) || folderType == 2)
            {
                folderType = 2;
                return nextMonthFolder;
            }

            return justTagsFolder;
        }
        private string _GetFolderTag(string folderToCheck, FileData f)
        {
            // Check <TAG>
            if (!f.DirectoryName.StartsWith(folderToCheck))
            {
                return string.Empty;
            }

            string restOfDir = f.DirectoryName.Replace(folderToCheck, "");
            int endOfFolder = restOfDir.Contains(ModelSettings.FolderSeparator) ? restOfDir.IndexOf(ModelSettings.FolderSeparator) : restOfDir.Length;
            string retTag = restOfDir.Substring(0, endOfFolder);
            if (retTag.StartsWith("-") || retTag.StartsWith("_"))
            {
                retTag = retTag.Substring(1);
            }
            return retTag;
        }
        private string _ReplaceTags(string s, FileData f)
        {
            string cam = f.CameraType; ;
            if (CheckedCamera.Checked)
            {
                cam = f.CameraType == "Unknown" ? "Others" : "Camera";
            }
            string newString = s.Replace("<CAM>", cam);
            newString = newString.Replace("<YEAR>", f.DateTaken.Year.ToString("d4"));
            newString = newString.Replace("<MONTH>", f.DateTaken.Month.ToString("d2"));
            newString = newString.Replace("<MONTHNAME>", _GetMonthName(f.DateTaken.Month));
            newString = newString.Replace("<MONTHPREV>", (f.DateTaken.Month - 1).ToString("d2"));
            newString = newString.Replace("<MONTHNAMEPREV>", _GetMonthName((f.DateTaken.Month - 1)));
            newString = newString.Replace("<MONTHNEXT>", (f.DateTaken.Month + 1).ToString("d2"));
            newString = newString.Replace("<MONTHNAMENEXT>", _GetMonthName((f.DateTaken.Month + 1)));
            newString = newString.Replace("<DAY>", f.DateTaken.Day.ToString("d2"));
            newString = newString.Replace("<HOUR>", f.DateTaken.Hour.ToString("d2"));
            newString = newString.Replace("<MIN>", f.DateTaken.Minute.ToString("d2"));
            newString = newString.Replace("<SEC>", f.DateTaken.Second.ToString("d2"));
            newString = newString.Replace("<NUM>", (_FileCounter).ToString("d5"));
            newString = newString.Replace("<-TAG>", f.Tag == string.Empty ? string.Empty : "-"+f.Tag);
            newString = newString.Replace("<_TAG>", f.Tag == string.Empty ? string.Empty : "_" + f.Tag);
            newString = newString.Replace("<TAG>", f.Tag);
            return newString;
        }
        private string _GetMonthName(int i)
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
                    break;
            }
        }
        
        private void _OrganizeFolder(object parameter)
        {
            _WorkerInit(ref _WorkerUi);
            _WorkerUi.DoWork += _DoWorkOrganizeFolder;
            _WorkerUi.RunWorkerAsync();
        }
        private void _DoWorkOrganizeFolder(object sender, DoWorkEventArgs wEvent)
        {
            _FileCounter = 0;
            _CountFilesDeleted = 0;
            _CountFoldersDeleted = 0;
            TimeLeftToMove.Start();
            Invoke((Action)delegate { FilesMoved.Value = 0; });

            if (FilesInFolder == null)
            {
                FilesInFolder = new BindingList<FileData>();
                OtherFilesInFolder = new BindingList<FileData>();
                string[] filePaths = _GetAllInputFiles();
                for (int i = 0; i < filePaths.Length; i++)
                {
                    // canceling
                    if (_WorkerUi.CancellationPending)
                    {
                        wEvent.Cancel = true;
                        break;
                    }

                    // new file
                    FileData f = new FileData();
                    f.FilePath = filePaths[i];
                    _RenameFileData(ref f, DirectoriesRenamed);

                    // progress
                    _WorkerUi.ReportProgress((int)((double)(i) / filePaths.Length * 100), $"Analyzing {f.FilePath.Replace(FolderPathInput.Text, "")}");

                    // Analyze
                    bool move = _AnalyzeFile(ref f);

                    // Organize
                    if (move) _OrganizeFile(f);
                }
            }
            else
            {
                //foreach (var f in FilesInFolder)
                for (int i = 0; i < FilesInFolder.Count; i++)
                {
                    FileData f = FilesInFolder[i];
                    _FileCounter++;

                    // Organize
                    _OrganizeFile(f);

                    // canceling
                    if (_WorkerUi.CancellationPending)
                    {
                        wEvent.Cancel = true;
                        break;
                    }

                    // progress
                    _WorkerUi.ReportProgress((int)((double)(_FileCounter) / FilesInFolder.Count * 100));
                }
            }

            FilesInFolder = null;

            // Delete empty folders
            if (DeleteEmptyFolders.Checked && !_WorkerUi.CancellationPending)
            {
                ShowMessage($"Deleting empty directories.");
                _DeleteEmptyDirectories(FolderPathInput.Text);
            }

            foreach (var item in OtherFilesInFolder)
            {
                ShowMessage($"Other file in folder : {item.FilePath}");
            }

            ShowMessage($"Other files in folders: {_OtherFilesCounter}");
            ShowMessage($"Files moved: {FilesMoved.Value}");
            ShowMessage($"Files deleted: {_CountFilesDeleted}");
            ShowMessage($"Folders deleted: {_CountFoldersDeleted}");
        }
        private void _OrganizeFile(FileData f)
        {

            // rename directory if necessary
            _RenameFileData(ref f, DirectoriesRenamed);

            // No action needed
            if (f.FilePathNew.Equals(f.FilePath))
            {
                ShowMessage($"No action necessary {f.FilePath.Replace(FolderPathInput.Text, "")}");
                return;
            }

            f.FilePathNew = _NewPathDublicate(f.FilePathNew, f);
            int startIndexFileName = f.FilePathNew.LastIndexOf(ModelSettings.FolderSeparator) + 1;
            //int startIndexFileName = f.FilePathNew.LastIndexOf(f.FileNameNew.Replace(f.FileType, "").Replace(f.FileType.ToLower(), ""));
            f.FileNameNew = f.FilePathNew.Substring(startIndexFileName);

            if (File.Exists(f.FilePath))
            {
                if (CheckedJustRename.Checked)
                {
                    // Rename directory 
                    // => save renamed directories in dict
                    // => TODO: recognize sub folder in folder structure!
                    string fileDir = f.DirectoryName.Replace(f.DirectoryNameOld, f.DirectoryNameNew);
                    if (f.DirectoryName.StartsWith(f.DirectoryNameOld) && f.DirectoryNameNew != f.DirectoryNameOld)// && !Directory.Exists(f.DirectoryNameNew))
                    {
                        try
                        {
                            Directory.Move(f.DirectoryNameOld, f.DirectoryNameNew);
                            DirectoriesRenamed.Add(f.DirectoryNameOld, f.DirectoryNameNew);
                            // rename directory in file data if necessary
                            _RenameFileData(ref f, DirectoriesRenamed);
                            ShowMessage($"Renamed directory {f.DirectoryName.Replace(FolderPathInput.Text, "")} to: {fileDir.Replace(FolderPathInput.Text, "")}");
                        }
                        catch (Exception e)
                        {
                            ShowMessage($"Renaming directory {f.DirectoryName.Replace(FolderPathInput.Text, "")} failed.", LogImage.Error, e);
                        }
                    }

                    // Rename file
                    if (fileDir != FolderTrash)
                    {
                        string filePath = Toolbox.CombinePathAndFileName(fileDir, f.FileNameNew);
                        filePath = _NewPathDublicate(filePath, f);
                        if (!filePath.Equals(f.FilePath) && File.Exists(f.FilePath))
                        {
                            File.Move(f.FilePath, filePath);
                            ShowMessage($"Renamed {f.FilePath.Replace(FolderPathInput.Text, "")} to: {filePath.Replace(FolderPathInput.Text, "")}");
                            Invoke((Action)delegate { FilesMoved.Value++; });
                        }
                    }
                }
                else
                {
                    // Check if new directory exists
                    System.IO.Directory.CreateDirectory(f.DirectoryNameNew);

                    // Move file to new directory
                    File.Move(f.FilePath, f.FilePathNew);
                    ShowMessage($"Moved {f.FilePath.Replace(FolderPathInput.Text, "")} to: {f.FilePathNew.Replace(FolderPathOutput.Text, "")}");
                    Invoke((Action)delegate { FilesMoved.Value++; });
                }
            }
        }
        private string _NewPathDublicate(string filePath, FileData f)
        {
            string newPath = filePath;
            string newName = filePath.Replace(f.FileType.ToLower(), "");
            int count = 0;
            if (filePath == f.FilePath.Replace(f.FileType, f.FileType.ToLower()))
            {
                return filePath;
            }
            while (File.Exists(newPath))
            {
                FileInfo fi = new FileInfo(filePath);
                //if (newPath == f.FilePath)
                //{
                //    return filePath;
                //}
                if (f.FileSize == fi.Length)
                {
                    // -> same size? delete in input folder
                    _DeleteFile(f);
                    _CountFilesDeleted++;
                    break;
                }
                else
                {
                    // -> same Name? append "_2"
                    newPath = newName + "_" + ++count + f.FileType;
                }
            }
            return newPath;
        }

        private void _RenameFileData(ref FileData f, Dictionary<string, string> directoriesRenamed)
        {
            foreach (var item in directoriesRenamed)
            {
                if (f.DirectoryName != null && f.DirectoryName.Contains(item.Key))
                {
                    f.DirectoryName = f.DirectoryName.Replace(item.Key, item.Value);
                    f.DirectoryNameNew = f.DirectoryNameNew.Replace(item.Key, item.Value);
                    f.DirectoryNameOld = f.DirectoryNameOld.Replace(item.Key, item.Value);

                    f.FilePath = f.FilePath.Replace(item.Key, item.Value);
                    f.FilePathNew = f.FilePathNew.Replace(item.Key, item.Value);
                    f.FilePathOld = f.FilePathOld.Replace(item.Key, item.Value);
                }
                else if (f.DirectoryName == null && f.FilePath.Contains(item.Key))
                {
                    f.FilePath = f.FilePath.Replace(item.Key, item.Value);
                }
            }
        }
        private void _DeleteFile(FileData f)
        {
            if (UseTrashFolder.Checked)
            {
                string newPath = Toolbox.CombinePathAndFileName(FolderTrash, f.FileName).Replace(f.FileType, "");
                string trash = newPath + f.FileType;
                int count = 0;
                while (File.Exists(trash))
                {
                    trash = newPath + "_" + ++count + f.FileType;
                }

                System.IO.Directory.CreateDirectory(FolderTrash);
                File.Move(f.FilePath, trash);
                ShowMessage($"Moved to trash folder: {f.FilePath.Replace(FolderPathInput.Text, "")}");
            }
            else
            {
                File.Delete(f.FilePath);
                ShowMessage($"Deleted: {f.FilePath.Replace(FolderPathInput.Text, "")}");
            }
        }

        private void _DeleteEmptyDirectories(string startLocation)
        {
            foreach (var directory in System.IO.Directory.GetDirectories(startLocation))
            {
                _DeleteEmptyDirectories(directory);
                if (System.IO.Directory.GetFiles(directory).Length == 0 &&
                    System.IO.Directory.GetDirectories(directory).Length == 0)
                {
                    System.IO.Directory.Delete(directory, false);
                    ShowMessage($"Directory deleted: {directory}");
                    _CountFoldersDeleted++;
                }
            }
        }
        private void _GetNewNames(object parameter)
        {
            _WorkerInit(ref _WorkerUi);
            _WorkerUi.DoWork += _DoWorkGetNewNames;
            _WorkerUi.RunWorkerAsync();
        }
        private void _DoWorkGetNewNames(object sender, DoWorkEventArgs wEvent)
        {
            _FileCounter = 0;
            if (FilesInFolder != null)
            {
                for (int i = 0; i < FilesInFolder.Count; i++)
                {
                    FileData f = FilesInFolder[i];
                    // canceling
                    if (_WorkerUi.CancellationPending)
                    {
                        wEvent.Cancel = true;
                        break;
                    }

                    _GetNewFileName(ref f);

                    // progress
                    _WorkerUi.ReportProgress((int)((double)(i) / FilesInFolder.Count * 100), $"Renamed {f.FilePathNew.Replace(FolderPathInput.Text, "")}");
                }
            }
        }

        #endregion

        #region AbortableBackgroundWorker

        private Result _WorkerInit(ref AbortableBackgroundWorker worker)
        {
            _WorkerAbort(ref worker);
            worker = new AbortableBackgroundWorker();
            worker.WorkerReportsProgress = true;
            // You have to set cancellation manually. Default is now abortion:
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerCompleted += _WorkerCompleted;
            worker.ProgressChanged += _WorkerProgressChanged;

            Progress.Visible = true;
            CancelWorker.Visible = true;

            return new Result(Status.Ok);
        }

        private void _WorkerAbort(ref AbortableBackgroundWorker worker)
        {
            if (worker != null)
            {
                worker.Abort();
                worker = null;
                Thread.Sleep(100);
                Progress.Visible = false;
                CancelWorker.Visible = false;
            }
        }

        private void _CancelWorker(object parameter)
        {
            if (_WorkerUi != null)
            {
                if (_WorkerUi.WorkerSupportsCancellation && _WorkerUi.CancellationPending
                    || !_WorkerUi.WorkerSupportsCancellation)
                {
                    _WorkerAbort(ref _WorkerUi);
                }
                else if (_WorkerUi.WorkerSupportsCancellation && _WorkerUi.IsBusy)
                {
                    _WorkerUi.CancelAsync();
                }
            }
        }

        /// <summary>
        /// Moves progress of AbortableBackgroundWorker to display. 
        /// Progress is shown in Progressbar
        /// </summary>
        private void _WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Invoke((Action)delegate
            {
                Progress.Value = Math.Min(100, e.ProgressPercentage);
                if (e.UserState != null && !string.IsNullOrEmpty(e.UserState.ToString()))
                {
                    ShowMessage(e.UserState.ToString());
                }

                if (e.ProgressPercentage > 0)
                {
                    double p = 100 / e.ProgressPercentage - 1;
                    double rawH = TimeLeftToMove.Elapsed.TotalHours * p;
                    double rawM = (TimeLeftToMove.Elapsed.TotalMinutes) * p;
                    double overM = Math.Truncate(rawM / 60);
                    rawM -= overM*60;
                    rawH += overM;
                    double h = Convert.ToDouble(Convert.ToInt64(rawH)); //Math.Truncate(rawH);// Convert.ToDouble(Convert.ToInt64(rawH));
                    double m = Convert.ToDouble(Convert.ToInt64(rawM * 100)) / 100; //Math.Truncate(rawM * 100) / 100;// Convert.ToDouble(Convert.ToInt64(rawH));
                    TimeLeft.Text = $"Time left: {h.ToString()}h {m.ToString()}min";
                }
            });
        }

        /// <summary>
        /// Refreshes datagrid after devices are loaded
        /// </summary>
        private void _WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!e.Cancelled && e.Result != null && e.Result is Result)
                {
                    ShowMessage("Error: ", LogImage.Error, (e.Result as Result).State as Exception);
                }
                else
                {
                    //Message = "";
                    Progress.Value = 0;
                    Progress.Visible = false;
                    CancelWorker.Visible = false;
                }
            }
            catch (System.Reflection.TargetInvocationException exp)
            {
                ShowMessage("Error: ", LogImage.Error, exp);
                ShowMessage("Inner exception: ", LogImage.Error, exp.InnerException);
            }

            TimeLeftToMove.Stop();
            double h = Math.Truncate(TimeLeftToMove.Elapsed.TotalHours * 100) / 100;
            double s = Math.Truncate((TimeLeftToMove.Elapsed.TotalMinutes - h * 60)  * 100) / 100;
            TimeLeft.Text = $"Time elapsed: {h.ToString()}h {s.ToString()}min";
            if (sender == _WorkerUi) _WorkerUi = null;
        }

        #endregion

        #region Logger

        /// <summary>
        /// Adds error message to display if it fits filter settings
        /// Add filter to list of all messages
        /// </summary>
        public void ShowMessage(string message, string level = LogImage.Info, Exception error = null)//, string sError)//Exception eError)
        {
            LogEntry logEntry = new LogEntry(message, level);
            LogEntry logEntryFile = new LogEntry(message, level);
            if (error != null) // TODO: Error-Message macht ja nur bei Level.Warning und Level.Error Sinn!!
            {
                if (level == LogImage.Warning)
                {
                    logEntry.Message += Environment.NewLine + "Warning: " + error.Message;
                }
                else
                {
                    logEntry.Message += Environment.NewLine + "Error: " + error.Message;
                }

                logEntryFile.Message = logEntry.Message;
                if (error.StackTrace != string.Empty) // TODO: test
                {
                    logEntryFile.Message += Environment.NewLine + error.StackTrace;
                }
            }

            ModelLogFile logFile = new ModelLogFile(Toolbox.CombinePathAndFileName(ModelSettings.ExeFilePath, ModelSettings.LogFileName));

            // Write in File
            logFile.AddLogEntry(logEntryFile);

            // Show in Status Bar
            this.Invoke((Action)delegate
            {
                string msg = logEntry.Message.Replace("_", "__");
                if (level == LogImage.Info)
                {
                    //Message = msg;
                }
                else
                {
                    //ErrorMessage = msg;
                }
                Logger.Items.Add(msg);
                Logger.TopIndex = Logger.Items.Count - 1;
            });
        }

        #endregion

        #region Settings
        private void _OpenSettings(object parameter)
        {
            //ViewSettings win = new ViewSettings();
            //ViewModelSettings vm = new ViewModelSettings();
            //win.DataContext = vm;
            //bool? bresult = win.ShowDialog();

            var form2 = new ViewModelSettings();
            form2.Show();
        }
        #endregion

        #endregion
    }
}
