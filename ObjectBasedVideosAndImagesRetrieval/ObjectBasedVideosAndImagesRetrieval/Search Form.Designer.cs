namespace ObjectBasedVideosAndImagesRetrieval {
    partial class SearchForm {
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
            this.ResultImage = new Emgu.CV.UI.ImageBox();
            this.VideoOptionsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.TrackingOptions = new System.Windows.Forms.GroupBox();
            this.Tracking = new System.Windows.Forms.CheckBox();
            this.VideoButton = new System.Windows.Forms.Button();
            this.SecondLayout = new System.Windows.Forms.TableLayoutPanel();
            this.ResultList = new System.Windows.Forms.ListView();
            this.ObjectImage = new Emgu.CV.UI.ImageBox();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.MainLayout.SuspendLayout();
            this.FirstLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultImage)).BeginInit();
            this.VideoOptionsLayout.SuspendLayout();
            this.TrackingOptions.SuspendLayout();
            this.SecondLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectImage)).BeginInit();
            this.SuspendLayout();
            // 
            // MainLayout
            // 
            this.MainLayout.AutoSize = true;
            this.MainLayout.ColumnCount = 2;
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.MainLayout.Controls.Add(this.FirstLayout, 0, 0);
            this.MainLayout.Controls.Add(this.SecondLayout, 1, 0);
            this.MainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayout.Location = new System.Drawing.Point(0, 0);
            this.MainLayout.Name = "MainLayout";
            this.MainLayout.RowCount = 1;
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainLayout.Size = new System.Drawing.Size(1060, 517);
            this.MainLayout.TabIndex = 0;
            // 
            // FirstLayout
            // 
            this.FirstLayout.ColumnCount = 1;
            this.FirstLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.FirstLayout.Controls.Add(this.ResultImage, 0, 0);
            this.FirstLayout.Controls.Add(this.VideoOptionsLayout, 0, 1);
            this.FirstLayout.Controls.Add(this.Progress, 0, 2);
            this.FirstLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FirstLayout.Location = new System.Drawing.Point(3, 3);
            this.FirstLayout.Name = "FirstLayout";
            this.FirstLayout.RowCount = 3;
            this.FirstLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.FirstLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.FirstLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.FirstLayout.Size = new System.Drawing.Size(789, 511);
            this.FirstLayout.TabIndex = 0;
            // 
            // ResultImage
            // 
            this.ResultImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResultImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultImage.Location = new System.Drawing.Point(3, 3);
            this.ResultImage.Name = "ResultImage";
            this.ResultImage.Size = new System.Drawing.Size(783, 428);
            this.ResultImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ResultImage.TabIndex = 2;
            this.ResultImage.TabStop = false;
            // 
            // VideoOptionsLayout
            // 
            this.VideoOptionsLayout.ColumnCount = 2;
            this.VideoOptionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.VideoOptionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.VideoOptionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.VideoOptionsLayout.Controls.Add(this.TrackingOptions, 1, 0);
            this.VideoOptionsLayout.Controls.Add(this.VideoButton, 0, 0);
            this.VideoOptionsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoOptionsLayout.Location = new System.Drawing.Point(3, 437);
            this.VideoOptionsLayout.Name = "VideoOptionsLayout";
            this.VideoOptionsLayout.RowCount = 1;
            this.VideoOptionsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.VideoOptionsLayout.Size = new System.Drawing.Size(783, 45);
            this.VideoOptionsLayout.TabIndex = 3;
            // 
            // TrackingOptions
            // 
            this.TrackingOptions.Controls.Add(this.Tracking);
            this.TrackingOptions.Location = new System.Drawing.Point(524, 3);
            this.TrackingOptions.Name = "TrackingOptions";
            this.TrackingOptions.Size = new System.Drawing.Size(151, 39);
            this.TrackingOptions.TabIndex = 0;
            this.TrackingOptions.TabStop = false;
            this.TrackingOptions.Text = "Tracking Options";
            // 
            // Tracking
            // 
            this.Tracking.AutoSize = true;
            this.Tracking.Checked = true;
            this.Tracking.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Tracking.Location = new System.Drawing.Point(6, 17);
            this.Tracking.Name = "Tracking";
            this.Tracking.Size = new System.Drawing.Size(68, 17);
            this.Tracking.TabIndex = 2;
            this.Tracking.Text = "Tracking";
            this.Tracking.UseVisualStyleBackColor = true;
            this.Tracking.CheckedChanged += new System.EventHandler(this.Tracking_CheckedChanged);
            // 
            // VideoButton
            // 
            this.VideoButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.VideoButton.AutoSize = true;
            this.VideoButton.Location = new System.Drawing.Point(223, 11);
            this.VideoButton.Name = "VideoButton";
            this.VideoButton.Size = new System.Drawing.Size(75, 23);
            this.VideoButton.TabIndex = 1;
            this.VideoButton.Text = "Pause";
            this.VideoButton.UseVisualStyleBackColor = true;
            this.VideoButton.Click += new System.EventHandler(this.VideoButton_Click);
            // 
            // SecondLayout
            // 
            this.SecondLayout.ColumnCount = 1;
            this.SecondLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SecondLayout.Controls.Add(this.ResultList, 0, 0);
            this.SecondLayout.Controls.Add(this.ObjectImage, 0, 1);
            this.SecondLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SecondLayout.Location = new System.Drawing.Point(798, 3);
            this.SecondLayout.Name = "SecondLayout";
            this.SecondLayout.RowCount = 2;
            this.SecondLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.SecondLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.SecondLayout.Size = new System.Drawing.Size(259, 511);
            this.SecondLayout.TabIndex = 1;
            // 
            // ResultList
            // 
            this.ResultList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultList.Location = new System.Drawing.Point(3, 3);
            this.ResultList.Name = "ResultList";
            this.ResultList.Size = new System.Drawing.Size(253, 351);
            this.ResultList.TabIndex = 0;
            this.ResultList.UseCompatibleStateImageBehavior = false;
            this.ResultList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ResultList_ItemSelectionChanged);
            // 
            // ObjectImage
            // 
            this.ObjectImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ObjectImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ObjectImage.Location = new System.Drawing.Point(3, 360);
            this.ObjectImage.Name = "ObjectImage";
            this.ObjectImage.Size = new System.Drawing.Size(253, 148);
            this.ObjectImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ObjectImage.TabIndex = 2;
            this.ObjectImage.TabStop = false;
            // 
            // Progress
            // 
            this.Progress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Progress.Location = new System.Drawing.Point(3, 488);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(783, 20);
            this.Progress.TabIndex = 4;
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 517);
            this.Controls.Add(this.MainLayout);
            this.Name = "SearchForm";
            this.Text = "Search Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SearchForm_FormClosed);
            this.MainLayout.ResumeLayout(false);
            this.FirstLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ResultImage)).EndInit();
            this.VideoOptionsLayout.ResumeLayout(false);
            this.VideoOptionsLayout.PerformLayout();
            this.TrackingOptions.ResumeLayout(false);
            this.TrackingOptions.PerformLayout();
            this.SecondLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ObjectImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainLayout;
        private System.Windows.Forms.TableLayoutPanel FirstLayout;
        private System.Windows.Forms.TableLayoutPanel SecondLayout;
        private Emgu.CV.UI.ImageBox ResultImage;
        private System.Windows.Forms.TableLayoutPanel VideoOptionsLayout;
        private System.Windows.Forms.GroupBox TrackingOptions;
        private System.Windows.Forms.CheckBox Tracking;
        private System.Windows.Forms.Button VideoButton;
        private System.Windows.Forms.ListView ResultList;
        private Emgu.CV.UI.ImageBox ObjectImage;
        private System.Windows.Forms.ProgressBar Progress;
    }
}