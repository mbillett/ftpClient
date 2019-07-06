/**
 * CS 410 Agile Developement Summer 2019 
 * Team #7 
 * Ftp Client Project 
 * 
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//

using System.IO;
using System.Net;
using System.Net.WebSockets; // ftp library
using System.Configuration;
using System.Reflection;
using System.Security.Principal;
//using System.Net.Socket;
using System.Threading;  // password

namespace ftpClientConApp
{
    class ServerConnectionInformation
    {
        #region Private Member Variables
        private string userName;
        private string passWord;
        private string serverName;
        private string emailAddress;
        private string fileName;
        private string myAnswer;
        private string runOption;
        #endregion

        #region Public Declarations
        //public static string MyDateTime = DateTime.Now.ToString("yyyMMddHHmm");
        //public static string LogName = "SFTPLog_" + MyDateTime + ".log";
        //public static StreamWriter PrepLog = File.AppendText(LogName);
        #endregion

        #region Public Properties
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string PassWord
        {
            get { return passWord; }
            set => passWord = value;
        }
        public string ServerName
        {
            get { return serverName; }
            set => serverName = value;
        }
        public string EmailAddress
        {
            get { return emailAddress; }
            set => emailAddress = value;
        }
        public string FileName
        {
            get { return fileName; }
            set => fileName = value;
        }
        public string MyAnswer
        {
            get { return myAnswer; }
            set => myAnswer = value;
        }
        public string RunOption
        {
            get { return runOption; }
            set => runOption = value;
        }
        // #endregion

        // #region Public Methods  
        /// Constructor
        public ServerConnectionInformation()
        {
            ServerName = "";
            UserName = "anonymous";
            PassWord = "nobody@nowhere.com";
            FileName = "";
        }
        ///Constructor
        public ServerConnectionInformation(string serverName, string userName, string passWord,  string fileName)
        {
            ServerName = serverName;
            UserName = userName;
            PassWord = PassWord;
            FileName = fileName;
        }

        public static void GetConnectionInformation(ref ServerConnectionInformation myConnection )
        {
            Console.WriteLine("\nPlease provide the following information: ");

            // get ServerName
            Console.WriteLine("Server Name: ");
            Console.WriteLine("Example ftp://speedtest.tele2.net ");
            string serverName = Console.ReadLine();
            Console.WriteLine($"\nYou Entered Server Name: {serverName}");

            // get UserName
            Console.WriteLine("\nUser Name / anonymous : ");
            string userName = Console.ReadLine();
            Console.WriteLine($"\nYou Entered User Name: {userName}");

            // get PassWord
            Console.WriteLine("\nPassword / anonymous : ");
            string passWord = Console.ReadLine();
            //PassWord = ReadPassword();
            Console.WriteLine($"\nYou Entered Password: {passWord}");

            // get FileName
            Console.WriteLine("\nFileName : ");
            string fileName = Console.ReadLine();
            Console.WriteLine($"\nYou Entered FileName: {fileName}");

            myConnection.ServerName = serverName;
            myConnection.UserName = userName;
            myConnection.PassWord = passWord;
            myConnection.FileName = fileName;

        } // end getConnectionInformation

        public static void GetConnectionInformationList(ref ServerConnectionInformation myConnection)
        {
            Console.WriteLine("\nPlease provide the following information: ");

            // get ServerName
            Console.WriteLine("Server Name: ");
            Console.WriteLine("Example ftp://speedtest.tele2.net ");
            string serverName = Console.ReadLine();
            Console.WriteLine($"\nYou Entered Server Name: {serverName}");

            // get UserName
            Console.WriteLine("\nUser Name / anonymous : ");
            string userName = Console.ReadLine();
            Console.WriteLine($"\nYou Entered User Name: {userName}");

            // get PassWord
            Console.WriteLine("\nPassword / anonymous : ");
            string passWord = Console.ReadLine();
            //PassWord = ReadPassword();
            Console.WriteLine($"\nYou Entered Password: {passWord}");

            myConnection.ServerName = serverName;
            myConnection.UserName = userName;
            myConnection.PassWord = passWord;

        } // end getConnectionInformationList

        #endregion

        static void Main(string[] args)
        {
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("  Welcome to FTPClient \n");
            //Console.ReadKey();

            bool LetsContinueLoop=true;

            do
            {
                DisplayMenu();
                LetsContinueLoop = GetResponce();

            } while (LetsContinueLoop);

        } // end Main()

        public static void DisplayMenu()
        {
            Console.WriteLine("Please Select Action");
            Console.WriteLine("1) Get File From Remote Server");
            Console.WriteLine("2) Put File on Remote Server");
            Console.WriteLine("3) List Files on Remote Server");
            Console.WriteLine("4) Create Directory on Remote Server");
            Console.WriteLine("5) Delete file on Remote Server");
            Console.WriteLine("6) Change file permission on Remote Server");
            Console.WriteLine("7) Rename file on Remote Server");
            Console.WriteLine("8) ");
            Console.WriteLine("9) Exit ftpClient \n");

        } // end DisplayMenu()

        public static bool GetResponce()
        {
            string getAnswer = "";
            bool MyAnswer = true;
            getAnswer = Console.ReadLine();
 
            switch (getAnswer)
            {
                case "9":
                    Console.WriteLine(" You choose 9, Good Bye \n");
                    MyAnswer = false;
                    break;
                case "8":
                    Console.WriteLine(" You choose 8, We will get right on that!  \n");
                    MyAnswer = true;
                    break;
                case "7":
                    Console.WriteLine(" You choose 7, We will get right on that!  \n");
                    MyAnswer = true;
                    break;
                case "6":
                    Console.WriteLine(" You choose 6, We will get right on that!  \n");
                    MyAnswer = true;
                    break;
                case "5":
                    Console.WriteLine(" You choose 5, Delete Files, We will get right on that!  \n");
                    ServerConnectionInformation tmpConnectionC5 = new ServerConnectionInformation();
                    GetConnectionInformation(ref tmpConnectionC5);
                    //DeleteRemoteFile(tmpConnectionC5);
                    MyAnswer = false;
                    break;
                case "4":
                    Console.WriteLine(" You choose 4, Create Directory, We will get right on that!  \n");
                    ServerConnectionInformation tmpConnectionC4 = new ServerConnectionInformation();
                    GetConnectionInformation(ref tmpConnectionC4);
                    //CreateRemoteDirectory(tmpConnectionC4);
                    MyAnswer = false;
                    break;
                case "3":
                    Console.WriteLine(" You choose 3, Listing Remote Files \n");
                    ServerConnectionInformation tmpConnectionC3 = new ServerConnectionInformation();
                    GetConnectionInformationList(ref tmpConnectionC3);
                    ListRemoteDirectory(tmpConnectionC3);
                    MyAnswer = false; 
                    break;
                case "2":
                    Console.WriteLine(" You choose 2, Put File, We will get right on that! \n");
                    ServerConnectionInformation tmpConnectionC2 = new ServerConnectionInformation();
                    GetConnectionInformation(ref tmpConnectionC2);
                    UpLoadRemoteFile(tmpConnectionC2);
                    MyAnswer = false;
                    break;
                case "1":
                    Console.WriteLine(" You choose 1, Get File, We will get right on that!  \n");
                    ServerConnectionInformation tmpConnectionC1 = new ServerConnectionInformation();
                    GetConnectionInformation(ref tmpConnectionC1);
                    //DownLoadRemoteFile(tmpConnectionC1);
                    MyAnswer = false;
                    break;
                default:
                    Console.WriteLine("\n That was not a valid input, Please try again \n");
                    break;
            }
            return MyAnswer;
        } // end getResponce()

        public static void ListRemoteDirectory(ServerConnectionInformation myConnection)
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(myConnection.ServerName);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(myConnection.UserName, myConnection.PassWord);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            Console.WriteLine(reader.ReadToEnd());

            Console.WriteLine($"Directory List Complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        } // end ListRemoteDirectory()

        public static void UpLoadRemoteFile(ServerConnectionInformation myConnection)
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(myConnection.ServerName);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(myConnection.UserName, myConnection.PassWord);

            // Copy the contents of the file to the request stream.
            byte[] fileContents;
            using (StreamReader sourceStream = new StreamReader(myConnection.FileName ))
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

        } // end UpLoadRemoteFile()

        public static void DownLoadRemoteFile(ServerConnectionInformation myConnection)
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(myConnection.ServerName);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            request.Credentials = new NetworkCredential(myConnection.UserName, myConnection.PassWord);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            Console.WriteLine(reader.ReadToEnd());

            Console.WriteLine($"Download Complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();

        } // end DownLoadRemoteFile()

    } // end class ServerConnectionInformation
} // end namespace ftpClientConApp
