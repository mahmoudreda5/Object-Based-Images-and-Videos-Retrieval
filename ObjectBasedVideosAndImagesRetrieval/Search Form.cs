using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Diagnostics;

//Emgu CV imports
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;

//OpenCVSahrp
using OpenCvSharp;

namespace ObjectBasedVideosAndImagesRetrieval {
    public partial class SearchForm : Form {

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //global member Objects
        Image<Bgr, Byte> objectImage;  //objectImage

        const int IMAGE = 1;  //static const image flag
        const int VIDEO = 2;  //static const video flag

        string objectImagePath;  //object image
        string searchFolderPath;  //serach folder

        bool firstTime;
        int counter;

        static string[] extensions = new string[] { ".jpg", ".jpeg", ".png", ".bmp",
                ".mpg", ".avi", ".wma", ".mov", ".mp4", ".wmv"};  //media files extensions
        const int extensionBarrier = 4;  //number of images Extensions

        List<FileInfo> acceptedFiles;

        Surf mySurf;

        BackgroundWorker backgroundProcess;

        Button searchButton;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        public SearchForm(string objectImagePath, string searchFolderPath, Button searchButton) {
            InitializeComponent();

            this.objectImagePath = objectImagePath;
            this.searchFolderPath = searchFolderPath;
            this.searchButton = searchButton;

            this.firstTime = true;
            this.counter = 0;

            this.acceptedFiles = new List<FileInfo>();

            //do searching process in background thread
            this.backgroundProcess = new BackgroundWorker();

            //add backgroundProcess events
            backgroundProcess.WorkerReportsProgress = true;  //can report progress
            backgroundProcess.DoWork += new DoWorkEventHandler(processInBackground);
            backgroundProcess.RunWorkerCompleted += new RunWorkerCompletedEventHandler(processCompleted);
            backgroundProcess.ProgressChanged += new ProgressChangedEventHandler(updateProgress);

            backgroundProcess.RunWorkerAsync();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //get all files at search folder
        FileInfo[] getFiles() {

            //get selected folder files
            DirectoryInfo directoryInfo = new DirectoryInfo(searchFolderPath);
            IEnumerable<FileInfo> filesInfoEnumerable = directoryInfo.EnumerateFiles();

            FileInfo[] filesInfo = filesInfoEnumerable.Where(file => extensions.Contains(file.Extension)).ToArray<FileInfo>();
            
            return filesInfo;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        List<FileInfo> search(List<FileInfo> filesInfo) {
            //initialize accepted files list
            //List<FileInfo> acceptedFiles = new List<FileInfo>();

            Stopwatch stopWatch = new Stopwatch();


            //read object image
            try {
                objectImage = new Image<Bgr, Byte>(objectImagePath);

                ObjectImage.Image = objectImage;
            } catch(Exception ex) {
                MessageBox.Show("Unable to Load objectImage, ERROR: " + ex.Message, "ERROR!");
                return null;
            }

            //Progress.Maximum = filesInfo.Count;

            //float ratio = 100 / filesInfo.Count;
            //float progress = 0;
            //int counter = 0;

            stopWatch.Start();
            //search for matching files
            foreach(FileInfo fileInfo in filesInfo) {
                //check extension of file image or video

                int extensionIndex = 0;
                //search for extension index
                for(int i = 0; i < extensions.Length; i++)
                    if(extensions[i].Equals(fileInfo.Extension)){
                        extensionIndex = i;
                        break;
                    }
                
                Image<Bgr, Byte> sceneImage = null;
                VideoCapture video = null;

                //read media files
                try {
                    //check image or video
                    if(extensionIndex < extensionBarrier)
                        sceneImage = new Image<Bgr, Byte>(fileInfo.FullName);
                    else
                        video = new VideoCapture(fileInfo.FullName);

                } catch(Exception ex) {
                    MessageBox.Show("Unable to Load media file, ERROR: " + ex.Message, "ERROR!");
                    return null;
                }

                //match file
                Surf mySurf = new Surf(0.8 /*as lowe paper*/, 400);

                bool accepted = false;

                //match read file
                if(sceneImage != null)
                    accepted = mySurf.processSurf(objectImage, sceneImage, ResultImage);
                else if(video != null) accepted = mySurf.processSurf(objectImage, video, ResultImage);

                if(accepted)
                    acceptedFiles.Add(fileInfo);

                //update progress bar
                backgroundProcess.ReportProgress(filesInfo.Count);

                //if((int) (progress + ratio) > (int) progress)
                //    backgroundProcess.ReportProgress((int) (progress + ratio) - (int) progress);

                //progress += ratio;

                //if(counter++ == filesInfo.Count)
                //    final = true;
            }

            stopWatch.Stop();
            MessageBox.Show("we are done.., Time: " + stopWatch.Elapsed.ToString(), "Seaching");
            stopWatch.Reset();

            return acceptedFiles;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        void updateProgress(object obj, ProgressChangedEventArgs e) {
            if(firstTime) {
                Progress.Maximum = e.ProgressPercentage;
                firstTime = false;
            }

            Progress.Increment(1);

            if(counter < acceptedFiles.Count)
                ResultList.Items.Add(acceptedFiles[counter++].Name);

            //if(final)
            //    Progress.Increment(100);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        List<FileInfo> process() {
            //get search folder files
            FileInfo[] filesInfo = getFiles();

            //search
            List<FileInfo> acceptedFiles = new List<FileInfo>(filesInfo);
            /*acceptedFiles = */search(acceptedFiles);

            //this.acceptedFiles = acceptedFiles;

            return acceptedFiles;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void updateList() {
            for(int i = 0; i < acceptedFiles.Count; i++)
                ResultList.Items.Add(acceptedFiles[i].Name);
        }

        private void ResultList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            //get item fileinfo
            //string itemName = ResultList.Items[e.ItemIndex].Text;

            if(mySurf != null && mySurf.isPlaying)
                mySurf.isPlaying = false;

            int itemIndex = e.ItemIndex;
            //search for item FileInfo
            //for(int i = 0; i < acceptedFiles.Count; i++)
                //if(acceptedFiles[i].Name.Equals(itemName)) itemIndex = i;

            int extensionIndex = 0;
            //search for extension
            for(int i = 0; i < extensions.Length; i++)
                if(acceptedFiles[itemIndex].Extension.Equals(extensions[i])) extensionIndex = i;

            Image<Bgr, Byte> sceneImage = null;
            VideoCapture video = null;
            try {
                //check image or video
                if(extensionIndex < extensionBarrier)
                    sceneImage = new Image<Bgr, Byte>(acceptedFiles[itemIndex].FullName);
                else
                    video = new VideoCapture(acceptedFiles[itemIndex].FullName);

            } catch(Exception ex) {
                MessageBox.Show("Unable to Load media file, ERROR: " + ex.Message, "ERROR!");
                return;
            }
            
            mySurf = new Surf(0.8, 400);
            if(sceneImage != null)
                mySurf.processSurf(objectImage, sceneImage, ResultImage, false);
            else if(video != null)
                mySurf.processSurf(objectImage, video, ResultImage, false);
        }

        private void SearchForm_FormClosed(object sender, FormClosedEventArgs e) {
            this.searchButton.Enabled = true;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void processInBackground(object sender, DoWorkEventArgs e) {
            process();
        }

        private void processCompleted(object sender, RunWorkerCompletedEventArgs e) {
            //updateList();
        }

        private void VideoButton_Click(object sender, EventArgs e) {
            if(mySurf != null) {
                mySurf.isPlaying = !mySurf.isPlaying;

                if(VideoButton.Text.Equals("Play"))
                    VideoButton.Text = "Pause";
                else VideoButton.Text = "Play";
            }
        }

        private void Tracking_CheckedChanged(object sender, EventArgs e) {
            if(Tracking.Checked)
                mySurf.localize = true;
            else mySurf.localize = false;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////



    }
}
