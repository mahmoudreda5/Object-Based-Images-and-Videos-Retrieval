using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

//Emgu CV imports
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;

namespace ObjectBasedVideosAndImagesRetrieval {
    class SurfImage {

        ////////////////////////////////////////////////////////////////////////////////
        //global member Objects
        public Image<Bgr, Byte> image;  //process image

        //CPU
        public VectorOfKeyPoint keyPoints;  //SURF or SIFT keyPoints container
        public Matrix<float> descriptors;  //SURF or SIFT Descriptors container

        public bool accepted;
        ////GPU
        //public GpuMat<float> GPUDescriptors;  //SURF or SIFT Descriptors container for GPU
        ////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////
        //constructors
        public SurfImage(Image<Bgr, Byte> image, bool accepted = false) {
            //initialization
            this.image = image;  //process image

            this.keyPoints = new VectorOfKeyPoint();  //SURF or SIFT keyPoints container
            this.descriptors = null;  //SURF or SIFT Descriptors container

            this.accepted = accepted;  //default
            //this.GPUDescriptors = null;  //SURF or SIFT Descriptors container for GPU
        }
        ////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////
        //member functions

        ////////////////////////////////////////////////////////////////////////////////
        //inline reset processImage properties
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void reset(/*no params*/) {
            //reset image KeyPoints and Descriptors
            this.keyPoints = new VectorOfKeyPoint();  //SURF or SIFT keyPoints container
            this.descriptors = null;  //SURF or SIFT Descriptors container

            //this.GPUDescriptors = null;  //SURF or SIFT Descriptors container for GPU
        }

        ////////////////////////////////////////////////////////////////////////////////
        //inline convert processImage from RGB to GrayScale image
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Image<Gray, Byte> toGrayScale(/*no params*/) {
            return this.image.Convert<Gray, Byte>();
        }
        ////////////////////////////////////////////////////////////////////////////////

    }
}
