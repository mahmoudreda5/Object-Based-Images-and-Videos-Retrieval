# Object-Based-Images-and-Videos-Retrieval
Search for an Object in Large Dataset of Images and Videos using the Object Image (query Image) and Retrieve all Images and Videos contains that Object.


## Dependencies
This project uses Computer Vision Library for C# (EmguCV2.4.1 and OpenCvSharp3.2), we use OpenCvSharp3.2 only for opening videos because of some issues in EmguCV2.4.1 with videos.
* .Net FrameWork (C#).
* EmguCV2.4.1 and OpenCVSharp3.2.

## Set up development environment
* clone the repo.
* open .sln folder using Visual Studio

* link EmguCV2.4.1 to the project:

  *Manual:
    * `download EmguCV2.4.10 from link:https://sourceforge.net/projects/emgucv/files/emgucv/2.4.10/, and install it.`
    * `add .dll files at bin dicrectory from Project->Add Refrence.`
    * `add .dll files at x64 or x86 dicrectory according to your machine from Project->Add Existing Item.`
    
  *Nuget:
    * `open Package Manger Console from Tools->Library Package Manager.`
    * `PM> Install-Package VDK.EmguCV.x64 -Version 2.4.10`
    
    
* link OpenCvSharp3.2 to the project:

  *Nuget:
    * `open Package Manger Console from Tools->Library Package Manager.`
    * `PM> Install-Package OpenCvSharp3-AnyCPU -Version 3.2.0.20170126`
