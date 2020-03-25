namespace ImageOrganizerWinForms.ViewModel
{
    partial class ViewModelMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FolderPathInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SelectFolderInput = new System.Windows.Forms.Button();
            this.FolderPathOutput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectFolderOutput = new System.Windows.Forms.Button();
            this.AnalyzeFolderInput = new System.Windows.Forms.Button();
            this.AnalyzeWithSubFolders = new System.Windows.Forms.CheckBox();
            this.NewNameFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.NewNameFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.OrganizeFolder = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CheckedJustRename = new System.Windows.Forms.RadioButton();
            this.CheckedMove = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CheckedCameraType = new System.Windows.Forms.RadioButton();
            this.CheckedCamera = new System.Windows.Forms.RadioButton();
            this.DeleteEmptyFolders = new System.Windows.Forms.CheckBox();
            this.UseTrashFolder = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.RenewFileNames = new System.Windows.Forms.Button();
            this.CancelWorker = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.OpenSettings = new System.Windows.Forms.Button();
            this.Logger = new System.Windows.Forms.ListBox();
            this.FilesInFolderDataGrid = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FilesInFolderDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // FolderPathInput
            // 
            this.FolderPathInput.Location = new System.Drawing.Point(95, 21);
            this.FolderPathInput.Name = "FolderPathInput";
            this.FolderPathInput.Size = new System.Drawing.Size(279, 20);
            this.FolderPathInput.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Folder input";
            // 
            // SelectFolderInput
            // 
            this.SelectFolderInput.Location = new System.Drawing.Point(381, 19);
            this.SelectFolderInput.Name = "SelectFolderInput";
            this.SelectFolderInput.Size = new System.Drawing.Size(35, 23);
            this.SelectFolderInput.TabIndex = 2;
            this.SelectFolderInput.Text = "...";
            this.SelectFolderInput.UseVisualStyleBackColor = true;
            this.SelectFolderInput.Click += new System.EventHandler(this.SelectFolderInput_Click);
            // 
            // FolderPathOutput
            // 
            this.FolderPathOutput.Location = new System.Drawing.Point(95, 50);
            this.FolderPathOutput.Name = "FolderPathOutput";
            this.FolderPathOutput.Size = new System.Drawing.Size(279, 20);
            this.FolderPathOutput.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Folder output";
            // 
            // SelectFolderOutput
            // 
            this.SelectFolderOutput.Location = new System.Drawing.Point(381, 48);
            this.SelectFolderOutput.Name = "SelectFolderOutput";
            this.SelectFolderOutput.Size = new System.Drawing.Size(35, 23);
            this.SelectFolderOutput.TabIndex = 2;
            this.SelectFolderOutput.Text = "...";
            this.SelectFolderOutput.UseVisualStyleBackColor = true;
            this.SelectFolderOutput.Click += new System.EventHandler(this.SelectFolderOutput_Click);
            // 
            // AnalyzeFolderInput
            // 
            this.AnalyzeFolderInput.Location = new System.Drawing.Point(9, 15);
            this.AnalyzeFolderInput.Name = "AnalyzeFolderInput";
            this.AnalyzeFolderInput.Size = new System.Drawing.Size(147, 23);
            this.AnalyzeFolderInput.TabIndex = 3;
            this.AnalyzeFolderInput.Text = "Analyze folder input";
            this.AnalyzeFolderInput.UseVisualStyleBackColor = true;
            this.AnalyzeFolderInput.Click += new System.EventHandler(this.AnalyzeFolderInput_Click);
            // 
            // AnalyzeWithSubFolders
            // 
            this.AnalyzeWithSubFolders.AutoSize = true;
            this.AnalyzeWithSubFolders.Location = new System.Drawing.Point(172, 19);
            this.AnalyzeWithSubFolders.Name = "AnalyzeWithSubFolders";
            this.AnalyzeWithSubFolders.Size = new System.Drawing.Size(76, 17);
            this.AnalyzeWithSubFolders.TabIndex = 4;
            this.AnalyzeWithSubFolders.Text = "Subfolders";
            this.AnalyzeWithSubFolders.UseVisualStyleBackColor = true;
            // 
            // NewNameFolder
            // 
            this.NewNameFolder.Location = new System.Drawing.Point(108, 76);
            this.NewNameFolder.Name = "NewNameFolder";
            this.NewNameFolder.Size = new System.Drawing.Size(308, 20);
            this.NewNameFolder.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "New folder name";
            // 
            // NewNameFile
            // 
            this.NewNameFile.Location = new System.Drawing.Point(108, 102);
            this.NewNameFile.Name = "NewNameFile";
            this.NewNameFile.Size = new System.Drawing.Size(308, 20);
            this.NewNameFile.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "New file name";
            // 
            // OrganizeFolder
            // 
            this.OrganizeFolder.Location = new System.Drawing.Point(9, 52);
            this.OrganizeFolder.Name = "OrganizeFolder";
            this.OrganizeFolder.Size = new System.Drawing.Size(147, 23);
            this.OrganizeFolder.TabIndex = 3;
            this.OrganizeFolder.Text = "Organize files";
            this.OrganizeFolder.UseVisualStyleBackColor = true;
            this.OrganizeFolder.Click += new System.EventHandler(this.OrganizeFiles_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CheckedJustRename);
            this.groupBox1.Controls.Add(this.CheckedMove);
            this.groupBox1.Location = new System.Drawing.Point(176, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 31);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // CheckedJustRename
            // 
            this.CheckedJustRename.AutoSize = true;
            this.CheckedJustRename.Location = new System.Drawing.Point(86, 6);
            this.CheckedJustRename.Name = "CheckedJustRename";
            this.CheckedJustRename.Size = new System.Drawing.Size(103, 17);
            this.CheckedJustRename.TabIndex = 0;
            this.CheckedJustRename.Text = "Just rename files";
            this.CheckedJustRename.UseVisualStyleBackColor = true;
            // 
            // CheckedMove
            // 
            this.CheckedMove.AutoSize = true;
            this.CheckedMove.Checked = true;
            this.CheckedMove.Location = new System.Drawing.Point(7, 5);
            this.CheckedMove.Name = "CheckedMove";
            this.CheckedMove.Size = new System.Drawing.Size(73, 17);
            this.CheckedMove.TabIndex = 0;
            this.CheckedMove.TabStop = true;
            this.CheckedMove.Text = "Move files";
            this.CheckedMove.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CheckedCameraType);
            this.groupBox2.Controls.Add(this.CheckedCamera);
            this.groupBox2.Location = new System.Drawing.Point(176, 53);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(252, 31);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // CheckedCameraType
            // 
            this.CheckedCameraType.AutoSize = true;
            this.CheckedCameraType.Location = new System.Drawing.Point(143, 5);
            this.CheckedCameraType.Name = "CheckedCameraType";
            this.CheckedCameraType.Size = new System.Drawing.Size(84, 17);
            this.CheckedCameraType.TabIndex = 0;
            this.CheckedCameraType.Text = "Camera type";
            this.CheckedCameraType.UseVisualStyleBackColor = true;
            // 
            // CheckedCamera
            // 
            this.CheckedCamera.AutoSize = true;
            this.CheckedCamera.Checked = true;
            this.CheckedCamera.Location = new System.Drawing.Point(7, 5);
            this.CheckedCamera.Name = "CheckedCamera";
            this.CheckedCamera.Size = new System.Drawing.Size(130, 17);
            this.CheckedCamera.TabIndex = 0;
            this.CheckedCamera.TabStop = true;
            this.CheckedCamera.Text = "With / without camera";
            this.CheckedCamera.UseVisualStyleBackColor = true;
            // 
            // DeleteEmptyFolders
            // 
            this.DeleteEmptyFolders.AutoSize = true;
            this.DeleteEmptyFolders.Checked = true;
            this.DeleteEmptyFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DeleteEmptyFolders.Location = new System.Drawing.Point(183, 91);
            this.DeleteEmptyFolders.Name = "DeleteEmptyFolders";
            this.DeleteEmptyFolders.Size = new System.Drawing.Size(122, 17);
            this.DeleteEmptyFolders.TabIndex = 6;
            this.DeleteEmptyFolders.Text = "Delete empty folders";
            this.DeleteEmptyFolders.UseVisualStyleBackColor = true;
            // 
            // UseTrashFolder
            // 
            this.UseTrashFolder.AutoSize = true;
            this.UseTrashFolder.Checked = true;
            this.UseTrashFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UseTrashFolder.Location = new System.Drawing.Point(183, 114);
            this.UseTrashFolder.Name = "UseTrashFolder";
            this.UseTrashFolder.Size = new System.Drawing.Size(256, 17);
            this.UseTrashFolder.TabIndex = 6;
            this.UseTrashFolder.Text = "Use trash folder instead of deleting dublicate files";
            this.UseTrashFolder.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Progress);
            this.groupBox3.Controls.Add(this.UseTrashFolder);
            this.groupBox3.Controls.Add(this.RenewFileNames);
            this.groupBox3.Controls.Add(this.CancelWorker);
            this.groupBox3.Controls.Add(this.OrganizeFolder);
            this.groupBox3.Controls.Add(this.DeleteEmptyFolders);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Location = new System.Drawing.Point(422, 65);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(451, 140);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Organizing";
            // 
            // Progress
            // 
            this.Progress.Location = new System.Drawing.Point(9, 108);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(147, 23);
            this.Progress.TabIndex = 7;
            this.Progress.Visible = false;
            // 
            // RenewFileNames
            // 
            this.RenewFileNames.Location = new System.Drawing.Point(9, 18);
            this.RenewFileNames.Name = "RenewFileNames";
            this.RenewFileNames.Size = new System.Drawing.Size(147, 23);
            this.RenewFileNames.TabIndex = 3;
            this.RenewFileNames.Text = "Renew file names";
            this.RenewFileNames.UseVisualStyleBackColor = true;
            this.RenewFileNames.Click += new System.EventHandler(this.RenewFileNames_Click);
            // 
            // CancelWorker
            // 
            this.CancelWorker.Location = new System.Drawing.Point(9, 81);
            this.CancelWorker.Name = "CancelWorker";
            this.CancelWorker.Size = new System.Drawing.Size(147, 23);
            this.CancelWorker.TabIndex = 3;
            this.CancelWorker.Text = "Cancel";
            this.CancelWorker.UseVisualStyleBackColor = true;
            this.CancelWorker.Visible = false;
            this.CancelWorker.Click += new System.EventHandler(this.CancelWorker_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.AnalyzeFolderInput);
            this.groupBox4.Controls.Add(this.AnalyzeWithSubFolders);
            this.groupBox4.Location = new System.Drawing.Point(422, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(287, 49);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Analyzing";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Possible TAGS:",
            "<CAM> => Camera type",
            "<NUM> => Counting number",
            "<YEAR> => Year taken / Year last modified",
            "<MONTHNAME> => Month taken as german name (e.g. April)",
            "<MONTH>",
            "<DAY>",
            "<HOUR>",
            "<MIN>",
            "<SEC>"});
            this.listBox1.Location = new System.Drawing.Point(18, 128);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(398, 82);
            this.listBox1.TabIndex = 9;
            // 
            // OpenSettings
            // 
            this.OpenSettings.Location = new System.Drawing.Point(726, 19);
            this.OpenSettings.Name = "OpenSettings";
            this.OpenSettings.Size = new System.Drawing.Size(147, 23);
            this.OpenSettings.TabIndex = 3;
            this.OpenSettings.Text = "Save settings";
            this.OpenSettings.UseVisualStyleBackColor = true;
            this.OpenSettings.Click += new System.EventHandler(this.SaveSettings_Click);
            // 
            // Logger
            // 
            this.Logger.FormattingEnabled = true;
            this.Logger.Location = new System.Drawing.Point(18, 216);
            this.Logger.Name = "Logger";
            this.Logger.Size = new System.Drawing.Size(855, 290);
            this.Logger.TabIndex = 10;
            // 
            // FilesInFolderDataGrid
            // 
            this.FilesInFolderDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FilesInFolderDataGrid.Location = new System.Drawing.Point(18, 382);
            this.FilesInFolderDataGrid.Name = "FilesInFolderDataGrid";
            this.FilesInFolderDataGrid.Size = new System.Drawing.Size(855, 129);
            this.FilesInFolderDataGrid.TabIndex = 11;
            this.FilesInFolderDataGrid.Visible = false;
            // 
            // ViewModelMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 524);
            this.Controls.Add(this.FilesInFolderDataGrid);
            this.Controls.Add(this.Logger);
            this.Controls.Add(this.OpenSettings);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.SelectFolderOutput);
            this.Controls.Add(this.SelectFolderInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NewNameFile);
            this.Controls.Add(this.NewNameFolder);
            this.Controls.Add(this.FolderPathOutput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FolderPathInput);
            this.Name = "ViewModelMain";
            this.Text = "Image organizer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FilesInFolderDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FolderPathInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SelectFolderInput;
        private System.Windows.Forms.TextBox FolderPathOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SelectFolderOutput;
        private System.Windows.Forms.Button AnalyzeFolderInput;
        private System.Windows.Forms.CheckBox AnalyzeWithSubFolders;
        private System.Windows.Forms.TextBox NewNameFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox NewNameFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button OrganizeFolder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton CheckedJustRename;
        private System.Windows.Forms.RadioButton CheckedMove;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton CheckedCameraType;
        private System.Windows.Forms.RadioButton CheckedCamera;
        private System.Windows.Forms.CheckBox DeleteEmptyFolders;
        private System.Windows.Forms.CheckBox UseTrashFolder;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button OpenSettings;
        private System.Windows.Forms.Button RenewFileNames;
        private System.Windows.Forms.ListBox Logger;
        private System.Windows.Forms.ProgressBar Progress;
        private System.Windows.Forms.Button CancelWorker;
        private System.Windows.Forms.DataGridView FilesInFolderDataGrid;
    }
}

