//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;

//namespace Common.Stuff
//{
//    public static class Logger
//    {
//        private const string FileName = @"c:\FotoRoomLog.txt";


//        public static void ExceptionSaveAndThrow(Exception ex)
//        {
//            var stream = new FileStream(FileName, FileMode.Append, FileAccess.Write);
//            var streamWriter = new StreamWriter(stream);

//            Exception currentEx = ex;

//            while(currentEx != null)
//            {
//                streamWriter.Write("<<====================== START EXCEPTION STACK =========================>>");

//                currentEx = ex.InnerException;

//                streamWriter.Write("Message: " + currentEx.Message);
//                streamWriter.Write("StackTrace: " + currentEx.StackTrace);
//                streamWriter.Write("Source: " + currentEx.Source);
//                streamWriter.Write("TargetSite: " + currentEx.TargetSite);

//                if(currentEx.InnerException !=null)
//                    streamWriter.Write("---------------------- NEXT STACK EXCEPTION ELEMENT --------------------------");
//            }

//            streamWriter.Write("<<====================== END EXCEPTION STACK =========================>>");
//            streamWriter.Flush();
//            streamWriter.Close();
//            streamWriter.Dispose();

//            stream.Flush();
//            stream.Close();
//            stream.Dispose();

//            throw ex;
//        }
//    }
//}
