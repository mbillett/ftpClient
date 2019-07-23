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
    public class MainMenu
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
     
        public static void GetConnectionInformation(ref MainMenu myConnection )
        {
            bool LetsContinueLoop = true;
            string myAnswer = "";
            string userName = "anonymous";
            string passWord = "anonymous";
            string fileName = "";
            string serverName = "";

            do
            {
                Console.WriteLine("\nPlease provide the following information: ");
                Console.WriteLine("Server Name: ");
                Console.WriteLine("Example localhost ");
                string serverResponse = Console.ReadLine();
                serverName = "ftp://" + serverResponse;

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

        

        public static bool DeleteRemoteFile(Uri serverUri){
            //Source: https://docs.microsoft.com/en-us/dotnet/api/system.net.ftpwebrequest?view=netframework-4.8

            if(serverUri.Scheme == Uri.UriSchemeFtp){
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
 
                FtpWebResponse response = (FtpWebResponse) request.GetResponse();
                Console.WriteLine("Delete status: {0}",response.StatusDescription);  
                response.Close();
                
                return true;
            }

            else{
                return false;
            }
        }

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
                    Console.WriteLine(" Not Implemented Yet \n");
                    MyAnswer = false;
                    break;
                case "8":
                    Console.WriteLine(" Not Implemented Yet  \n");
                    MyAnswer = true;
                    break;
                case "7":
                    Console.WriteLine(" Not Implemented Yet  \n");
                    MyAnswer = true;
                    break;
                case "6":
                    Console.WriteLine(" Not Implemented Yet  \n");
                    MyAnswer = true;
                    break;
                case "5":
                    Console.WriteLine(" Not Implemented Yet  \n");

                    MyAnswer = true;
                    break;
                case "4":
                    Console.WriteLine(" You choose 4, Create Directory:  \n");
                    MainMenu tmpConnectionC4 = new MainMenu();
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
                    Console.WriteLine(" Not Implemented Yet \n");
                    MyAnswer = true; 
                    break;
                case "2":
                    Console.WriteLine(" Not Implemented Yet \n");
                    MyAnswer = true;
                    break;
                case "1":
                    Console.WriteLine(" Not Implemented Yet  \n");
                    break;
                default:
                    Console.WriteLine("\n That was not a valid input, Please try again \n");
                    break;
            }
            return MyAnswer;
        } // end getResponce()

        public static void GetConnectionInformationDLF(ref MainMenu myConnection)
        {
            bool LetsContinueLoop = true;
            string myAnswer = "";
            string localFilePath = "";
            do
            {
                Console.WriteLine("\nLocal Path and FileName : ");
                Console.WriteLine("Example c:/download/512KB.zip , note directory must already exist");
                localFilePath = Console.ReadLine();

                Console.WriteLine($"\nYou Entered Local Path and File Name: {localFilePath}");
                Console.WriteLine($"\n Y or y to accept and continue. ");
                myAnswer = Console.ReadLine();
                if (myAnswer == "Y" || myAnswer == "y") { LetsContinueLoop = false; }

            } while (LetsContinueLoop);

            myConnection.localFilePath = localFilePath;

        } // end getConnectionInformationDLF

        public static void GetConnectionInformationList(ref MainMenu myConnection)
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
       

    } // end class MainMenu
} // end namespace ftpClientConApp
