using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ImageOrganizerWinForms.Common
{
    public class FileData
    {
        public string FileName; //{ get { return _FileName; } set { _FileName = value; } }
        //private string _FileName;
        public string FilePath; //{ get { return _FilePath; } set { _FilePath = value; } }
        //private string _FilePath;
        public string DirectoryName; //{ get { return _DirectoryName; } set { _DirectoryName = value; } }
        //private string _DirectoryName;
        public string FileNameNew; //{ get { return _FileNameNew; } set { _FileNameNew = value; } }
        //private string _FileNameNew;
        public string FilePathNew; //{ get { return _FilePathNew; } set { _FilePathNew = value; } }
        //private string _FilePathNew;
        public string DirectoryNameNew; //{ get { return _DirectoryNameNew; } set { _DirectoryNameNew = value; } }
        //private string _DirectoryNameNew;
        public string FileNameOld; //{ get { return _FileNameOld; } set { _FileNameOld = value; } }
        //private string _FileNameOld;
        public string FilePathOld; //{ get { return _FilePathOld; } set { _FilePathOld = value; } }
        //private string _FilePathOld;
        public string DirectoryNameOld; //{ get { return _DirectoryNameOld; } set { _DirectoryNameOld = value; } }
        //private string _DirectoryNameOld;
        public string FileType; //{ get { return _FileType; } set { _FileType = value; } }
        //private string _FileType;
        public long FileSize; //{ get { return _FileSize; } set { _FileSize = value; } }
        //private long _FileSize;
        public string CameraType; //{ get { return _CameraType; } set { _CameraType = value; } }
        //private string _CameraType;
        public DateTime DateTaken; //{ get { return _DateTaken; } set { _DateTaken = value; } }
        //private DateTime _DateTaken;
        public string Tag; //{ get { return _Tag; } set { _Tag = value; } }
                           //private string _Tag;

        public const string YEAR = "<YEAR>";
        public const string MONTH = "<MONTH>";
        public const string DAY = "<DAY>";
        public const string HOUR = "<HOUR>";
        public const string MIN = "<MIN>";
        public const string SEC = "<SEC>";

        private string[] _SupportedImages = new string[] {
            ".jpg",
            ".png",
            ".jpeg",
            ".gif"
        };

        private string[] _SupportedVideos = new string[] {
            ".wmv",
            ".mp4",
            ".avi",
            ".3gp",
            ".mpo",
            ".wav",
            ".mts",
            ".mov"
        };

        public FileData(string fileName = "", string fileType = "", string filePath = "")
        {
            FileName = fileName;
            FileType = fileType;
            FilePath = filePath;
        }

        public bool IsImage()
        {
            foreach (var ext in _SupportedImages)
            {
                if (FileType.ToLower().Equals(ext))
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsVideo()
        {
            foreach (var ext in _SupportedVideos)
            {
                if (FileType.ToLower().Equals(ext))
                {
                    return true;
                }
            }
            return false;
        }
        
        public string ReplaceTags(string s, bool camera, int counter)
        {
            string cam = CameraType; ;
            if (camera)
            {
                cam = CameraType == "Unknown" ? "Others" : "Camera";
            }
            string newString = s.Replace("<CAM>", cam);
            newString = newString.Replace(YEAR, DateTaken.Year.ToString("d4"));
            newString = newString.Replace(MONTH, DateTaken.Month.ToString("d2"));
            newString = newString.Replace("<MONTHNAME>", Toolbox.GetMonthName(DateTaken.Month));
            newString = newString.Replace("<MONTHPREV>", (DateTaken.Month - 1).ToString("d2"));
            newString = newString.Replace("<MONTHNAMEPREV>", Toolbox.GetMonthName((DateTaken.Month - 1)));
            newString = newString.Replace("<MONTHNEXT>", (DateTaken.Month + 1).ToString("d2"));
            newString = newString.Replace("<MONTHNAMENEXT>", Toolbox.GetMonthName((DateTaken.Month + 1)));
            newString = newString.Replace(DAY, DateTaken.Day.ToString("d2"));
            newString = newString.Replace(HOUR, DateTaken.Hour.ToString("d2"));
            newString = newString.Replace(MIN, DateTaken.Minute.ToString("d2"));
            newString = newString.Replace(SEC, DateTaken.Second.ToString("d2"));
            newString = newString.Replace("<NUM>", (counter).ToString("d5"));
            newString = newString.Replace("<-TAG>", Tag == string.Empty ? string.Empty : "-" + Tag);
            newString = newString.Replace("<_TAG>", Tag == string.Empty ? string.Empty : "_" + Tag);
            newString = newString.Replace("<TAG>", Tag);
            return newString;
        }

        public bool ConvertFileName2DateTaken(string structure)
        {
            Regex test = new Regex(Structure2Pattern(structure));

            //string patternTest = @"^IMG_\d{4}\d{2}\d{2}_\d{2}\d{2}\d{2}";

            if (test.IsMatch(FileName))
            {
                int year = Tag2Number(structure, YEAR, DateTaken.Year);
                int month = Tag2Number(structure, MONTH, DateTaken.Month);
                int day = Tag2Number(structure, DAY, DateTaken.Day);
                int hour = Tag2Number(structure, HOUR, DateTaken.Hour);
                int min = Tag2Number(structure, MIN, DateTaken.Minute);
                int sec = Tag2Number(structure, SEC, DateTaken.Second);
                DateTaken = new DateTime(year, month, day, hour, min, sec);
                ChangeDateTaken(DateTaken);
                return true;
            }
            return false;
        }
        public void ChangeDateTaken(DateTime newDateTaken)
        {
            try
            {

                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                using (Image theImage = Image.FromStream(fs, false, false))
                {
                    //PropertyItem[] propItems = theImage.PropertyItems;
                    Encoding _Encoding = Encoding.UTF8;
                    //var DataTakenProperty1 = propItems.Where(a => a.Id.ToString("x") == "9004").FirstOrDefault();
                    //var DataTakenProperty2 = propItems.Where(a => a.Id.ToString("x") == "9003").FirstOrDefault();
                    //string originalDateString = _Encoding.GetString(DataTakenProperty1.Value);
                    //originalDateString = originalDateString.Remove(originalDateString.Length - 1);
                    //DateTime originalDate = DateTime.ParseExact(originalDateString, "yyyy:MM:dd HH:mm:ss", null);

                    //originalDate = originalDate.AddHours(-7);

                    //PropertyItem DataTakenProperty1, DataTakenProperty2;
                    var DataTakenProperty1 = theImage.GetPropertyItem(0x9004);//36867); //PropertyTagExifDTOrig
                    DataTakenProperty1.Value = _Encoding.GetBytes(newDateTaken.ToString("yyyy:MM:dd HH:mm:ss") + '\0');
                    theImage.SetPropertyItem(DataTakenProperty1);
                    var DataTakenProperty2 = theImage.GetPropertyItem(0x9003);//36867); //PropertyTagExifDTOrig
                    DataTakenProperty2.Value = _Encoding.GetBytes(newDateTaken.ToString("yyyy:MM:dd HH:mm:ss") + '\0');
                    theImage.SetPropertyItem(DataTakenProperty2);
                    //string new_path = System.IO.Path.GetDirectoryName(path) + "\\_" + System.IO.Path.GetFileName(path);
                    theImage.Save(FilePath);
                }
            }
            catch
            {
                // Update the CreationTime, LastWriteTime and LastAccessTime.
                var fi = new FileInfo(FilePath);
                fi.CreationTime = fi.LastWriteTime = fi.LastAccessTime =
                        newDateTaken;
                fi = null;
            }
        }



        public int Tag2Number(string structure,string tag,int defaultNum)
        {
            int num = defaultNum;
            if (structure.Contains(tag))
            {
                string placeholders = Structure2Placeholder(structure);
                string placeholder = Tag2Placeholder(tag);
                string sub = FileName.Substring(placeholders.IndexOf(placeholder), placeholder.Length);
                num = Convert.ToInt16(sub);
            }
            return num;
        }

        public static string Tag2Placeholder(string tag)
        {
            switch (tag)
            {
                case YEAR:
                    return "YYYY";
                case MONTH:
                    return "MM";
                case DAY:
                    return "DD";
                case HOUR:
                    return "HH";
                case MIN:
                    return "mm";
                case SEC:
                    return "SS";
                default:
                    return "";
            }
        }
        public static string Structure2Placeholder(string structure)
        {
            string pattern = structure.Replace(YEAR, "YYYY");
            pattern = pattern.Replace(MONTH, "MM");
            pattern = pattern.Replace(DAY, "DD");
            pattern = pattern.Replace(HOUR, "HH");
            pattern = pattern.Replace(MIN, "mm");
            pattern = pattern.Replace(SEC, "SS");
            return pattern;
        }

        public static string Structure2Pattern(string structure)
        {
            string pattern = structure.Replace("<CAM>", @"\w*");
            pattern = pattern.Replace(YEAR, @"\d{4}");
            pattern = pattern.Replace(MONTH, @"\d{2}");
            pattern = pattern.Replace(DAY, @"\d{2}");
            pattern = pattern.Replace(HOUR, @"\d{2}");
            pattern = pattern.Replace(MIN, @"\d{2}");
            pattern = pattern.Replace(SEC, @"\d{2}");
            pattern = pattern.Replace("<NUM>", @"\d*");
            pattern = pattern.Replace("<-TAG>", @"(-\w*)|(\w*)");
            pattern = pattern.Replace("<_TAG>", @"(-\w*)|(\w*)");
            pattern = pattern.Replace("<TAG>", @"\w*");
            return pattern;
        }
    }
}
