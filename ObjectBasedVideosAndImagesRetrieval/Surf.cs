using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Diagnostics;

//EmguCV imports
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;
using Emgu.CV.GPU;
using Emgu.CV.Features2D;
using Emgu.CV.Flann;

//OpenCVSahrp
using OpenCvSharp;
using OpenCvSharp.Extensions;


namespace ObjectBasedVideosAndImagesRetrieval {

    class Surf {

        ////////////////////////////////////////////////////////////////////////////////
        //global member Objects
        int K_NEIGHBORS;  //K nearst neighbors, K_NEIGHBORS = 2
        double UNIQNESS_THRESHOLD;  //uniqness threshold ratio, UNIQNESS_THRESHOLD = 0.8 as lowe's paper
        double MIN_HESSIAN;  //minimum hessian matrix thrashold used by SURF algortihm
        int MIN_GOOD_MATCHES;  //minimum number of good matches
        public bool matches, localize;  //matches and lines drawing flags

        int matchingType;  //matching type
        bool threading;  //flag for using multithreading
        bool GPUProcessing;  //flag for using GPU processing

        int ignoredFrames;  //number of ignored frames
        int passedFrames;

        //int currentProcessIndex;  //currentProcessIndex for multithreading

        //int MatchingType;  //flag for SURF Features matching type, BruteForce = 1; Flann = 2
        public const int BRUTE_FORCE = 1;  //brute force SURF Features matching flag
        public const int FLANN = 2;  //flann SURF Features matching flag

        public bool isPlaying;

        SurfImage objectImage;  //object image

        VideoCapture videoSplitter;  //video capture for video processing

        Queue<SurfProcess> processesList;  //list of all processes

        //System.Timers.Timer processTrigger;  //timer for matching process trigging
        System.Timers.Timer frameRead;  //timer for reading frames
        System.Windows.Forms.Timer showingTimer;  //for video showing
        System.Threading.Timer processTrigger;  //timer for matching process trigging

        Semaphore framesSync;  //video frames syncronization
        Semaphore readingSync;
        Object myLock;

        ImageBox showResult;  //result ImageBox
        Stopwatch stopWatch;  //stopwatch to calculate processing time

        bool accepted;  //acceptance flag
        int acceptedVideoFrames;
        int firstObjectFrame;

        bool isSearching;
        bool objectFound;
        int acceptedFramesThreshold;
        /////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////
        //constructors

