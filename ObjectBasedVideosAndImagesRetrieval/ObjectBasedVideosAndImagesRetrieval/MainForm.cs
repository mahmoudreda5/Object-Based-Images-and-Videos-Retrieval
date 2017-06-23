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

//Emgu CV imports
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;

//OpenCVSahrp
using OpenCvSharp;

namespace ObjectBasedVideosAndImagesRetrieval {
    public partial class MainForm : Form {

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //global member Objects
        Image<Bgr, Byte> objectImage;  //object image

        const int IMAGE = 1;  //static const image flag
        const int VIDEO = 2;  //static const video flag

        string objectImagePath;  //object image
        string searchFolderPath;  //serach folder
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        public MainForm() {
            InitializeComponent();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //open image file from hard disk
        OpenFileDialog openFile(int fileFlag) {
            //load file to memory (Image | Video)
            //choose file using OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //check fileFlag to load desired file (Image | Video)
            if(fileFlag == MainForm.IMAGE) {
                //Image file
                //set openFileDialog title and filter
                openFileDialog.Title = "Choose Image";
                openFileDialog.Filter = "Image Files | *.jpg; *.jpeg; *.png; *.bmp";
            } else if(fileFlag == MainForm.VIDEO) {
                //Video file
                //set openFileDialog title and filter
                openFileDialog.Title = "Choose Video";
                openFileDialog.Filter = "Media Files|*.mpg; *.avi; *.wma; *.mov; *.mp4; *.wmv";
            }

            //show dialog and get it's result
            DialogResult dialogResult = openFileDialog.ShowDialog();

            //check choosen file
            if(dialogResult != DialogResult.OK || openFileDialog.FileName == "")
                //no file choosen
                return null;

            //return openFileDialog object
            return openFileDialog;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //open search folder
        FolderBrowserDialog openFolder(/*no params*/) {
            //choose folder using FolderBrowserDialog
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();

            //show dailog
            DialogResult dialogResult = openFolderDialog.ShowDialog();

            //check choosen folder
            if(dialogResult != DialogResult.OK || String.IsNullOrWhiteSpace(openFolderDialog.SelectedPath))
                //no folder choosen
                return null;

            //return openFolderDialog object
            return openFolderDialog;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void ObjectButton_Click(object sender, EventArgs e) {
            //load and open objectImage
            //open Image file
            OpenFileDialog openObjectImage = openFile(MainForm.IMAGE);

            //check choosen file, return openFileDialog must be not equal null
            if(openObjectImage != null) {
                //load objectImage to memory then open it to ImageBox
                try {
                    this.objectImage = new Image<Bgr, Byte>(openObjectImage.FileName);
                    
                    //set objectImage global path
                    objectImagePath = openObjectImage.FileName;

                } catch(Exception ex) {
                    MessageBox.Show("Unable to Load objectImage, ERROR: " + ex.Message, "ERROR!");
                    return;
                }

                //show Image
                this.ObjectImage.Image = this.objectImage;
            }
        }

        private void FolderButton_Click(object sender, EventArgs e) {
            //open search folder
            FolderBrowserDialog openFolderDialog = openFolder();

            if(openFolderDialog != null) {
                //set search folder global path
                searchFolderPath = openFolderDialog.SelectedPath;

                //get selected folder files
                string[] extensions = new string[] { ".jpg", ".jpeg", ".png", ".bmp",
                ".mpg", ".avi", ".wma", ".mov", ".mp4", ".wmv"};

                DirectoryInfo directoryInfo = new DirectoryInfo(openFolderDialog.SelectedPath);
                IEnumerable<FileInfo> filesInfoEnumerable = directoryInfo.EnumerateFiles();

                FileInfo[] filesInfo = filesInfoEnumerable.Where(file=> extensions.Contains(file.Extension)).ToArray<FileInfo>();
                FilesNumber.Text = filesInfo.Length.ToString() + " Selected Files..";
            }

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////


        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //move to search form
        private void SearchButton_Click(object sender, EventArgs e) {
            //launch search form
            if(this.objectImagePath != null && this.searchFolderPath != null) {
                SearchForm searchForm = new SearchForm(this.objectImagePath, this.searchFolderPath, this.SearchButton);
                searchForm.Show();

                //disable this form controls
                this.SearchButton.Enabled = false;
                
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
