namespace ObjectBasedVideosAndImagesRetrieval {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.MainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.FirstLayout = new System.Windows.Forms.TableLayoutPanel();
            this.ObjectImage = new Emgu.CV.UI.ImageBox();
            this.SettingsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.ThreadingOptions = new System.Windows.Forms.GroupBox();
            this.MultiThreading = new System.Windows.Forms.RadioButton();
            this.MainThread = new System.Windows.Forms.RadioButton();
            this.MatchingOptions = new System.Windows.Forms.GroupBox();
            this.FlannMatching = new System.Windows.Forms.RadioButton();
            this.BruteForceMatching = new System.Windows.Forms.RadioButton();
            this.OtherOptions = new System.Windows.Forms.GroupBox();
            this.FilesNumber = new System.Windows.Forms.Label();
            this.GPU = new System.Windows.Forms.CheckBox();
            this.SecondLayout = new System.Windows.Forms.TableLayoutPanel();
            this.ObjectButton = new System.Windows.Forms.Button();
            this.FolderButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.MainLayout.SuspendLayout();
            this.FirstLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectImage)).BeginInit();
            this.SettingsLayout.SuspendLayout();
            this.ThreadingOptions.SuspendLayout();
            this.MatchingOptions.SuspendLayout();
            this.OtherOptions.SuspendLayout();
            this.SecondLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainLayout
            // 
            this.MainLayout.AutoSize = true;
            this.MainLayout.ColumnCount = 2;
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.MainLayout.Controls.Add(this.FirstLayout, 0, 0);
            this.MainLayout.Controls.Add(this.SecondLayout, 1, 0);
            this.MainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayout.Location = new System.Drawing.Point(0, 0);
            this.MainLayout.Name = "MainLayout";
            this.MainLayout.RowCount = 1;
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainLayout.Size = new System.Drawing.Size(1069, 517);
            this.MainLayout.TabIndex = 0;
            // 
            // FirstLayout
            // 
            this.FirstLayout.ColumnCount = 1;
            this.FirstLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.FirstLayout.Controls.Add(this.ObjectImage, 0, 0);
            this.FirstLayout.Controls.Add(this.SettingsLayout, 0, 1);
            this.FirstLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FirstLayout.Location = new System.Drawing.Point(3, 3);
            this.FirstLayout.Name = "FirstLayout";
            this.FirstLayout.RowCount = 2;
            this.FirstLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.FirstLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.FirstLayout.Size = new System.Drawing.Size(849, 511);
            this.FirstLayout.TabIndex = 0;
            // 
            // ObjectImage
            // 
            this.ObjectImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ObjectImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ObjectImage.Enabled = false;
            this.ObjectImage.Location = new System.Drawing.Point(3, 3);
            this.ObjectImage.Name = "ObjectImage";
            this.ObjectImage.Size = new System.Drawing.Size(843, 428);
            this.ObjectImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ObjectImage.TabIndex = 3;
            this.ObjectImage.TabStop = false;
            // 
            // SettingsLayout
            // 
            this.SettingsLayout.ColumnCount = 3;
            this.SettingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.SettingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.SettingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.SettingsLayout.Controls.Add(this.ThreadingOptions, 0, 0);
            this.SettingsLayout.Controls.Add(this.MatchingOptions, 0, 0);
            this.SettingsLayout.Controls.Add(this.OtherOptions, 2, 0);
            this.SettingsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsLayout.Location = new System.Drawing.Point(3, 437);
            this.SettingsLayout.Name = "SettingsLayout";
            this.SettingsLayout.RowCount = 1;
            this.SettingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SettingsLayout.Size = new System.Drawing.Size(843, 71);
            this.SettingsLayout.TabIndex = 0;
            // 
            // ThreadingOptions
            // 
            this.ThreadingOptions.Controls.Add(this.MultiThreading);
            this.ThreadingOptions.Controls.Add(this.MainThread);
            this.ThreadingOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ThreadingOptions.Location = new System.Drawing.Point(284, 3);
            this.ThreadingOptions.Name = "ThreadingOptions";
            this.ThreadingOptions.Size = new System.Drawing.Size(275, 65);
            this.ThreadingOptions.TabIndex = 5;
            this.ThreadingOptions.TabStop = false;
            this.ThreadingOptions.Text = "Threading Options";
            // 
            // MultiThreading
            // 
            this.MultiThreading.AutoSize = true;
            this.MultiThreading.Location = new System.Drawing.Point(8, 38);
            this.MultiThreading.Name = "MultiThreading";
            this.MultiThreading.Size = new System.Drawing.Size(95, 17);
            this.MultiThreading.TabIndex = 1;
            this.MultiThreading.TabStop = true;
            this.MultiThreading.Text = "MultiThreading";
            this.MultiThreading.UseVisualStyleBackColor = true;
            // 
            // MainThread
            // 
            this.MainThread.AutoSize = true;
            this.MainThread.Checked = true;
            this.MainThread.Location = new System.Drawing.Point(8, 15);
            this.MainThread.Name = "MainThread";
            this.MainThread.Size = new System.Drawing.Size(85, 17);
            this.MainThread.TabIndex = 0;
            this.MainThread.TabStop = true;
            this.MainThread.Text = "Main Thread";
            this.MainThread.UseVisualStyleBackColor = true;
            // 
            // MatchingOptions
            // 
            this.MatchingOptions.Controls.Add(this.FlannMatching);
            this.MatchingOptions.Controls.Add(this.BruteForceMatching);
            this.MatchingOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MatchingOptions.Location = new System.Drawing.Point(3, 3);
            this.MatchingOptions.Name = "MatchingOptions";
            this.MatchingOptions.Size = new System.Drawing.Size(275, 65);
            this.MatchingOptions.TabIndex = 4;
            this.MatchingOptions.TabStop = false;
            this.MatchingOptions.Text = "Matching Options";
            // 
            // FlannMatching
            // 
            this.FlannMatching.AutoSize = true;
            this.FlannMatching.Location = new System.Drawing.Point(9, 38);
            this.FlannMatching.Name = "FlannMatching";
            this.FlannMatching.Size = new System.Drawing.Size(51, 17);
            this.FlannMatching.TabIndex = 1;
            this.FlannMatching.TabStop = true;
            this.FlannMatching.Text = "Flann";
            this.FlannMatching.UseVisualStyleBackColor = true;
            // 
            // BruteForceMatching
            // 
            this.BruteForceMatching.AutoSize = true;
            this.BruteForceMatching.Checked = true;
            this.BruteForceMatching.Location = new System.Drawing.Point(9, 15);
            this.BruteForceMatching.Name = "BruteForceMatching";
            this.BruteForceMatching.Size = new System.Drawing.Size(80, 17);
            this.BruteForceMatching.TabIndex = 0;
            this.BruteForceMatching.TabStop = true;
            this.BruteForceMatching.Text = "Brute Force";
            this.BruteForceMatching.UseVisualStyleBackColor = true;
            // 
            // OtherOptions
            // 
            this.OtherOptions.Controls.Add(this.FilesNumber);
            this.OtherOptions.Controls.Add(this.GPU);
            this.OtherOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OtherOptions.Location = new System.Drawing.Point(565, 3);
            this.OtherOptions.Name = "OtherOptions";
            this.OtherOptions.Size = new System.Drawing.Size(275, 65);
            this.OtherOptions.TabIndex = 6;
            this.OtherOptions.TabStop = false;
            this.OtherOptions.Text = "Other Options";
            // 
            // FilesNumber
            // 
            this.FilesNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FilesNumber.AutoSize = true;
            this.FilesNumber.Location = new System.Drawing.Point(6, 39);
            this.FilesNumber.Name = "FilesNumber";
            this.FilesNumber.Size = new System.Drawing.Size(0, 13);
            this.FilesNumber.TabIndex = 5;
            // 
            // GPU
            // 
            this.GPU.AutoSize = true;
            this.GPU.Location = new System.Drawing.Point(6, 19);
            this.GPU.Name = "GPU";
            this.GPU.Size = new System.Drawing.Size(49, 17);
            this.GPU.TabIndex = 3;
            this.GPU.Text = "GPU";
            this.GPU.UseVisualStyleBackColor = true;
            // 
            // SecondLayout
            // 
            this.SecondLayout.ColumnCount = 1;
            this.SecondLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SecondLayout.Controls.Add(this.ObjectButton, 0, 0);
            this.SecondLayout.Controls.Add(this.FolderButton, 0, 1);
            this.SecondLayout.Controls.Add(this.SearchButton, 0, 2);
            this.SecondLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SecondLayout.Location = new System.Drawing.Point(858, 3);
            this.SecondLayout.Name = "SecondLayout";
            this.SecondLayout.RowCount = 3;
            this.SecondLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.SecondLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.SecondLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.SecondLayout.Size = new System.Drawing.Size(208, 511);
            this.SecondLayout.TabIndex = 1;
            // 
            // ObjectButton
            // 
            this.ObjectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectButton.AutoSize = true;
            this.ObjectButton.Location = new System.Drawing.Point(96, 3);
            this.ObjectButton.Name = "ObjectButton";
            this.ObjectButton.Size = new System.Drawing.Size(109, 23);
            this.ObjectButton.TabIndex = 0;
            this.ObjectButton.Text = "Open Object Image";
            this.ObjectButton.UseVisualStyleBackColor = true;
            this.ObjectButton.Click += new System.EventHandler(this.ObjectButton_Click);
            // 
            // FolderButton
            // 
            this.FolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FolderButton.AutoSize = true;
            this.FolderButton.Location = new System.Drawing.Point(93, 173);
            this.FolderButton.Name = "FolderButton";
            this.FolderButton.Size = new System.Drawing.Size(112, 23);
            this.FolderButton.TabIndex = 1;
            this.FolderButton.Text = "Open Search Folder";
            this.FolderButton.UseVisualStyleBackColor = true;
            this.FolderButton.Click += new System.EventHandler(this.FolderButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButton.AutoSize = true;
            this.SearchButton.Location = new System.Drawing.Point(130, 343);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 2;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 517);
            this.Controls.Add(this.MainLayout);
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.MainLayout.ResumeLayout(false);
            this.FirstLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ObjectImage)).EndInit();
            this.SettingsLayout.ResumeLayout(false);
            this.ThreadingOptions.ResumeLayout(false);
            this.ThreadingOptions.PerformLayout();
            this.MatchingOptions.ResumeLayout(false);
            this.MatchingOptions.PerformLayout();
            this.OtherOptions.ResumeLayout(false);
            this.OtherOptions.PerformLayout();
            this.SecondLayout.ResumeLayout(false);
            this.SecondLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainLayout;
        private System.Windows.Forms.TableLayoutPanel FirstLayout;
        private System.Windows.Forms.TableLayoutPanel SecondLayout;
        private System.Windows.Forms.TableLayoutPanel SettingsLayout;
        private Emgu.CV.UI.ImageBox ObjectImage;
        private System.Windows.Forms.Button ObjectButton;
        private System.Windows.Forms.Button FolderButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.GroupBox MatchingOptions;
        private System.Windows.Forms.RadioButton FlannMatching;
        private System.Windows.Forms.RadioButton BruteForceMatching;
        private System.Windows.Forms.GroupBox ThreadingOptions;
        private System.Windows.Forms.RadioButton MultiThreading;
        private System.Windows.Forms.RadioButton MainThread;
        private System.Windows.Forms.GroupBox OtherOptions;
        private System.Windows.Forms.CheckBox GPU;
        private System.Windows.Forms.Label FilesNumber;
    }
}

