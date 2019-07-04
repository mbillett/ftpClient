/**
* FILE: MyMain.cs
*  Author Marcella Billett Draft start 
* Date: July 3 2019
**/
#region Using
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.WebSockets;
using System.Net.WebSockets;
using System.Text;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using System.Configuration;
using System.Net.Socket;
using System.Threading;  // password
#endregion

namespace FTPClient
{
   class MyMain
  {
      #region Public Declarations
      public static string RunOption = System.Configuration.ConfigurationManager.AppSettings["RunOption"];
      public static string MyDateTime = DateTime.Now.ToString("yyyMMddHHmm");
      public static string UserName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
      public static string PassWord = System.Configuration.ConfigurationManager.AppSettings["PassWord"];
      public static string ServerName = System.Configuration.ConfigurationManager.AppSettings["ServerName"];
      public static string FileName = System.Configuration.ConfigurationManager.AppSettings["FileName"];
      public static string EmailAddress = System.Configuration.ConfigurationManager.AppSettings["EmailAddress"];
      public static string MyAnswer = System.Configuration.ConfigurationManager.AppSettings["MyAnswer"];

      public static string LogName =  "SFTPLog_" + MyDateTime + ".log";
      public static StreamWriter PrepLog = File.AppendText(LogName);

      // Command Line args
      public static CmdArgs CmdArgs = null;

      #endregion

      #region Public Method
      static void Main( string[] args )
      {
          //Debug.StartLog();
          //Debug.WriteLog("Begin MyMain.Main ");
          //Debug.WriteLog("RunOption :  " + RunOption);

          //Run();
          getConnectionInformation();

          //Debug.WriteLog("End MyMain.Main ");
          //Debug.EndLog();
        } // end Main

       static void getConnectionInformation( ref string UserName, ref string PassWord, ref string ServerName )
       {
            // get ServerName
            Console.WriteLine("\nServer Name: ");
            ServerName = Console.ReadLine();
            Console.WriteLine($"\nYou Entered Server Name: {ServerName}");

            // get UserName
            Console.WriteLine("\nUser Name : ");
            UserName = Console.ReadLine();
            Console.WriteLine($"\nYou Entered User Name: {UserName}");

            // get PassWord
            Console.WriteLine("\nPassword : ");
            // PassWord = Console.ReadLine();
            PassWord = ReadPassword();
            Console.WriteLine($"\nYou Entered Password: {PassWord}");

            // get FileName
            Console.WriteLine("\nFileName : ");
            FileName = Console.ReadLine();
            Console.WriteLine($"\nYou Entered FileName: {FileName}");

        } // end getConnectionInformation

        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);

            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }

        public static void getFile(ref string UserName, ref string PassWord, ref string ServerName, ref string FileName)
        {
            using (FtpConnection ftp = new FtpConnection("ServerName", "UserName", "PassWord"))
            {

                ftp.Open(); /* Open the FTP connection */
                ftp.Login(); /* Login using previously provided credentials */

                if (ftp.DirectoryExists("/incoming")) /* check that a directory exists */
                    ftp.SetCurrentDirectory("/incoming"); /* change current directory */

                if (ftp.FileExists("/incoming/file.txt"))  /* check that a file exists */
                    ftp.GetFile("/incoming/file.txt", false); /* download /incoming/file.txt as file.txt to current executing directory, overwrite if it exists */

                //do some processing

                try
                {
                    ftp.SetCurrentDirectory("/outgoing");
                    ftp.PutFile(@"c:\localfile.txt", "file.txt"); /* upload c:\localfile.txt to the current ftp directory as file.txt */
                }
                catch (FtpException e)
                {
                    Console.WriteLine(String.Format("FTP Error: {0} {1}", e.ErrorCode, e.Message));
                }

                foreach (var dir in ftp.GetDirectories("/incoming/processed"))
                {
                    Console.WriteLine(dir.Name);
                    Console.WriteLine(dir.CreationTime);
                    foreach (var file in dir.GetFiles())
                    {
                        Console.WriteLine(file.Name);
                        Console.WriteLine(file.LastAccessTime);
                    }
                }
            }
        } // End getFile()

        public static void downLoadFile( ref string UserName, ref string PassWord, ref string ServerName, ref string FileName )
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ServerName");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // This example assumes the FTP site uses anonymous logon.
            //request.Credentials = new NetworkCredential("anonymous", "janeDoe@contoso.com");
            request.Credentials = new NetworkCredential("UserName", "PassWord");

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            Console.WriteLine(reader.ReadToEnd());

            Console.WriteLine($"Download Complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        } // End downLoadFile

        public static void upLoadFiles( ref string UserName, ref string PassWord, ref string ServerName, ref string FileName )
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ServerName");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("UserName", "PassWord");

            // Copy the contents of the file to the request stream.
            byte[] fileContents;
            using (StreamReader sourceStream = new StreamReader("FileName"))
            {
                fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            }

            request.ContentLength = fileContents.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
            }
        } // End upLoadFiles()

        public static void listDirectoryContents( ref string UserName, ref string PassWord, ref string ServerName, ref string FileName )
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ServerName");
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("UserName", "PassWord");

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            Console.WriteLine(reader.ReadToEnd());

            Console.WriteLine($"Directory List Complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        } // End listDirectoryContents()


        #endregion

        #region Private Methods
        private static void Run()
       {
            Debug.WriteLog("BEGIN MyMain.Run");

            try
            {
               /* if (!MigDoc.Open(CmdArgs.MigDocFull))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("*** ERROR failed  ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return;
                }

                */
            }
            catch (Exception ex)
            { /*
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*** ERROR " + ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            */
            }
            finally
            { /*

               */
            }

            Debug.WriteLog("END MyMain.Run");
        }
        #endregion

        class Debug
        {
            public static void StartLog()
            {
                TextWriter w = MyMain.PrepLog;
                w.WriteLine("=============================================================");
                w.WriteLine("{0} === BEGIN LOG", DateTime.Now.ToString("yyyyMMdd-HH:mm:ss"));

                if (MyMain.RunOption == "DEBUGGING")
                {
                    Console.WriteLine("=============================================================");
                    Console.WriteLine("{0} === BEGIN LOG", DateTime.Now.ToString("yyyyMMdd-HH:mm:ss"));
                }
            } // end StartLog

            public static void WriteLog(string logMessage)
            {
                TextWriter w = MyMain.PrepLog;
                w.WriteLine("{0} === {1}", DateTime.Now.ToString("yyyyMMdd-HH:mm:ss"), logMessage);

                if (MyMain.RunOption == "DEBUGGING")
                    Console.WriteLine("{0} === {1}", DateTime.Now.ToString("yyyyMMdd-HH:mm:ss"), logMessage);
            } // end WriteLog

            public static void EndLog()
            {
                TextWriter w = MyMain.PrepLog;
                w.WriteLine("{0} === END LOG", DateTime.Now.ToString("yyyyMMdd-HH:mm:ss"));
                w.WriteLine("=============================================================");

                if (MyMain.RunOption == "DEBUGGING")
                {
                    Console.WriteLine("{0} === END LOG", DateTime.Now.ToString("yyyyMMdd-HH:mm:ss"));
                    Console.WriteLine("=============================================================");
                }

                w.Close();
            } // End Endlog
        } // end Debug

  } // End MyMain

} // End Namespace SFTP




