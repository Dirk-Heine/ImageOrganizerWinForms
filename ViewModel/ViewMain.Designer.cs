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
            this.MoveOtherFiles = new System.Windows.Forms.CheckBox();
            this.TimeLeft = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.FilesToMove = new System.Windows.Forms.NumericUpDown();
            this.FilesMoved = new System.Windows.Forms.NumericUpDown();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.CancelWorker = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.OldNameFile = new System.Windows.Forms.TextBox();
            this.OldNameFolder = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.OpenSettings = new System.Windows.Forms.Button();
            this.Logger = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FilesToMove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilesMoved)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // FolderPathInput
            // 
            this.FolderPathInput.Location = new System.Drawing.Point(127, 26);
            this.FolderPathInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FolderPathInput.Name = "FolderPathInput";
            this.FolderPathInput.Size = new System.Drawing.Size(371, 22);
            this.FolderPathInput.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Folder input";
            // 
            // SelectFolderInput
            // 
            this.SelectFolderInput.Location = new System.Drawing.Point(508, 23);
            this.SelectFolderInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SelectFolderInput.Name = "SelectFolderInput";
            this.SelectFolderInput.Size = new System.Drawing.Size(47, 28);
            this.SelectFolderInput.TabIndex = 2;
            this.SelectFolderInput.Text = "...";
            this.SelectFolderInput.UseVisualStyleBackColor = true;
            this.SelectFolderInput.Click += new System.EventHandler(this.SelectFolderInput_Click);
            // 
            // FolderPathOutput
            // 
            this.FolderPathOutput.Location = new System.Drawing.Point(127, 62);
            this.FolderPathOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FolderPathOutput.Name = "FolderPathOutput";
            this.FolderPathOutput.Size = new System.Drawing.Size(371, 22);
            this.FolderPathOutput.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Folder output";
            // 
            // SelectFolderOutput
            // 
            this.SelectFolderOutput.Location = new System.Drawing.Point(508, 59);
            this.SelectFolderOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SelectFolderOutput.Name = "SelectFolderOutput";
            this.SelectFolderOutput.Size = new System.Drawing.Size(47, 28);
            this.SelectFolderOutput.TabIndex = 4;
            this.SelectFolderOutput.Text = "...";
            this.SelectFolderOutput.UseVisualStyleBackColor = true;
            this.SelectFolderOutput.Click += new System.EventHandler(this.SelectFolderOutput_Click);
            // 
            // AnalyzeFolderInput
            // 
            this.AnalyzeFolderInput.Location = new System.Drawing.Point(12, 18);
            this.AnalyzeFolderInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AnalyzeFolderInput.Name = "AnalyzeFolderInput";
            this.AnalyzeFolderInput.Size = new System.Drawing.Size(196, 28);
            this.AnalyzeFolderInput.TabIndex = 7;
            this.AnalyzeFolderInput.Text = "Analyze folder input";
            this.AnalyzeFolderInput.UseVisualStyleBackColor = true;
            this.AnalyzeFolderInput.Click += new System.EventHandler(this.AnalyzeFolderInput_Click);
            // 
            // AnalyzeWithSubFolders
            // 
            this.AnalyzeWithSubFolders.AutoSize = true;
            this.AnalyzeWithSubFolders.Location = new System.Drawing.Point(229, 23);
            this.AnalyzeWithSubFolders.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AnalyzeWithSubFolders.Name = "AnalyzeWithSubFolders";
            this.AnalyzeWithSubFolders.Size = new System.Drawing.Size(98, 21);
            this.AnalyzeWithSubFolders.TabIndex = 8;
            this.AnalyzeWithSubFolders.Text = "Subfolders";
            this.AnalyzeWithSubFolders.UseVisualStyleBackColor = true;
            // 
            // NewNameFolder
            // 
            this.NewNameFolder.Location = new System.Drawing.Point(144, 94);
            this.NewNameFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.NewNameFolder.Name = "NewNameFolder";
            this.NewNameFolder.Size = new System.Drawing.Size(409, 22);
            this.NewNameFolder.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 97);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "New folder name";
            // 
            // NewNameFile
            // 
            this.NewNameFile.Location = new System.Drawing.Point(144, 126);
            this.NewNameFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.NewNameFile.Name = "NewNameFile";
            this.NewNameFile.Size = new System.Drawing.Size(409, 22);
            this.NewNameFile.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 129);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "New file name";
            // 
            // OrganizeFolder
            // 
            this.OrganizeFolder.Location = new System.Drawing.Point(15, 26);
            this.OrganizeFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OrganizeFolder.Name = "OrganizeFolder";
            this.OrganizeFolder.Size = new System.Drawing.Size(196, 28);
            this.OrganizeFolder.TabIndex = 1;
            this.OrganizeFolder.Text = "Organize files";
            this.OrganizeFolder.UseVisualStyleBackColor = true;
            this.OrganizeFolder.Click += new System.EventHandler(this.OrganizeFiles_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CheckedJustRename);
            this.groupBox1.Controls.Add(this.CheckedMove);
            this.groupBox1.Location = new System.Drawing.Point(235, 20);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(336, 38);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // CheckedJustRename
            // 
            this.CheckedJustRename.AutoSize = true;
            this.CheckedJustRename.Location = new System.Drawing.Point(115, 10);
            this.CheckedJustRename.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CheckedJustRename.Name = "CheckedJustRename";
            this.CheckedJustRename.Size = new System.Drawing.Size(136, 21);
            this.CheckedJustRename.TabIndex = 13;
            this.CheckedJustRename.Text = "Just rename files";
            this.CheckedJustRename.UseVisualStyleBackColor = true;
            // 
            // CheckedMove
            // 
            this.CheckedMove.AutoSize = true;
            this.CheckedMove.Checked = true;
            this.CheckedMove.Location = new System.Drawing.Point(9, 10);
            this.CheckedMove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CheckedMove.Name = "CheckedMove";
            this.CheckedMove.Size = new System.Drawing.Size(92, 21);
            this.CheckedMove.TabIndex = 12;
            this.CheckedMove.TabStop = true;
            this.CheckedMove.Text = "Move files";
            this.CheckedMove.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CheckedCameraType);
            this.groupBox2.Controls.Add(this.CheckedCamera);
            this.groupBox2.Location = new System.Drawing.Point(235, 65);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(336, 38);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // CheckedCameraType
            // 
            this.CheckedCameraType.AutoSize = true;
            this.CheckedCameraType.Location = new System.Drawing.Point(191, 11);
            this.CheckedCameraType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CheckedCameraType.Name = "CheckedCameraType";
            this.CheckedCameraType.Size = new System.Drawing.Size(109, 21);
            this.CheckedCameraType.TabIndex = 15;
            this.CheckedCameraType.Text = "Camera type";
            this.CheckedCameraType.UseVisualStyleBackColor = true;
            // 
            // CheckedCamera
            // 
            this.CheckedCamera.AutoSize = true;
            this.CheckedCamera.Checked = true;
            this.CheckedCamera.Location = new System.Drawing.Point(9, 11);
            this.CheckedCamera.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CheckedCamera.Name = "CheckedCamera";
            this.CheckedCamera.Size = new System.Drawing.Size(164, 21);
            this.CheckedCamera.TabIndex = 14;
            this.CheckedCamera.TabStop = true;
            this.CheckedCamera.Text = "With / without camera";
            this.CheckedCamera.UseVisualStyleBackColor = true;
            // 
            // DeleteEmptyFolders
            // 
            this.DeleteEmptyFolders.AutoSize = true;
            this.DeleteEmptyFolders.Checked = true;
            this.DeleteEmptyFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DeleteEmptyFolders.Location = new System.Drawing.Point(244, 112);
            this.DeleteEmptyFolders.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DeleteEmptyFolders.Name = "DeleteEmptyFolders";
            this.DeleteEmptyFolders.Size = new System.Drawing.Size(160, 21);
            this.DeleteEmptyFolders.TabIndex = 16;
            this.DeleteEmptyFolders.Text = "Delete empty folders";
            this.DeleteEmptyFolders.UseVisualStyleBackColor = true;
            // 
            // UseTrashFolder
            // 
            this.UseTrashFolder.AutoSize = true;
            this.UseTrashFolder.Checked = true;
            this.UseTrashFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UseTrashFolder.Location = new System.Drawing.Point(244, 140);
            this.UseTrashFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.UseTrashFolder.Name = "UseTrashFolder";
            this.UseTrashFolder.Size = new System.Drawing.Size(341, 21);
            this.UseTrashFolder.TabIndex = 17;
            this.UseTrashFolder.Text = "Use trash folder instead of deleting dublicate files";
            this.UseTrashFolder.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.MoveOtherFiles);
            this.groupBox3.Controls.Add(this.TimeLeft);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.FilesToMove);
            this.groupBox3.Controls.Add(this.FilesMoved);
            this.groupBox3.Controls.Add(this.UseTrashFolder);
            this.groupBox3.Controls.Add(this.OrganizeFolder);
            this.groupBox3.Controls.Add(this.DeleteEmptyFolders);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Location = new System.Drawing.Point(575, 158);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(601, 213);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Organizing";
            // 
            // MoveOtherFiles
            // 
            this.MoveOtherFiles.AutoSize = true;
            this.MoveOtherFiles.Checked = true;
            this.MoveOtherFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MoveOtherFiles.Location = new System.Drawing.Point(244, 169);
            this.MoveOtherFiles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MoveOtherFiles.Name = "MoveOtherFiles";
            this.MoveOtherFiles.Size = new System.Drawing.Size(160, 21);
            this.MoveOtherFiles.TabIndex = 21;
            this.MoveOtherFiles.Text = "Move also other files";
            this.MoveOtherFiles.UseVisualStyleBackColor = true;
            // 
            // TimeLeft
            // 
            this.TimeLeft.AutoSize = true;
            this.TimeLeft.Location = new System.Drawing.Point(11, 81);
            this.TimeLeft.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TimeLeft.Name = "TimeLeft";
            this.TimeLeft.Size = new System.Drawing.Size(39, 17);
            this.TimeLeft.TabIndex = 20;
            this.TimeLeft.Text = "Time";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(119, 112);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 17);
            this.label8.TabIndex = 19;
            this.label8.Text = "Files moved";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 112);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 17);
            this.label7.TabIndex = 18;
            this.label7.Text = "Files to move";
            // 
            // FilesToMove
            // 
            this.FilesToMove.Enabled = false;
            this.FilesToMove.Location = new System.Drawing.Point(12, 137);
            this.FilesToMove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FilesToMove.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.FilesToMove.Name = "FilesToMove";
            this.FilesToMove.Size = new System.Drawing.Size(93, 22);
            this.FilesToMove.TabIndex = 6;
            // 
            // FilesMoved
            // 
            this.FilesMoved.Enabled = false;
            this.FilesMoved.Location = new System.Drawing.Point(117, 137);
            this.FilesMoved.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FilesMoved.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.FilesMoved.Name = "FilesMoved";
            this.FilesMoved.Size = new System.Drawing.Size(93, 22);
            this.FilesMoved.TabIndex = 6;
            // 
            // Progress
            // 
            this.Progress.Location = new System.Drawing.Point(164, 300);
            this.Progress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(252, 28);
            this.Progress.TabIndex = 7;
            this.Progress.Visible = false;
            // 
            // CancelWorker
            // 
            this.CancelWorker.Location = new System.Drawing.Point(428, 300);
            this.CancelWorker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CancelWorker.Name = "CancelWorker";
            this.CancelWorker.Size = new System.Drawing.Size(127, 28);
            this.CancelWorker.TabIndex = 19;
            this.CancelWorker.Text = "Cancel";
            this.CancelWorker.UseVisualStyleBackColor = true;
            this.CancelWorker.Visible = false;
            this.CancelWorker.Click += new System.EventHandler(this.CancelWorker_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.AnalyzeFolderInput);
            this.groupBox4.Controls.Add(this.AnalyzeWithSubFolders);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.OldNameFile);
            this.groupBox4.Controls.Add(this.OldNameFolder);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(577, 15);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(599, 130);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Analyzing";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 57);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Old folder name";
            // 
            // OldNameFile
            // 
            this.OldNameFile.Location = new System.Drawing.Point(144, 85);
            this.OldNameFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OldNameFile.Name = "OldNameFile";
            this.OldNameFile.Size = new System.Drawing.Size(409, 22);
            this.OldNameFile.TabIndex = 10;
            // 
            // OldNameFolder
            // 
            this.OldNameFolder.Location = new System.Drawing.Point(144, 53);
            this.OldNameFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OldNameFolder.Name = "OldNameFolder";
            this.OldNameFolder.Size = new System.Drawing.Size(409, 22);
            this.OldNameFolder.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 89);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "Org file name";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Items.AddRange(new object[] {
            "Possible TAGS:",
            "<TAG> => Placeholder (append at end to preserve old folder names)",
            "<CAM> => Camera type",
            "<NUM> => Counting number",
            "<YEAR> => Year taken / Year last modified",
            "<MONTHNAME> => Month taken as german name (e.g. April)",
            "<MONTH>",
            "<DAY>",
            "<HOUR>",
            "<MIN>",
            "<SEC>"});
            this.listBox1.Location = new System.Drawing.Point(24, 158);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(529, 132);
            this.listBox1.TabIndex = 100;
            // 
            // OpenSettings
            // 
            this.OpenSettings.Location = new System.Drawing.Point(24, 300);
            this.OpenSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OpenSettings.Name = "OpenSettings";
            this.OpenSettings.Size = new System.Drawing.Size(127, 28);
            this.OpenSettings.TabIndex = 18;
            this.OpenSettings.Text = "Save settings";
            this.OpenSettings.UseVisualStyleBackColor = true;
            this.OpenSettings.Click += new System.EventHandler(this.SaveSettings_Click);
            // 
            // Logger
            // 
            this.Logger.FormattingEnabled = true;
            this.Logger.ItemHeight = 16;
            this.Logger.Location = new System.Drawing.Point(24, 378);
            this.Logger.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Logger.Name = "Logger";
            this.Logger.Size = new System.Drawing.Size(1139, 244);
            this.Logger.TabIndex = 101;
            // 
            // ViewModelMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 645);
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.Logger);
            this.Controls.Add(this.OpenSettings);
            this.Controls.Add(this.CancelWorker);
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
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ViewModelMain";
            this.Text = "Image organizer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FilesToMove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilesMoved)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
        private System.Windows.Forms.ListBox Logger;
        private System.Windows.Forms.ProgressBar Progress;
        private System.Windows.Forms.Button CancelWorker;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox OldNameFolder;
        private System.Windows.Forms.TextBox OldNameFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown FilesToMove;
        private System.Windows.Forms.NumericUpDown FilesMoved;
        private System.Windows.Forms.Label TimeLeft;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox MoveOtherFiles;
    }
}

