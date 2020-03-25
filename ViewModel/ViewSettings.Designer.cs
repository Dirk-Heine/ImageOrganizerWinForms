namespace ImageOrganizerWinForms.ViewModel
{
    partial class ViewModelSettings
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
            this.label3 = new System.Windows.Forms.Label();
            this.DefaultFolderPathInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DefaultFolderPathOutput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DefaultNewNameFolder = new System.Windows.Forms.TextBox();
            this.DefaultNewNameFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SelectFolderInput = new System.Windows.Forms.Button();
            this.SelectFolderOutput = new System.Windows.Forms.Button();
            this.Ok = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Default folder path input";
            // 
            // DefaultFolderPathInput
            // 
            this.DefaultFolderPathInput.Location = new System.Drawing.Point(161, 12);
            this.DefaultFolderPathInput.Name = "DefaultFolderPathInput";
            this.DefaultFolderPathInput.Size = new System.Drawing.Size(218, 20);
            this.DefaultFolderPathInput.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Default folder path output";
            // 
            // DefaultFolderPathOutput
            // 
            this.DefaultFolderPathOutput.Location = new System.Drawing.Point(161, 38);
            this.DefaultFolderPathOutput.Name = "DefaultFolderPathOutput";
            this.DefaultFolderPathOutput.Size = new System.Drawing.Size(218, 20);
            this.DefaultFolderPathOutput.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Default new name for folders";
            // 
            // DefaultNewNameFolder
            // 
            this.DefaultNewNameFolder.Location = new System.Drawing.Point(161, 64);
            this.DefaultNewNameFolder.Name = "DefaultNewNameFolder";
            this.DefaultNewNameFolder.Size = new System.Drawing.Size(218, 20);
            this.DefaultNewNameFolder.TabIndex = 6;
            // 
            // DefaultNewNameFile
            // 
            this.DefaultNewNameFile.Location = new System.Drawing.Point(161, 90);
            this.DefaultNewNameFile.Name = "DefaultNewNameFile";
            this.DefaultNewNameFile.Size = new System.Drawing.Size(218, 20);
            this.DefaultNewNameFile.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Defaut new name for files";
            // 
            // SelectFolderInput
            // 
            this.SelectFolderInput.Location = new System.Drawing.Point(385, 12);
            this.SelectFolderInput.Name = "SelectFolderInput";
            this.SelectFolderInput.Size = new System.Drawing.Size(35, 23);
            this.SelectFolderInput.TabIndex = 8;
            this.SelectFolderInput.Text = "...";
            this.SelectFolderInput.UseVisualStyleBackColor = true;
            this.SelectFolderInput.Click += new System.EventHandler(this.SelectFolderInput_Click);
            // 
            // SelectFolderOutput
            // 
            this.SelectFolderOutput.Location = new System.Drawing.Point(385, 36);
            this.SelectFolderOutput.Name = "SelectFolderOutput";
            this.SelectFolderOutput.Size = new System.Drawing.Size(35, 23);
            this.SelectFolderOutput.TabIndex = 9;
            this.SelectFolderOutput.Text = "...";
            this.SelectFolderOutput.UseVisualStyleBackColor = true;
            this.SelectFolderOutput.Click += new System.EventHandler(this.SelectFolderOutput_Click);
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(17, 125);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(165, 23);
            this.Ok.TabIndex = 10;
            this.Ok.Text = "Ok";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(255, 125);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(165, 23);
            this.Cancel.TabIndex = 10;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // ViewModelSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 168);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.SelectFolderOutput);
            this.Controls.Add(this.SelectFolderInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DefaultNewNameFile);
            this.Controls.Add(this.DefaultNewNameFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DefaultFolderPathOutput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DefaultFolderPathInput);
            this.Name = "ViewModelSettings";
            this.Text = "ViewSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DefaultFolderPathInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DefaultFolderPathOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DefaultNewNameFolder;
        private System.Windows.Forms.TextBox DefaultNewNameFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button SelectFolderInput;
        private System.Windows.Forms.Button SelectFolderOutput;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Button Cancel;
    }
}