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
using System.Net.Sockets;
using System.Threading;  // password

namespace ftpClientConApp
{
    public class ServerConnectionInformation
    {
        #region Private Member Variables
        private string userName;
        private string passWord;
        private string serverName;
        private string emailAddress;
        private string fileName;
        private string localFilePath;
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
        public string LocalFilePath
        {
            get { return localFilePath; }
            set => localFilePath = value;
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
            LocalFilePath = "";
        }
        ///Constructor
        public ServerConnectionInformation(string serverName, string userName, string passWord, string fileName, string localFilePath)
        {
            ServerName = serverName;
            UserName = userName;
            PassWord = PassWord;
            FileName = fileName;
            LocalFilePath = localFilePath;
        }

        // general 
        public static void GetConnectionInformation(ref ServerConnectionInformation myConnection )
        {
            bool LetsContinueLoop = true;
            string myAnswer = "";
            string serverName = "ftp://speedtest.tele2.net/512KB.zip";
            string userName = "anonymous";
            string passWord = "anonymous";
            string fileName = "512KB.zip";
            //string localFilePath = "c:\\Download\\";

            do
            {
                Console.WriteLine("\nPlease provide the following information: ");

                Console.WriteLine("Server Name: ");
                Console.WriteLine("Example ftp://speedtest.tele2.net ");
                serverName = Console.ReadLine();

                Console.WriteLine("\nUser Name / anonymous : ");
                userName = Console.ReadLine();

                Console.WriteLine("\nPassword / anonymous : ");
                passWord = Console.ReadLine();
                //PassWord = ReadPassword(); // not included yet

                Console.WriteLine("\nFileName : ");
                fileName = Console.ReadLine();

                Console.WriteLine($"\nYou Entered Server Name: {serverName}");
                Console.WriteLine($"\nYou Entered User Name: {userName}");
                Console.WriteLine($"\nYou Entered Password: {passWord}");
                Console.WriteLine($"\nYou Entered FileName: {fileName}");
                Console.WriteLine($"\n Y or y to accept and continue. ");
                myAnswer = Console.ReadLine();
                if (myAnswer == "Y" || myAnswer == "y") { LetsContinueLoop = false; }

            } while (LetsContinueLoop);

            myConnection.ServerName = serverName;
            myConnection.UserName = userName;
            myConnection.PassWord = passWord;
            myConnection.FileName = fileName;

        } // end getConnectionInformation

        public static void UpLoadRemoteFile(ServerConnectionInformation myConnection)
        {
            try
            {
                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(myConnection.ServerName);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(myConnection.UserName, myConnection.PassWord);

                // Copy the contents of the file to the request stream.
                byte[] fileContents;
                using (StreamReader sourceStream = new StreamReader(myConnection.FileName))
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
            }
            catch (UriFormatException e)
            {
                Console.WriteLine("\n Exception Invalid URI Empty Credentials: {0}", e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("\n Generic Exception Handler: {0}", e.ToString());
            }
        } // end UpLoadRemoteFile()

        #endregion

        static void Main(string[] args)
        {
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("  Welcome to FTPClient \n");

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
                    MyAnswer = true;
                    break;
                case "4":
                    Console.WriteLine(" You choose 4, Create Directory, We will get right on that!  \n");
                    ServerConnectionInformation tmpConnectionC4 = new ServerConnectionInformation();
                    GetConnectionInformationList(ref tmpConnectionC4);
                    CreateRemoteDirectory createRemDir = new CreateRemoteDirectory(tmpConnectionC4 );
                    String directory = createRemDir.getDirectoryName();
                    String response = createRemDir.create(directory);
                    if(response == "success")
                    {
                        Console.Write("Directory Created\n");
                    } else
                    {
                        Console.Write("Could not create directory due to an error.\n" + response + "\n");
                    }
                    MyAnswer = true;
                    break;
                case "3":
                    Console.WriteLine(" You choose 3, Listing Remote Files \n");
                    ServerConnectionInformation tmpConnectionC3 = new ServerConnectionInformation();
                    GetConnectionInformationList(ref tmpConnectionC3);
                    ListRemoteDirectory(tmpConnectionC3);
                    MyAnswer = true; 
                    break;
                case "2":
                    Console.WriteLine(" You choose 2, Put File, We will get right on that! \n");
                    ServerConnectionInformation tmpConnectionC2 = new ServerConnectionInformation();
                    GetConnectionInformation(ref tmpConnectionC2);
                    //UpLoadRemoteFile(tmpConnectionC2);
                    MyAnswer = true;
                    break;
                case "1":
                    Console.WriteLine(" You choose 1, Get File, We will get right on that!  \n");
                    ServerConnectionInformation tmpConnectionC1 = new ServerConnectionInformation();
                    GetConnectionInformationDLF(ref tmpConnectionC1);
                    DownLoadRemoteFile(tmpConnectionC1);
                    MyAnswer = true;
                    break;
                default:
                    Console.WriteLine("\n That was not a valid input, Please try again \n");
                    break;
            }
            return MyAnswer;
        } // end getResponce()

// Down Load Remote File
        public static void GetConnectionInformationDLF(ref ServerConnectionInformation myConnection)
        {
            bool LetsContinueLoop = true;
            string myAnswer = "";
            string serverName = "ftp://speedtest.tele2.net/512KB.zip";
            string userName = "anonymous";
            string passWord = "anonymous";
            string localFilePath = "c:\\download\\512KB.zip";

            do
            {
                Console.WriteLine("\nPlease provide the following information: ");
                Console.WriteLine("Server Name: ");
                Console.WriteLine("Example ftp://speedtest.tele2.net/512KB.zip ");
                serverName = Console.ReadLine();

                Console.WriteLine("\nUser Name / anonymous : ");
                userName = Console.ReadLine();

                Console.WriteLine("\nPassword / anonymous : ");
                passWord = Console.ReadLine();
                //PassWord = ReadPassword();

                Console.WriteLine("\nLocal Path and FileName : ");
                Console.WriteLine("Example c:/download/512KB.zip , note directory must already exist");
                localFilePath = Console.ReadLine();

                Console.WriteLine($"\nYou Entered Server Name: {serverName}");
                Console.WriteLine($"\nYou Entered User Name: {userName}");
                Console.WriteLine($"\nYou Entered Password: {passWord}");
                Console.WriteLine($"\nYou Entered Local Path and File Name: {localFilePath}");
                Console.WriteLine($"\n Y or y to accept and continue. ");
                myAnswer = Console.ReadLine();
                if (myAnswer == "Y" || myAnswer == "y") { LetsContinueLoop = false; }

            } while (LetsContinueLoop);

            myConnection.ServerName = serverName;
            myConnection.UserName = userName;
            myConnection.PassWord = passWord;
            myConnection.localFilePath = localFilePath;

        } // end getConnectionInformationDLF

