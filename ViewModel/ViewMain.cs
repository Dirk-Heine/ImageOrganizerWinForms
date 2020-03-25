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
        private int _CountFilesToMove = 0;
        private int _CountFilesMoved = 0;
        private int _CountFilesDeleted = 0;
        private int _CountFoldersDeleted = 0;

        //we init this once so that if the function is repeatedly called
        //it isn't stressing the garbage man
        private static Regex r = new Regex(":");

        public BindingList<FileData> FilesInFolder;
        public BindingSource FilesInFolderSource;
        public string FolderTrash { get { return Toolbox.CombinePathAndFileName(FolderPathOutput.Text, "TRASH"); } }

        public class FileData
        {
            public string FileName { get { return _FileName; } set { _FileName = value;} }
            private string _FileName;
            public string FileNameNew { get { return _FileNameNew; } set { _FileNameNew = value; } }
            private string _FileNameNew;
            public string FilePath { get { return _FilePath; } set { _FilePath = value; } }
            private string _FilePath;
            public string FilePathNew { get { return _FilePathNew; } set { _FilePathNew = value; } }
            private string _FilePathNew;
            public string DirectoryName { get { return _DirectoryName; } set { _DirectoryName = value; } }
            private string _DirectoryName;
            public string DirectoryNameNew { get { return _DirectoryNameNew; } set { _DirectoryNameNew = value; } }
            private string _DirectoryNameNew;
            public string FileType { get { return _FileType; } set { _FileType = value; } }
            private string _FileType;
            public long FileSize { get { return _FileSize; } set { _FileSize = value; } }
            private long _FileSize;
            public string CameraType { get { return _CameraType; } set { _CameraType = value; } }
            private string _CameraType;
            public DateTime DateTaken { get { return _DateTaken; } set { _DateTaken = value; } }
            private DateTime _DateTaken;

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
            _FileCounter = 0;
            _CountFilesToMove = 0;

            // find all projects in default folder and subfolders
            ShowMessage($"Getting all file names in \"{FolderPathInput.Text}\"");
            SearchOption searchOption = AnalyzeWithSubFolders.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            string[] filePaths = System.IO.Directory.GetFiles(FolderPathInput.Text, "*.*", searchOption);
            for (int i = 0; i < filePaths.Length; i++)
            {
                // canceling
                if (_WorkerUi.CancellationPending)
                {
                    wEvent.Cancel = true;
                    break;
                }

                // Create file data
                FileData f = new FileData();
                f.FilePath = filePaths[i];
                f.FileType = Path.GetExtension(f.FilePath);
                f.FileName = Path.GetFileName(f.FilePath);

                // progress
                _WorkerUi.ReportProgress((int)((double)(i) / filePaths.Length * 100), $"Analyzing {f.FilePath.Replace(FolderPathInput.Text,"")}");

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
                            if (dateTaken != null && dateTakenFormat.Year>1970) f.DateTaken = dateTakenFormat;

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

                    //var dict = GetExtendedFileProperties(f.FilePath);
                    //string cameraType = GetExtendedFileProperty(f.FilePath, ExtFileProp.CAMERA_TYPE);
                    //string dateTaken = GetExtendedFileProperty(f.FilePath, ExtFileProp.DATE_TAKEN);

                    //string[] validformats = new[] { "dd.MM.yyyy", "dd.MM.yyyy HH:mm:ss", "dd.MM.yyyy HH:mm" };
                    //DateTime dateTakenConv;
                    //if (cameraType != null) f.CameraType = cameraType;
                    //if (dateTaken != null && DateTime.TryParseExact(dateTaken, validformats,
                    //    CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTakenConv))
                    //{
                    //    f.DateTaken = dateTakenConv;
                    //}

                    //Dictionary<string, string> ExifInfos = new Dictionary<string, string>();
                    //IEnumerable<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(f.FilePath);
                    //foreach (var dir in directories)
                    //{
                    //    foreach (var tag in dir.Tags)
                    //    {
                    //        if (!ExifInfos.ContainsKey(tag.Name))
                    //        {
                    //            ExifInfos.Add(tag.Name, tag.Description);
                    //            _ShowMessage($"[{dir.Name}] {tag.Name} = {tag.Description}");
                    //        }
                    //    }
                    //}
                    //if (_IsImage(f))
                    //{
                    //foreach (var dir in directories)
                    //    {
                    //        foreach (var tag in dir.Tags)
                    //            _ShowMessage($"[{dir.Name}] {tag.Name} = {tag.Description}");

                    //        if (dir.HasError)
                    //        {
                    //            foreach (var error in dir.Errors)
                    //                _ShowMessage($"ERROR: {error}");
                    //        }
                    //    }

                    //try
                    //{
                    //    // access the date time
                    //    var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                    //    var cameraType = subIfdDirectory?.GetString(ExifDirectoryBase.TagModel);
                    //    if (cameraType != null) f.CameraType = cameraType;
                    //    var dateTime = subIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTime);
                    //    if (dateTime != null) f.DateTaken = (DateTime)dateTime;
                    //}
                    //catch (Exception)
                    //{
                    //}


                    //using (FileStream fs = new FileStream(f.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    //{
                    //BitmapSource img = BitmapFrame.Create(fs);
                    //BitmapMetadata md = (BitmapMetadata)img.Metadata;

                    //DateTime date = Convert.ToDateTime(md.DateTaken);
                    //try
                    //{
                    //    f.CameraType = md.CameraModel != null ? md.CameraModel : "Unknown";
                    //    f.DateTaken = md.DateTaken != null ? date : fi.CreationTime;
                    //}
                    //catch (Exception)
                    //{
                    //}

                    //if (date != f.DateTaken)
                    //{
                    //    // TODO: Aufnahmedatum anpassen!?
                    //}
                    //}
                    //}
                    //if (_IsVideo(f))
                    //{
                    //    // TODO: Woher krieg ich die Metadaten??
                    //}

                    if (!f.FilePath.Equals(f.FilePathNew)) _CountFilesToMove++;

                    // Create new name
                    _GetNewFileName(ref f);
                    ShowMessage($"New name {f.FilePath.Replace(FolderPathInput.Text, "")} -> {f.FilePathNew.Replace(FolderPathOutput.Text, "")}");

                    // Add file to collection
                    _FileCounter++;
                    FilesInFolder.Add(f);
                }
                //else if (f.FileType.Contains("_"))
                //{
                //    string ending = f.FileType.Substring(f.FileType.IndexOf('_'));
                //    string name = f.FilePath.Replace(f.FileType, "") + ending + f.FileType.Replace(ending, "");
                //    // Rename
                //    File.Move(f.FilePath, name);
                //    _ShowMessage($"Renamed {f.FilePath} to: {name}");
                //}

            }
            FilesInFolder.RaiseListChangedEvents = true;
            ShowMessage($"Valid files found: {_FileCounter}");
            ShowMessage($"Files to move: {_CountFilesToMove}");
        }

        //public static class ExtFileProp
        //{
        //    // 3: Änderungsdatum
        //    // 12: Aufnahmedatum
        //    // 30: Kameramodell
        //    public const int LAST_MODIFIED = 3;
        //    public const int DATE_TAKEN = 12;
        //    public const int CAMERA_TYPE = 30;
        //}
        //public static string GetExtendedFileProperty(string filePath, int property)
        //{
        //    string value = string.Empty;
        //    string baseFolder = Path.GetDirectoryName(filePath);
        //    string fileName = Path.GetFileName(filePath);

        //    //Method to load and execute the Shell object for Windows server 8 environment otherwise you get "Unable to cast COM object of type 'System.__ComObject' to interface type 'Shell32.Shell'"
        //    Type shellAppType = Type.GetTypeFromProgID("Shell.Application");
        //    Object shell = Activator.CreateInstance(shellAppType);
        //    Shell32.Folder shellFolder = (Shell32.Folder)shellAppType.InvokeMember("NameSpace", System.Reflection.BindingFlags.InvokeMethod, null, shell, new object[] { baseFolder });

        //    //Parsename will find the specific file I'm looking for in the Shell32.Folder object
        //    Shell32.FolderItem folderitem = shellFolder.ParseName(fileName);
        //    if (folderitem != null)
        //    {
        //        //Read value of property
        //        value = shellFolder.GetDetailsOf(folderitem, property);
        //    }
        //    //returns string.Empty if no value was found for the specified property
        //    return value;
        //}

        //public static string GetExtendedFileProperty(string filePath, string propertyName)
        //{
        //    string value = string.Empty;
        //    string baseFolder = Path.GetDirectoryName(filePath);
        //    string fileName = Path.GetFileName(filePath);

        //    //Method to load and execute the Shell object for Windows server 8 environment otherwise you get "Unable to cast COM object of type 'System.__ComObject' to interface type 'Shell32.Shell'"
        //    Type shellAppType = Type.GetTypeFromProgID("Shell.Application");
        //    Object shell = Activator.CreateInstance(shellAppType);
        //    Shell32.Folder shellFolder = (Shell32.Folder)shellAppType.InvokeMember("NameSpace", System.Reflection.BindingFlags.InvokeMethod, null, shell, new object[] { baseFolder });

        //    //Parsename will find the specific file I'm looking for in the Shell32.Folder object
        //    Shell32.FolderItem folderitem = shellFolder.ParseName(fileName);
        //    if (folderitem != null)
        //    {
        //        for (int i = 0; i < short.MaxValue; i++)
        //        {
        //            //Get the property name for property index i
        //            string property = shellFolder.GetDetailsOf(null, i);

        //            //Will be empty when all possible properties has been looped through, break out of loop
        //            if (String.IsNullOrEmpty(property)) break;

        //            //Skip to next property if this is not the specified property
        //            if (property != propertyName) continue;

        //            //Read value of property
        //            value = shellFolder.GetDetailsOf(folderitem, i);
        //        }
        //    }
        //    //returns string.Empty if no value was found for the specified property
        //    return value;
        //}

        //public static Dictionary<string,string> GetExtendedFileProperties(string filePath)
        //{
        //    string value = string.Empty;
        //    string baseFolder = Path.GetDirectoryName(filePath);
        //    string fileName = Path.GetFileName(filePath);
        //    Dictionary<string, string> dict = new Dictionary<string, string>();

        //    //Method to load and execute the Shell object for Windows server 8 environment otherwise you get "Unable to cast COM object of type 'System.__ComObject' to interface type 'Shell32.Shell'"
        //    Type shellAppType = Type.GetTypeFromProgID("Shell.Application");
        //    Object shell = Activator.CreateInstance(shellAppType);
        //    Shell32.Folder shellFolder = (Shell32.Folder)shellAppType.InvokeMember("NameSpace", System.Reflection.BindingFlags.InvokeMethod, null, shell, new object[] { baseFolder });

        //    //Parsename will find the specific file I'm looking for in the Shell32.Folder object
        //    Shell32.FolderItem folderitem = shellFolder.ParseName(fileName);
        //    if (folderitem != null)
        //    {
        //        for (int i = 0; i < short.MaxValue; i++)
        //        {
        //            //Get the property name for property index i
        //            string property = shellFolder.GetDetailsOf(null, i);

        //            //Will be empty when all possible properties has been looped through, break out of loop
        //            if (String.IsNullOrEmpty(property)) break;

        //            //Read value of property
        //            value = shellFolder.GetDetailsOf(folderitem, i);

        //            //Add to dictionary
        //            dict.Add(property, value);
        //        }
        //    }
        //    //returns string.Empty if no value was found for the specified property
        //    return dict;
        //}

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
            f.FileNameNew = _ReplaceTags(NewNameFile.Text, f) + f.FileType.ToLower();
            f.DirectoryNameNew = Toolbox.CombinePathAndFileName(FolderPathOutput.Text, _ReplaceTags(NewNameFolder.Text, f));
            f.FilePathNew = Toolbox.CombinePathAndFileName(f.DirectoryNameNew, f.FileNameNew);
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
            newString = newString.Replace("<DAY>", f.DateTaken.Day.ToString("d2"));
            newString = newString.Replace("<HOUR>", f.DateTaken.Hour.ToString("d2"));
            newString = newString.Replace("<MIN>", f.DateTaken.Minute.ToString("d2"));
            newString = newString.Replace("<SEC>", f.DateTaken.Second.ToString("d2"));
            newString = newString.Replace("<NUM>", (_FileCounter).ToString("d5"));
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
            if (FilesInFolder == null)
            {
                _DoWorkAnalyzeFolder(sender, wEvent);
            }
            _FileCounter = 0;
            _CountFilesDeleted = 0;
            _CountFilesMoved = 0;
            _CountFoldersDeleted = 0;
            if (FilesInFolder == null)
            {
                ShowMessage("No files to organize.");
            }
            else
            {
                foreach (var f in FilesInFolder)
                {
                    _FileCounter++;

                    // No action needed
                    if (f.FilePathNew.Equals(f.FilePath))
                    {
                        continue;
                    }

                    // Check if file already exists in Output folder
                    string newPath = f.FilePathNew;
                    string newName = f.FileNameNew.Replace(f.FileType, "");
                    int count = 0;
                    while (File.Exists(newPath))
                    {
                        FileInfo fi = new FileInfo(f.FilePathNew);
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
                            newName = f.FileNameNew + "_" + ++count;
                            newPath = Toolbox.CombinePathAndFileName(f.DirectoryNameNew, newName + f.FileType);
                        }
                    }
                    f.FileNameNew = newName;

                    string msg = string.Empty;
                    if (File.Exists(f.FilePath))
                    {
                        // Check if new directory exists
                        System.IO.Directory.CreateDirectory(f.DirectoryNameNew);

                        if (CheckedJustRename.Checked)
                        {
                            // Rename
                            string filePath = Toolbox.CombinePathAndFileName(f.DirectoryName, newName);
                            File.Move(f.FilePath, filePath);
                            msg = $"Renamed {f.FilePath.Replace(FolderPathInput.Text, "")} to: {filePath.Replace(FolderPathOutput.Text, "")}";
                        }
                        else
                        {
                            File.Move(f.FilePath, newPath);
                            msg = $"Moved {f.FilePath.Replace(FolderPathInput.Text, "")} to: {newPath.Replace(FolderPathOutput.Text, "")}";
                        }
                        _CountFilesMoved++;
                    }

                    // canceling
                    if (_WorkerUi.CancellationPending)
                    {
                        wEvent.Cancel = true;
                        break;
                    }

                    // progress
                    _WorkerUi.ReportProgress((int)((double)(_FileCounter) / FilesInFolder.Count * 100), msg);
                }
            }

            FilesInFolder = null;

            // Delete empty folders
            if (DeleteEmptyFolders.Checked)
            {
                _DeleteEmptyDirectories(FolderPathInput.Text);
            }

            ShowMessage($"Files moved: {_CountFilesMoved}");
            ShowMessage($"Files deleted: {_CountFilesDeleted}");
            ShowMessage($"Folders deleted: {_CountFoldersDeleted}");
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
                if (e.UserState != null)
                {
                    ShowMessage(e.UserState.ToString());
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
                ShowMessage("Error: ", LogImage.Error, exp.InnerException);
            }

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
        private void _ShowMessage(LogEntry entry)
        {
            ShowMessage(entry.Message, entry.ImageSource);
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
