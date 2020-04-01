using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private string[] _SupportedImages = new string[] {
            ".jpg",
            ".png",
            ".jpeg"
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
            newString = newString.Replace("<YEAR>", DateTaken.Year.ToString("d4"));
            newString = newString.Replace("<MONTH>", DateTaken.Month.ToString("d2"));
            newString = newString.Replace("<MONTHNAME>", Toolbox.GetMonthName(DateTaken.Month));
            newString = newString.Replace("<MONTHPREV>", (DateTaken.Month - 1).ToString("d2"));
            newString = newString.Replace("<MONTHNAMEPREV>", Toolbox.GetMonthName((DateTaken.Month - 1)));
            newString = newString.Replace("<MONTHNEXT>", (DateTaken.Month + 1).ToString("d2"));
            newString = newString.Replace("<MONTHNAMENEXT>", Toolbox.GetMonthName((DateTaken.Month + 1)));
            newString = newString.Replace("<DAY>", DateTaken.Day.ToString("d2"));
            newString = newString.Replace("<HOUR>", DateTaken.Hour.ToString("d2"));
            newString = newString.Replace("<MIN>", DateTaken.Minute.ToString("d2"));
            newString = newString.Replace("<SEC>", DateTaken.Second.ToString("d2"));
            newString = newString.Replace("<NUM>", (counter).ToString("d5"));
            newString = newString.Replace("<-TAG>", Tag == string.Empty ? string.Empty : "-" + Tag);
            newString = newString.Replace("<_TAG>", Tag == string.Empty ? string.Empty : "_" + Tag);
            newString = newString.Replace("<TAG>", Tag);
            return newString;
        }
    }
}
