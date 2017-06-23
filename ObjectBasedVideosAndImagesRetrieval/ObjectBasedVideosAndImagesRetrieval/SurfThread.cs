using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;

//Emgu CV imports
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;

namespace ObjectBasedVideosAndImagesRetrieval {
    class SurfProcess {

        ////////////////////////////////////////////////////////////////////////////////
        //global member Objects
        public Thread process;  //process image

        public SurfImage processResult;  //result Image
        ////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////
        //constructors
        public SurfProcess(Thread process) {
            //initialization
            this.process = process;

            this.processResult = null;

        }
        ////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////
        //member functions

        ////////////////////////////////////////////////////////////////////////////////
        //inline reset processImage properties
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void reset(/*no params*/) {
            //reset image Process Thread and Image
            this.process = null;
            this.processResult = null;
        }

        ////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////

    }
}