        //constructor take UNIQNESS_THRESHOLD and MIN_HESSIAN as a parameter
        public Surf(double UNIQNESS_THRESHOLD, double MIN_HESSIAN) {
            //initialization
            this.K_NEIGHBORS = 2;  //K nearst neighbors, K_NEIGHBORS = 2
            this.UNIQNESS_THRESHOLD = UNIQNESS_THRESHOLD;  //uniqness threshold ratio, UNIQNESS_THRESHOLD = 0.8 as lowe's paper
            this.MIN_HESSIAN = MIN_HESSIAN;  //minimum hessian matrix thrashold
            this.MIN_GOOD_MATCHES = 4;  //minimum number of good matches, MIN_GOOD_MATCHES = 4 as number of object corners
            this.matches = false;  //draw matches by default
            this.localize = true;  //draw lines by default

            this.matchingType = 1;  //BruteForce default
            this.threading = false;  //mainThread default
            this.GPUProcessing = false; //CPU processing default

            this.isPlaying = true;
            this.passedFrames = 0;

            //objectImage to be initialized using process function

            //videoSplitter will be initialized after selecting video file
            //this.processTrigger = new System.Timers.Timer();  //timer for matching process trigging
            this.processTrigger = new System.Threading.Timer(new TimerCallback(frameProcess));  //timer for matching process trigging
            this.showingTimer = new System.Windows.Forms.Timer();
            this.frameRead = new System.Timers.Timer();  //timer for reading frames

            this.framesSync = new Semaphore(0 /*initial count*/, 1 /*serial video on-off*/);
            this.readingSync = new Semaphore(0, 7);
            this.myLock = new Object();

            this.stopWatch = new Stopwatch();  //stopwatch to calculate processing time

            this.ignoredFrames = 1;  //default 

            this.accepted = false;  //default
            this.acceptedVideoFrames = 0;
            this.firstObjectFrame = 0;

            this.isSearching = true;
            this.objectFound = false;
            this.acceptedFramesThreshold = 4;  //Trail and Error

        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////
        //member functions

        /////////////////////////////////////////////////////////////////////////////////////////////
        //SURF features detection and extraction using CPU processing
        SurfImage detectAndExtractSURFFeatures(SurfImage processImage, bool GPUProcessing) {

            //check processImage not equal null and not empty
            if(processImage != null) {
                if(GpuInvoke.HasCuda && GPUProcessing) {
                    //CPU processing
                    //SURF feature detection and extraction using GPU processing
                    GpuSURFDetector surfGPU = new GpuSURFDetector(/*default params*/);

                    //reset objectSurfKeyPoints and objectSurfDescriptors
                    processImage.reset();

                    //create gpu image
                    GpuImage<Gray, Byte> gpuProcessImage = new GpuImage<Gray, byte>(processImage.toGrayScale());

                    //SURF feature detection and extraction
                    GpuMat<float> gpuProcessImageKeyPoints = surfGPU.DetectKeyPointsRaw(gpuProcessImage, null /*mask*/);
                    GpuMat<float> gpuProcessImageDescriptors = surfGPU.ComputeDescriptorsRaw(gpuProcessImage, null /*mask*/,
                        gpuProcessImageKeyPoints);
                    
                    //download CPU keypoints and descriptors to processImage
                    surfGPU.DownloadKeypoints(gpuProcessImageKeyPoints, processImage.keyPoints /*out*/);
                    gpuProcessImageDescriptors.Download(processImage.descriptors /*out*/);


                }else {
                    //CPU processing
                    //SURF feature detection and extraction using CPU processing
                    SURFDetector surfCPU = new SURFDetector(this.MIN_HESSIAN, true/*64bit descriptors*/);

                    //reset objectSurfKeyPoints and objectSurfDescriptors
                    processImage.reset();

                    //SURF feature detection and extraction
                    processImage.descriptors = 
                        surfCPU.DetectAndCompute(processImage.toGrayScale(), null /*mask*/, processImage.keyPoints /*out*/);
                }
            }

            return processImage;  //return processed image for matching
        }
        /////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////
        //SURF Features matching and object localization
        SurfImage matchAndLocalize(SurfImage processImage, int matchingType, bool GPUProcessing) {

            //check keyPonts and Descriptors, they must be not equal null
            if(objectImage.keyPoints.Size != 0 && objectImage.descriptors != null
                && processImage.keyPoints.Size != 0 && processImage.descriptors != null) {

                //SURF matches Indices container
                Matrix<int> SURFMatchesIndices = null;

                //SURF matches Distances container
                Matrix<float> SURFMatchesDistances = null;

                HomographyMatrix homography = null;  //homography matrix for object localization
                Matrix<byte> filterMask = null;  //mask to filter SURF matches using ratio 0.8 as lowe's paper


                if(GpuInvoke.HasCuda && GPUProcessing) {
                    //GPU processing

                    //GPU has only BruteForce matching type
                    GpuBruteForceMatcher<float> GPUBruteForceMatcher = new GpuBruteForceMatcher<float>(DistanceType.L2);

                    //GPU descriptors to be matched
                    GpuMat<float> GPUObjectDescriptors = new GpuMat<float>(objectImage.descriptors);
                    GpuMat<float> GPUSceneDescriptors = new GpuMat<float>(processImage.descriptors);

                    //allocate SURF matches Indices and Distances container with sceneImage SURFFeatures size
                    //GPU SURFMatchesIndices and SURFMatchesDistances
                    GpuMat<int> GPUSURFMatchesIndices = new GpuMat<int>(GPUSceneDescriptors.Size.Height, K_NEIGHBORS,
                        1 /*channels*/, true /*continuous*/);
                    GpuMat<float> GPUSURFMatchesDistances = new GpuMat<float>(GPUSceneDescriptors.Size.Height, K_NEIGHBORS,
                        1 /*channels*/, true /*continuous*/);

                    //GPU mask
                    GpuMat<Byte> GPUMask = new GpuMat<byte>(GPUSceneDescriptors.Size.Height, 1 /*cols*/, 1 /*channels*/);

                    //match SURF features
                    Stream stream = new Stream();  //used by GPU matching
                    GPUBruteForceMatcher.KnnMatchSingle(GPUSceneDescriptors, GPUObjectDescriptors, GPUSURFMatchesIndices,
                        GPUSURFMatchesDistances, K_NEIGHBORS, null, stream);

                    //gpu implementation of voteForUniquess
                    using(GpuMat<float> firstDistances = GPUSURFMatchesDistances.Col(0) /*first col vector*/)
                    using(GpuMat<float> secondDistances = GPUSURFMatchesDistances.Col(1) /*second col vector*/) {
                        GpuInvoke.Multiply(secondDistances, new MCvScalar(UNIQNESS_THRESHOLD), secondDistances /*out*/, stream);
                        GpuInvoke.Compare(firstDistances, secondDistances, GPUMask /*out*/, CMP_TYPE.CV_CMP_LE, stream);
                    }

                    //wait for stream to complete tasks
                    stream.WaitForCompletion();

                    //download SURFMatchesIndices, SURFMatchesDistances and filterMask
                    GPUSURFMatchesIndices.Download(SURFMatchesIndices /*out*/);  //SURFMatchesIndices
                    GPUSURFMatchesDistances.Download(SURFMatchesDistances /*out*/);  //SURFMatchesDistances
                    GPUMask.Download(filterMask /*out*/);  //filterMask

                    //count number of good matches
                    int goodMatchesNumber = GpuInvoke.CountNonZero(GPUMask);
                    if(goodMatchesNumber >= 4) {
                        //good matches must be at least 4
                        goodMatchesNumber = Features2DToolbox.VoteForSizeAndOrientation(objectImage.keyPoints,
                            processImage.keyPoints, SURFMatchesIndices, filterMask, 1.5 /*SCALE*/, 20 /*ORIENTATION*/);
                        if(goodMatchesNumber >= 4)
                            homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(objectImage.keyPoints,
                                processImage.keyPoints, SURFMatchesIndices, filterMask, 2 /*RANSAC*/);
                    }
                }else {
                    //CPU processing

                    //allocate SURF matches Indices and Distances container with sceneImage SURFFeatures size
                    //SURF matches Indices container
                    SURFMatchesIndices = new Matrix<int>(processImage.descriptors.Rows, K_NEIGHBORS);

                    //SURF matches Distances container
                    SURFMatchesDistances = new Matrix<float>(processImage.descriptors.Rows, K_NEIGHBORS);

                    //check matching type
                    if(matchingType == Surf.BRUTE_FORCE) {
                        //match surf features using brute force

                        //brute force matching SURF feature
                        BruteForceMatcher<float> bruteForceSurfMatcher = new BruteForceMatcher<float>(DistanceType.L2);

                        //add object SURF features descriptor
                        bruteForceSurfMatcher.Add(this.objectImage.descriptors);
                        //match object SURF features descriptor with scene SURF features descriptor
                        bruteForceSurfMatcher.KnnMatch(processImage.descriptors, SURFMatchesIndices /*out*/,
                            SURFMatchesDistances /*out*/, K_NEIGHBORS, null);

                    } else if(matchingType == Surf.FLANN) {
                        //match surf features using flann

                        //flann matching SURF feature, add object SURF features descriptor
                        Index flannIndex = new Index(this.objectImage.descriptors);

                        //match object SURF features descriptor with scene SURF features descriptor
                        flannIndex.KnnSearch(processImage.descriptors, SURFMatchesIndices /*out*/,
                            SURFMatchesDistances /*out*/, K_NEIGHBORS, 7/*checks*/);
                    }

                    //mask to filter SURF matches using ratio 0.8 as lowe's paper
                    filterMask = new Matrix<byte>(SURFMatchesIndices.Rows, 1);
                    filterMask.SetValue(new MCvScalar(255));

                    //apply mask filtering using ratio 0.8 as lowe's paper
                    Features2DToolbox.VoteForUniqueness(SURFMatchesDistances, UNIQNESS_THRESHOLD, filterMask /*out*/);

                    //count none zero cells "good matches" from mask
                    int goodMatchesNumber = CvInvoke.cvCountNonZero(filterMask);

                    //good matches have to be at least >= MIN_GOOD_MATCHES
                    if(goodMatchesNumber >= this.MIN_GOOD_MATCHES) {
                        //SURFMatches filtering
                        goodMatchesNumber = Features2DToolbox.VoteForSizeAndOrientation(this.objectImage.keyPoints,
                            processImage.keyPoints, SURFMatchesIndices, filterMask, 1.5 /*SCALE*/, 20 /*ORIENTATION*/);

                        //good matches have to be at least >= MIN_GOOD_MATCHES
                        if(goodMatchesNumber >= this.MIN_GOOD_MATCHES)
                            //get homography matrix from good matches to localize object
                            homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(this.objectImage.keyPoints,
                                processImage.keyPoints, SURFMatchesIndices, filterMask, 2 /*RANSAC*/);
                    }
                }
                
                //result image
                SurfImage resultImage = null;
                /*Image<Bgr, Byte> borderedObject = null;*/

                if(matches) {
                    //borderedObject = objectImage.image;
                    //borderedObject.Draw(new Rectangle(1, 1, borderedObject.Width, borderedObject.Height),
                    //    new Bgr(255, 0, 0) /*color*/, 2 /*thickness*/);  //object image with borders

                    ////Draw the good matched keypoints
                    //resultImage = Features2DToolbox.DrawMatches(borderedObject, this.objectImage.keyPoints,
                    //    processImage.image, processImage.keyPoints, SURFMatchesIndices, new Bgr(0, 0, 0),
                    //    new Bgr(255, 255, 255), filterMask, Features2DToolbox.KeypointDrawType.DEFAULT);

                }else if(!matches) {
                    //concate scene image horizontally with object image
                    //borderedObject = objectImage.image;
                    //borderedObject.Draw(new Rectangle(1, 1, borderedObject.Width, borderedObject.Height),
                        //new Bgr(255, 0, 0) /*color*/, 2 /*thickness*/);  //object image with borders
                    resultImage = new SurfImage(processImage.image);  //scene image
                    //matchingImage = matchingImage.ConcateHorizontal(borderedObject);
                }

                //localize the object
                //check homography matrix to localize object at the scene image
                if(homography != null && localize) {
                    //draw rectangle around detected object at scene image

                    //create rectange with object size, (top, left) = (0, 0)
                    Rectangle objectRect = this.objectImage.image.ROI;

                    //create object corners array
                    PointF[] objectCorners = new PointF[] { 
                    new PointF(objectRect.Left, objectRect.Top),  //left, top
                    new PointF(objectRect.Left, objectRect.Bottom),  //left, bottom
                    new PointF(objectRect.Right, objectRect.Bottom),  //right, bottom
                    new PointF(objectRect.Right, objectRect.Top)  //right, top
                    };
                    //get Corresponding objectCorners at the scene image using homography matrix PerspectiveTransform
                    homography.ProjectPoints(objectCorners);



                    //get object corner Points
                    System.Drawing.Point[] objectCornerPoints = Array.ConvertAll<PointF,
                        System.Drawing.Point>(objectCorners, System.Drawing.Point.Round);

                    if(isRectangle(objectCornerPoints)) {
                        //draw rectangle polygon around detected object at scene image
                        resultImage.image.DrawPolyline(objectCornerPoints, true /*isClosed*/, new Bgr(0, 0, 255) /*LineColor*/, 2 /*thickness*/);

                        accepted = true;
                        resultImage.accepted = true;
                    }
                }

                //CvInvoke.Imshow("matching window", this.matchingImage);
                return resultImage;

            } else return new SurfImage(processImage.image);  //no features of sceneImage

        }
        /////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////
        //apply SURF feature detection, extraction and matching then localize detected object 
        //using CPU SEQUENTIAl processing
        SurfImage process(SurfImage sceneImage, int matchingType, bool GPUProcessing, ImageBox resultBox = null) {
            //match two images and return SURF matching image

            //SEQUENTIAl detect and extract surf features for both images
            detectAndExtractSURFFeatures(sceneImage, GPUProcessing);

            //SEQUENTIAl match surf features for both images and then localize detected object at scene image
            SurfImage resultImage = matchAndLocalize(sceneImage, matchingType, GPUProcessing);

            //return result matching image
            return resultImage;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //processTriger timer EventHandler function
        private void frameProcess(Object sender/*, System.Timers.ElapsedEventArgs args*/) {

            if(processesList != null && processesList.Count > (int) videoSplitter.Fps / 2 /*maxNumberOfThreads*/)
                return;


            //MainThread Processing
            //split choosen video into frk2ames using video
            //OpenCvSharp.Mat videoFrameMat = new OpenCvSharp.Mat();  //Mat for frame

            //readingSync.WaitOne();
            //critical section
            lock(myLock) {
                //ignore frames
                for(int i = 0; i < ignoredFrames; i++) {
                    if(videoSplitter != null && passedFrames < videoSplitter.FrameCount) {
                        OpenCvSharp.Mat temp = new OpenCvSharp.Mat();  //Mat for frame
                        videoSplitter.Read(temp);  //read video frame
                        firstObjectFrame++;
                    }
                    passedFrames++;
                }
            }
            //readingSync.Release();

            OpenCvSharp.Mat videoFrameMat = new OpenCvSharp.Mat();  //Mat for frame
            if(videoSplitter != null)
               videoSplitter.Read(videoFrameMat);  //read video frame

            passedFrames++;


                //check video end
                if(videoFrameMat != null && videoFrameMat.Size() != new OpenCvSharp.Size(0, 0) && passedFrames < videoSplitter.FrameCount) {

                    SurfImage videoFrame = new SurfImage(new Image<Bgr, Byte>(
                    OpenCvSharp.Extensions.BitmapConverter.ToBitmap(videoFrameMat)));  //get query frame
                    

                    if(!threading)
                        //process Matching between objectImage and videoFrame
                        if(isSearching) {
                            SurfImage processedFrame = process(videoFrame, this.matchingType, this.GPUProcessing);
                            if(!processedFrame.accepted) firstObjectFrame++;
                            else if(processedFrame.accepted && !objectFound) {
                                firstObjectFrame++;
                                objectFound = true;
                            }

                            if(processedFrame.accepted) acceptedVideoFrames++;

                            if(acceptedVideoFrames >= acceptedFramesThreshold) {
                                //end of video
                                this.accepted = true;

                                //release caller thread to return timer result
                                framesSync.Release();

                                //stop timer, no more trigging
                                //processTrigger.Stop();
                                processTrigger.Dispose();

                                if(videoSplitter != null) {
                                    //release frames data of videoSplitter Capture from memory
                                    videoSplitter.Release();  //release video
                                    videoSplitter.Dispose();  //dispose videoSplitter

                                    videoSplitter = null;
                                }
                            }

                        } else showResult.Image = process(videoFrame, this.matchingType, this.GPUProcessing).image;
                    else if(threading) {
                        //MultiThread Processing

                        //create thread object
                        SurfProcess processThread = new SurfProcess(null);
                        processThread.process = new Thread(() => {
                            processThread.processResult =
                                process(videoFrame, this.matchingType, this.GPUProcessing);
                        });

                        processThread.process.Start();
                        processesList.Enqueue(processThread);

                    }

                } else {
                    //end of video
                    this.accepted = false;

                    if(isSearching)
                    //release caller thread to return timer result
                    framesSync.Release();

                    //stop timer, no more trigging
                    //processTrigger.Stop();
                    processTrigger.Dispose();

                    //release frames data of videoSplitter Capture from memory
                    videoSplitter.Release();  //release video
                    videoSplitter.Dispose();  //dispose videoSplitter

                    videoSplitter = null;
                }

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void showFrame(Object sender, EventArgs args) {

            if(processesList != null && processesList.Count > (int) videoSplitter.Fps / 2 /*maxNumberOfThreads*/)
                return;

            if(!isPlaying) return;


            //MainThread Processing
            //split choosen video into frk2ames using video
            OpenCvSharp.Mat videoFrameMat = new OpenCvSharp.Mat();  //Mat for frame

            //readingSync.WaitOne();
            //critical section
            lock(myLock) {
                //ignore frames
                for(int i = 0; i < ignoredFrames; i++) {
                    if(videoSplitter != null && passedFrames < videoSplitter.FrameCount) {
                        videoSplitter.Read(videoFrameMat);  //read video frame
                        firstObjectFrame++;
                    }

                    passedFrames++;
                }
            }
            //readingSync.Release();

            if(videoSplitter != null)
                videoSplitter.Read(videoFrameMat);  //read video frame

            passedFrames++;


            //check video end
            if(videoFrameMat != null && passedFrames < videoSplitter.FrameCount) {

                SurfImage videoFrame = new SurfImage(new Image<Bgr, Byte>(
                OpenCvSharp.Extensions.BitmapConverter.ToBitmap(videoFrameMat)));  //get query frame


                if(!threading)
                    //process Matching between objectImage and videoFrame
                    if(isSearching) {
                        SurfImage processedFrame = process(videoFrame, this.matchingType, this.GPUProcessing);
                        if(!processedFrame.accepted) firstObjectFrame++;
                        else if(processedFrame.accepted && !objectFound) {
                            firstObjectFrame++;
                            objectFound = true;
                        }

                        if(processedFrame.accepted) acceptedVideoFrames++;

                        if(acceptedVideoFrames >= acceptedFramesThreshold) {
                            //end of video
                            this.accepted = true;

                            //release caller thread to return timer result
                            framesSync.Release();

                            //stop timer, no more trigging
                            //processTrigger.Stop();
                            processTrigger.Dispose();

                            if(videoSplitter != null) {
                                //release frames data of videoSplitter Capture from memory
                                videoSplitter.Release();  //release video
                                videoSplitter.Dispose();  //dispose videoSplitter

                                videoSplitter = null;
                            }
                        }

                    } else showResult.Image = process(videoFrame, this.matchingType, this.GPUProcessing).image;
                else if(threading) {
                    //MultiThread Processing

                    //create thread object
                    SurfProcess processThread = new SurfProcess(null);
                    processThread.process = new Thread(() => {
                        processThread.processResult =
                            process(videoFrame, this.matchingType, this.GPUProcessing);
                    });

                    processThread.process.Start();
                    processesList.Enqueue(processThread);

                }

            } else {
                //end of video
                this.accepted = false;

                if(isSearching)
                    //release caller thread to return timer result
                    framesSync.Release();

                //stop timer, no more trigging
                //processTrigger.Stop();
                processTrigger.Dispose();

                //release frames data of videoSplitter Capture from memory
                videoSplitter.Release();  //release video
                videoSplitter.Dispose();  //dispose videoSplitter

                videoSplitter = null;
            }

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //procesk2sTriger timer EventHandler function
        private void multiFrameRead(Object sender, System.Timers.ElapsedEventArgs args) {
            //int id = Thread.CurrentThread.ManagedThreadId;

            //get thread result to show
            if(processesList.Count != 0 && processesList.Peek().processResult != null)
                showResult.Image = processesList.Dequeue().processResult.image;
            else if(processesList.Count == 0)
                frameRead.Stop();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //main function to be called from outside
        //Image vs Image
        public bool /*ProcessingTime*/ processSurf(Image<Bgr, Byte> objectImage, Image<Bgr, Byte> sceneImage, ImageBox showResult,
            bool isSearching = true /*searching*/, int matchingType = 1 /*BruteForce*/, bool threading = false /*MainThread*/,
            bool GPUProcessing = false /*CPU*/) {

            //string elapsedTime = "";  //processing elapsedTime

            //check input
            if(objectImage != null && sceneImage != null && showResult != null){
                //match images
                this.objectImage = new SurfImage(objectImage);  //object
                SurfImage sceneprocessImage = new SurfImage(sceneImage);  //scene

                //result ImageBox
                this.showResult = showResult;

                //set params
                this.matchingType = matchingType;
                this.threading = threading;
                this.GPUProcessing = GPUProcessing;
                this.isSearching = isSearching;

                //image vs image runs MainThread always
                threading = false;

                //check threading option
                if(threading) {
                    //MultiThreading
                    //two images only no need for Multithreading
                } else if(!threading) {
                    //MainThread

                    //start stopwatch
                    //stopWatch.Start();

                    //first call processObject once
                    detectAndExtractSURFFeatures(this.objectImage ,GPUProcessing);  //detect and compute object features once

                    //detect and compute scene features, then match with object
                    SurfImage resultimage = process(sceneprocessImage, matchingType, GPUProcessing);

                    //stop stopwatch get elapsedTime and reset stopwatch
                    //stopWatch.Stop();
                    //elapsedTime = stopWatch.Elapsed.ToString();
                    //stopWatch.Reset();


                    //show result
                    if(!isSearching)
                        showResult.Image = resultimage.image;

                    return resultimage.accepted;
                }

            }

            //return processing elapsed time
            //return elapsedTime;

            return false;
        }
        
        //Image vs (videoFile | WebCam)
        public bool processSurf(Image<Bgr, Byte> objectImage, VideoCapture videoSplitter, ImageBox showResult,
            bool isSearching = true /*searching*/, int matchingType = 1 /*BruteForce*/, bool threading = false /*MainThread*/,
            bool GPUProcessing = false /*CPU*/) {

            //check input
            if(objectImage != null && videoSplitter != null && showResult != null) {
                //match image
                this.objectImage = new SurfImage(objectImage);  //object image

                //result ImageBox
                this.showResult = showResult;

                //set params
                this.matchingType = matchingType;
                this.threading = threading;
                this.GPUProcessing = GPUProcessing;
                this.isSearching = isSearching;
                this.passedFrames = 0;

                this.videoSplitter = videoSplitter;  //VideoFileReader
                myLock = new Object();

                //first call processObject once
                detectAndExtractSURFFeatures(this.objectImage, GPUProcessing);  //detect and compute object features once

                //check if webcam open default one
                if(!videoSplitter.IsOpened()) {
                    videoSplitter.Open(0 /*default webcam*/);
                    videoSplitter.Fps = 30.0f;  //set streaming frame rate to 30 fps, default = 0.0
                }
                //else video already loaded

                //set time interval between each two Consecutive triggers in milliseconds
                //processTrigger.Interval = (int) ((double) 1000 /*SECOND*/ / this.videoSplitter.Fps);   //frame rate
                //processTrigger.Elapsed += new System.Timers.ElapsedEventHandler(frameProcess);  //trigging event function

                if(threading) {
                    //set time interval between each two Consecutive triggers in milliseconds
                    frameRead.Interval = (int) ((double) 1000 /*SECOND*/ / this.videoSplitter.Fps) / 2;  //frame rate
                    frameRead.Elapsed += new System.Timers.ElapsedEventHandler(multiFrameRead);  //trigging event function
                }

                if(!isSearching) {
                    showingTimer.Interval = (int) ((double) 1000 /*SECOND*/ / this.videoSplitter.Fps) / 2;  //frame rate
                    showingTimer.Tick += new EventHandler(showFrame);  //trigging event function
                }

                ignoredFrames = (int) (this.videoSplitter.Fps /*ignore factor*/ * 0.7);
                if(!isSearching)
                    ignoredFrames = (int) (this.videoSplitter.Fps * .1 /*ignore factor*/);

                //check threading option
                if(threading)
                    //MultiThreading
                    processesList = new Queue<SurfProcess>((int) this.videoSplitter.Fps);

                //start processTrigger
                //processTrigger.Start();
                if(isSearching)
                    processTrigger.Change(0, (int) ((double) 1000 /*SECOND*/ / this.videoSplitter.Fps) * 4);
                else showingTimer.Start();

                if(threading)
                    frameRead.Start();

                if(isSearching)
                //wait for video processTrigger to stop
                framesSync.WaitOne();  //block current thread
            }

            return this.accepted;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //check localization rectangle
        bool isRectangle(System.Drawing.Point[] objectCorners) {
            //check rectangularity of objectCorners Points
            if(objectCorners[0].X < objectCorners[3].X && objectCorners[0].Y < objectCorners[1].Y &&
                objectCorners[0].X < objectCorners[2].X && objectCorners[0].Y < objectCorners[2].Y &&
                objectCorners[1].X < objectCorners[2].X && objectCorners[1].X < objectCorners[3].X &&
                objectCorners[1].Y > objectCorners[3].Y && objectCorners[2].Y > objectCorners[3].Y)
                return true;

            return false;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
