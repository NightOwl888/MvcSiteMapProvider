//using System;
//using System.IO;

//namespace MvcSiteMapProvider.IO
//{
//    public interface IStreamFactory
//    {
//        // TODO: Need to add an "id" for the stream (so file name or guid can be passed to identify the stream for later)
//        // TODO: Need to make a CreateReadable() and CreateWriteable() method here.
//        // TODO: Should probably make a separate abstraction for dealing with HTTP streams and dealing with BLOB (or memory) streams so they can both be injected simultaneously without 
//        // confusion as to where they should go.

//        Stream Create();
//        void Release(Stream stream);
//    }
//}