        public static FtpWebRequest CreateFtpWebRequest(string ftpDirectoryPath, string userName, string password, bool keepAlive = false)
        {
             //// Credit
            ////https://stackoverflow.com/questions/12519290/downloading-files-using-ftpwebrequest
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(ftpDirectoryPath));

            //Set proxy to null. Under current configuration if this option is not set then the proxy that is used will get an html response from the web content gateway (firewall monitoring system)
            request.Proxy = null;

            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = keepAlive;

            request.Credentials = new NetworkCredential(userName, password);

            return request;
        } // end CreateFtpWebRequest()

        public static void DownLoadRemoteFile(ServerConnectionInformation myConnection)
        {
             //// Credit
            ////https://stackoverflow.com/questions/12519290/downloading-files-using-ftpwebrequest
            try
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];

                FtpWebRequest request = CreateFtpWebRequest(myConnection.ServerName, myConnection.UserName, myConnection.PassWord, true);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                Stream reader = request.GetResponse().GetResponseStream();
                FileStream fileStream = new FileStream(myConnection.LocalFilePath, FileMode.Create);

                while (true)
                {
                    bytesRead = reader.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();       
            }
            catch (UriFormatException e)
            {
                Console.WriteLine("\n Exception Invalid URI Empty Credentials: {0}", e.ToString());
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("\n Generic Exception Handler: {0}", e.ToString());
                throw;
            }
        } // end DownLoadRemoteFile()

// List Remote Directory
        public static void GetConnectionInformationList(ref ServerConnectionInformation myConnection)
        {
            bool LetsContinueLoop = true;
            string myAnswer = "";
            string serverName = "ftp://speedtest.tele2.net";
            string userName = "anonymous";
            string passWord = "anonymous";

            do
            {
                Console.WriteLine("\nPlease provide the following information: ");

                Console.WriteLine("Server Name: ");
                Console.WriteLine("Example ftp://speedtest.tele2.net ");
                serverName = Console.ReadLine();

                Console.WriteLine("\nUser Name / anonymous : ");
                userName = Console.ReadLine();

                Console.WriteLine("\nPassword / anonymous : ");
                passWord = Console.ReadLine();
                //PassWord = ReadPassword();

                Console.WriteLine($"\nYou Entered Server Name: {serverName}");
                Console.WriteLine($"\nYou Entered User Name: {userName}");
                Console.WriteLine($"\nYou Entered Password: {passWord}");
                Console.WriteLine($"\n Y or y to accept and continue. ");
                myAnswer = Console.ReadLine();
                if (myAnswer == "Y" || myAnswer == "y") { LetsContinueLoop = false; }

            } while (LetsContinueLoop);

            myConnection.ServerName = serverName;
            myConnection.UserName = userName;
            myConnection.PassWord = passWord;

        } // end getConnectionInformationList

        public static void ListRemoteDirectory(ServerConnectionInformation myConnection)
        {
            //credit
            //https://stackoverflow.com/questions/41110384/list-names-of-files-in-ftp-directory-and-its-subdirectories
            try
            {
                //Note from Bryan: I am concerned about this code. It looks copied from an example. What about other code?

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
            }
            catch (UriFormatException e)
            {
                Console.WriteLine("\n Exception Invalid URI Empty Credentials: {0}", e.ToString());
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("\n Generic Exception Handler: {0}", e.ToString());
                throw;
            }
        } // end ListRemoteDirectory()

    } // end class ServerConnectionInformation
} // end namespace ftpClientConApp
