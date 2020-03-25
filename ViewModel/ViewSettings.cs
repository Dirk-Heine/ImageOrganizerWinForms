using ImageOrganizerWinForms;
using ImageOrganizerWinForms.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageOrganizerWinForms.ViewModel
{
    public partial class ViewModelSettings : Form
    {
        public ViewModelSettings()
        {
            InitializeComponent();
            DefaultFolderPathInput.Text = ModelSettings.FolderPathInput.Replace(ModelSettings.ExeFilePath, "");
            DefaultFolderPathOutput.Text = ModelSettings.FolderPathOutput.Replace(ModelSettings.ExeFilePath, "");
            DefaultNewNameFolder.Text = ModelSettings.NewNameFolder.Replace(ModelSettings.ExeFilePath, "");
            DefaultNewNameFile.Text = ModelSettings.NewNameFile.Replace(ModelSettings.ExeFilePath, "");
        }
        private void SelectFolderInput_Click(object sender, EventArgs e)
        {
            _SelectInputFolder(null);
        }

        private void SelectFolderOutput_Click(object sender, EventArgs e)
        {
            _SelectOutputFolder(null);
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            _Yes(null);
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            _No(null);
        }

        #region Bindings

        private string _DefaultFolderPathInput
        {
            get { return DefaultFolderPathInput.Text.Replace(ModelSettings.ExeFilePath, ""); }
        }
        private string _DefaultFolderPathOutput
        {
            get { return DefaultFolderPathOutput.Text.Replace(ModelSettings.ExeFilePath, ""); }
        }
        private string _DefaultNewNameFolder
        {
            get { return DefaultNewNameFolder.Text; }
        }
        private string _DefaultNewNameFile
        {
            get { return DefaultNewNameFile.Text; }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Confirmational Dialog Result
        /// </summary>
        private void _Yes(object parameter)
        {
            ModelSettings.FolderPathOutput = _DefaultFolderPathOutput;
            ModelSettings.FolderPathInput = _DefaultFolderPathInput;
            ModelSettings.NewNameFolder = _DefaultNewNameFolder;
            ModelSettings.NewNameFile = _DefaultNewNameFile;

            // write xml
            ModelSettings.WriteSettings();

            // dialog feedback
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Negativ dialog result
        /// </summary>
        private void _No(object parameter)
        {
            // dialog feedback
            // dialog feedback
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void _SelectInputFolder(object parameter)
        {
            string path = Toolbox.OpenFolder(ModelSettings.ExeFilePath);
            if (!string.IsNullOrEmpty(path)) DefaultFolderPathInput.Text = path;
        }
        private void _SelectOutputFolder(object parameter)
        {
            string path = Toolbox.OpenFolder(ModelSettings.ExeFilePath);
            if (!string.IsNullOrEmpty(path)) DefaultFolderPathOutput.Text = path;
        }
        #endregion

    }
}
