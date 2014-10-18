namespace Add7downloader
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxShowName = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSeason = new System.Windows.Forms.TextBox();
            this.textBoxEpisode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonFile = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonViewSubs = new System.Windows.Forms.Button();
            this.buttonBrowser = new System.Windows.Forms.Button();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.buttonDownloadSubs = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Show name:";
            // 
            // textBoxShowName
            // 
            this.textBoxShowName.Location = new System.Drawing.Point(80, 8);
            this.textBoxShowName.Name = "textBoxShowName";
            this.textBoxShowName.Size = new System.Drawing.Size(248, 20);
            this.textBoxShowName.TabIndex = 1;
            this.toolTip.SetToolTip(this.textBoxShowName, "Enter the name of a TV-show here.");
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonSearch.BackgroundImage")));
            this.buttonSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonSearch.Location = new System.Drawing.Point(336, 8);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(32, 32);
            this.buttonSearch.TabIndex = 2;
            this.toolTip.SetToolTip(this.buttonSearch, "Search subtitles on addic7ed.com.");
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.startSearch);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Season:";
            // 
            // textBoxSeason
            // 
            this.textBoxSeason.Location = new System.Drawing.Point(80, 40);
            this.textBoxSeason.Name = "textBoxSeason";
            this.textBoxSeason.Size = new System.Drawing.Size(40, 20);
            this.textBoxSeason.TabIndex = 4;
            this.toolTip.SetToolTip(this.textBoxSeason, "For better search results season# in 2-digit format, e.g. 04.");
            // 
            // textBoxEpisode
            // 
            this.textBoxEpisode.Location = new System.Drawing.Point(288, 40);
            this.textBoxEpisode.Name = "textBoxEpisode";
            this.textBoxEpisode.Size = new System.Drawing.Size(40, 20);
            this.textBoxEpisode.TabIndex = 5;
            this.toolTip.SetToolTip(this.textBoxEpisode, "For better search results enter episode# in 2-digit format, e.g. 06.");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(232, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Episode:";
            // 
            // buttonFile
            // 
            this.buttonFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonFile.BackgroundImage")));
            this.buttonFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonFile.Location = new System.Drawing.Point(336, 40);
            this.buttonFile.Name = "buttonFile";
            this.buttonFile.Size = new System.Drawing.Size(32, 32);
            this.buttonFile.TabIndex = 7;
            this.toolTip.SetToolTip(this.buttonFile, "Select an episode video file to search for matching subtitles.");
            this.buttonFile.UseVisualStyleBackColor = true;
            this.buttonFile.Click += new System.EventHandler(this.selectVideoFile);
            // 
            // listBox
            // 
            this.listBox.AllowDrop = true;
            this.listBox.FormattingEnabled = true;
            this.listBox.HorizontalScrollbar = true;
            this.listBox.Location = new System.Drawing.Point(8, 96);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(360, 173);
            this.listBox.TabIndex = 9;
            this.toolTip.SetToolTip(this.listBox, "Drag and drop an episode video file here to search for matching subtitles.");
            this.listBox.SelectedValueChanged += new System.EventHandler(this.listItemSelected);
            this.listBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.dropHandler);
            this.listBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.dragHandler);
            this.listBox.DoubleClick += new System.EventHandler(this.listDoubleClick);
            // 
            // buttonBack
            // 
            this.buttonBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonBack.BackgroundImage")));
            this.buttonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonBack.Location = new System.Drawing.Point(8, 272);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(32, 32);
            this.buttonBack.TabIndex = 10;
            this.toolTip.SetToolTip(this.buttonBack, "Return back to search results.");
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.back);
            // 
            // buttonViewSubs
            // 
            this.buttonViewSubs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonViewSubs.BackgroundImage")));
            this.buttonViewSubs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonViewSubs.Location = new System.Drawing.Point(40, 272);
            this.buttonViewSubs.Name = "buttonViewSubs";
            this.buttonViewSubs.Size = new System.Drawing.Size(32, 32);
            this.buttonViewSubs.TabIndex = 11;
            this.toolTip.SetToolTip(this.buttonViewSubs, "View available subs for the selected episode.");
            this.buttonViewSubs.UseVisualStyleBackColor = true;
            this.buttonViewSubs.Click += new System.EventHandler(this.getSubs);
            // 
            // buttonBrowser
            // 
            this.buttonBrowser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonBrowser.BackgroundImage")));
            this.buttonBrowser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonBrowser.Location = new System.Drawing.Point(72, 272);
            this.buttonBrowser.Name = "buttonBrowser";
            this.buttonBrowser.Size = new System.Drawing.Size(32, 32);
            this.buttonBrowser.TabIndex = 12;
            this.toolTip.SetToolTip(this.buttonBrowser, "Open the episode page in a default web-browser.");
            this.buttonBrowser.UseVisualStyleBackColor = true;
            this.buttonBrowser.Click += new System.EventHandler(this.openURL);
            // 
            // buttonAbout
            // 
            this.buttonAbout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonAbout.BackgroundImage")));
            this.buttonAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonAbout.Location = new System.Drawing.Point(336, 272);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(32, 32);
            this.buttonAbout.TabIndex = 13;
            this.toolTip.SetToolTip(this.buttonAbout, "About program.");
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.showAbout);
            // 
            // buttonDownloadSubs
            // 
            this.buttonDownloadSubs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonDownloadSubs.BackgroundImage")));
            this.buttonDownloadSubs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonDownloadSubs.Location = new System.Drawing.Point(40, 272);
            this.buttonDownloadSubs.Name = "buttonDownloadSubs";
            this.buttonDownloadSubs.Size = new System.Drawing.Size(32, 32);
            this.buttonDownloadSubs.TabIndex = 14;
            this.toolTip.SetToolTip(this.buttonDownloadSubs, "Download the selected subtitles.");
            this.buttonDownloadSubs.UseVisualStyleBackColor = true;
            this.buttonDownloadSubs.Click += new System.EventHandler(this.download);
            // 
            // labelStatus
            // 
            this.labelStatus.Location = new System.Drawing.Point(8, 72);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(360, 16);
            this.labelStatus.TabIndex = 8;
            // 
            // MainWindow
            // 
            this.AcceptButton = this.buttonSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 306);
            this.Controls.Add(this.buttonDownloadSubs);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.buttonBrowser);
            this.Controls.Add(this.buttonViewSubs);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxEpisode);
            this.Controls.Add(this.textBoxSeason);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxShowName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Addic7ed.com Sub Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxShowName;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSeason;
        private System.Windows.Forms.TextBox textBoxEpisode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonFile;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonViewSubs;
        private System.Windows.Forms.Button buttonBrowser;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.Button buttonDownloadSubs;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

