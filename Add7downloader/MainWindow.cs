﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Add7downloader
{
    public partial class MainWindow : Form
    {
        string videoFile;        
        string showname;
        string season;
        string episode;
        string release;
        List<string[]> episodeList;
        List<string[]> subList;
        int epPosition;
        string fileName;        

        public MainWindow()
        {
            InitializeComponent();
            epListButtons();
            buttonChanger();            
            videoFile = "";            
        }

        void epListButtons(bool found = false)
        {
            buttonBack.Visible = false;
            buttonDownloadSubs.Visible = false;
            buttonViewSubs.Visible = found;
            buttonBrowser.Visible = found;            
        }

        void subListButtons(bool found = false)
        {
            buttonViewSubs.Visible = false;
            buttonBack.Visible = true;
            buttonDownloadSubs.Visible = found;
            buttonBrowser.Visible = found;
        }

        void buttonChanger()
        {
            int position = listBox.SelectedIndex;
            bool isSelected = position >= 0;
            string label;
            try
            {
                label = labelStatus.Text.Substring(0, 4);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                label = "Epis";
            }
            if (label == "Epis")
            {
                buttonViewSubs.Enabled = isSelected;
                buttonBrowser.Enabled = isSelected;
            }
            else if (label == "Subs")
            {
                buttonDownloadSubs.Enabled = isSelected;
            }
        }       

        void search(string show, string season, string episode, bool fromFile = false)
        {            
            this.showname = show;
            this.season = season;
            this.episode = episode;
            labelStatus.Text = "Searching...";
            Application.DoEvents();
            listBox.Items.Clear();
            if (!fromFile)
            {
                videoFile = "";
                release = "";
            }
            Cursor.Current = Cursors.WaitCursor;
            episodeList = Addic7ed.SearchEpisode(this.showname, this.season, this.episode);
            Cursor.Current = Cursors.Default;
            if (episodeList != null && episodeList.Count > 0)
            {
                for (int i = 0; i < episodeList.Count; i++)
                {
                    listBox.Items.Add(episodeList[i][1]);
                }
                labelStatus.Text = String.Format("Episodes found for {0} - {1}x{2}:", this.showname, this.season, this.episode);
                epListButtons(true);
            }
            else
            {
                if (episodeList == null)
                {
                    MessageBox.Show(this, "Unable to connect to addic7ed.com.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    labelStatus.Text = "Nothing found!";
                }
                epListButtons(false);
            }
            buttonChanger();
        }

        private void listItemSelected(object sender, EventArgs e)
        {
            buttonChanger();
        }

        private void startSearch(object sender, EventArgs e)
        {
            search(textBoxShowName.Text, textBoxSeason.Text, textBoxEpisode.Text);
        }

        private void viewSubs(object sender, EventArgs e)
        {
            epPosition = listBox.SelectedIndex;
            if (epPosition >= 0)
            {
                listBox.Items.Clear();
                labelStatus.Text = "Loading...";
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                subList = Addic7ed.GetEpisode(episodeList[epPosition][0]);
                Cursor.Current = Cursors.Default;
                if (subList != null && subList.Count > 0)
                {
                    subListButtons(true);
                    listBox.Items.Clear();
                    for (int i = 0; i < subList.Count; i++)
                    {
                        listBox.Items.Add(subList[i][1]);
                    }
                    labelStatus.Text = String.Format("Subs for {0}:", episodeList[epPosition][1]);
                    if (release != "")
                    {
                        for (int i = 0; i < subList.Count; i++)
                        {
                            if (subList[i][1].ToLower().Contains(release.ToLower()))
                            {
                                if (MessageBox.Show(this, String.Format("Do you want to auto-download \"{0}\" subtitles for\n{1}?", subList[i][1], fileName), "Matching subtitles found!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    listBox.SelectedIndex = i;
                                    downloadSubs(true);
                                }
                                else
                                {
                                    listBox.SelectedIndex = -1;
                                }
                                break;
                            }
                        }
                    }
                }
                else
                {
                    labelStatus.Text = String.Format("No English subtitles for {0} found!", episodeList[epPosition][1]);
                    subListButtons(false);
                }
                buttonChanger();
            }
        }

        private void selectVideoFile(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video files (*.mkv;*.avi;*.mp4)|*.mkv;*.avi;*.mp4";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                videoFile = openFileDialog.FileName;
                filenameParse();
            }
        }

        void filenameParse()
        {
            fileName = System.IO.Path.GetFileName(videoFile);
            Match relMatch = Regex.Match(fileName, @"\-(.*?)\.");
            if (relMatch != null)
            {
                release = relMatch.Groups[1].Value;
            }
            else
            {
                release = "";
            }
            var fileRegexes = new string[4] {@"(.*?)[ \.](?:[0-9]*?[ \.])?[Ss]([0-9]+)[ \.]?[Ee]([0-9]+)",
                                                @"(.*?)[ \.](?:[0-9]*?[ \.])?([0-9]+)[Xx]([0-9]+)",
                                                @"(.*?)[ \.][0-9]{4}()()",
                                                @"(.*?)[ \.]([0-9])([0-9]{2})"};
            bool parseSuccess = false;
            foreach (string regex in fileRegexes)
            {
                Match episodeData = Regex.Match(fileName, regex);
                if (episodeData.Groups.Count == 4)
                {
                    string show = episodeData.Groups[1].Value.Replace(".", " ");
                    string seasonNum = episodeData.Groups[2].Value;
                    string episNum = episodeData.Groups[3].Value;
                    textBoxShowName.Text = show;
                    textBoxSeason.Text = seasonNum;
                    textBoxEpisode.Text = episNum;
                    search(show, seasonNum, episNum, true);
                    parseSuccess = true;
                    break;
                }
            }
            if (!parseSuccess)
            {
                MessageBox.Show(this, String.Format("Unable to process the filename: {0}!\nTry to enter search data manually.", fileName), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void downloadSubs(bool auto = false)
        {
            string filename;
            string subFile = "";
            int position = listBox.SelectedIndex;
            if (position >= 0)
            {               
                if (videoFile != "")
                {
                    filename = fileName.Substring(0, fileName.Length - 3) + "srt";
                }
                else
                {
                    filename = String.Format("{0}.{1}x{2}.srt", showname, season, episode);
                }
                if (auto)
                {
                    subFile = filename;
                }
                else
                {
                    var saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "SRT subtitles (*.srt)|*.srt";
                    saveFileDialog.FileName = filename;
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.OverwritePrompt = true;
                    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        subFile = saveFileDialog.FileName;
                    }
                }
                if (subFile != "")
                {                    
                    int result = Addic7ed.SubDownload(subList[position][0], episodeList[epPosition][0], subFile);
                    if (result == 1)
                    {
                        MessageBox.Show(this, String.Format("Subtitles for {0} downloaded.", fileName), "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (result == -1)
                    {
                        MessageBox.Show(this, "Exceeded daily limit for subs downloads.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(this, "Unable to download subtitles.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void back(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            foreach (string[] item in episodeList)
            {
                listBox.Items.Add(item[1]);
            }
            epListButtons(true);
            labelStatus.Text = String.Format("Episodes found for {0} - {1}x{2}:", this.showname, this.season, this.episode);
            buttonChanger();
        }

        private void listDoubleClick(object sender, EventArgs e)
        {
            string results = labelStatus.Text.Substring(0, 4);
            if (results == "Epis")
            {
                viewSubs(null, null);
            }
            else if (results == "Subs")
            {
                downloadSubs();
            }
        }

        private void openURL(object sender, EventArgs e)
        {
            string results = labelStatus.Text.Substring(0, 4);
            int position = listBox.SelectedIndex;
            if (results == "Epis" && position >= 0)
            {
                System.Diagnostics.Process.Start(episodeList[position][0]);
            }
            else if (results == "Subs" || results == "No E")
            {
                System.Diagnostics.Process.Start(episodeList[epPosition][0]);
            }
        }

        private void download(object sender, EventArgs e)
        {
            downloadSubs();
        }

        private void dragHandler(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }                
            else
            {
                e.Effect = DragDropEffects.None;
            }                
        }

        private void dropHandler(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            videoFile = files[0];
            filenameParse();
        }

        private void showAbout(object sender, EventArgs e)
        {
            Form aboutBox = new AboutBox();
            aboutBox.ShowDialog(this);
        }
    }
}
