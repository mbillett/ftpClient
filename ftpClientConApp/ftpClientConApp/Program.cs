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
    class Program
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
            set => userName = value;
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
        #endregion

        static void Main(string[] args)
        {
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Welcome to FTP Client ");
            //Console.ReadKey();

            bool LetsContinueLoop=true;

            do
            {
                DisplayMenu();
                LetsContinueLoop = getResponce();

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
            Console.WriteLine("9) Exit ftpClient ");

        } // end DisplayMenu()

        static public bool getResponce()
        {
            string getAnswer = "";
            bool MyAnswer = true;
            getAnswer = Console.ReadLine();
 
            switch (getAnswer)
            {
                case "9":
                    Console.WriteLine(" You choose 9, Good Bye \n");
                    return MyAnswer = false;
                case "8":
                    Console.WriteLine(" You choose 8, We will get right on that!  \n");
                    return MyAnswer = true;
                case "7":
                    Console.WriteLine(" You choose 7, We will get right on that!  \n");
                    return MyAnswer = true;
                case "6":
                    Console.WriteLine(" You choose 6, We will get right on that!  \n");
                    return MyAnswer = true;
                case "5":
                    Console.WriteLine(" You choose 5, We will get right on that!  \n");
                    return MyAnswer = true;
                case "4":
                    Console.WriteLine(" You choose 4, We will get right on that!  \n");
                    return MyAnswer = true;
                case "3":
                    Console.WriteLine(" You choose 3, Listing Remote Files \n");
                    return MyAnswer = true;
                case "2":
                    Console.WriteLine(" You choose 2, We will get right on that! \n");
                    return MyAnswer = true;
                case "1":
                    Console.WriteLine(" You choose 1, We will get right on that!  \n");
                    //getConnectionInformation(UserName, PassWord, ServerName, FileName);
                    return MyAnswer = true;

                default:
                    Console.WriteLine("\n\n That was not a valid input, Please try again ");
                    break;
            }

            return MyAnswer;
        }

        //private static void getConnectionInformation(string userName, string passWord, string serverName, string fileName)
        //{
        //    throw new NotImplementedException();
        //}

        //public static void getConnectionInformation(ref string UserName, ref string PassWord, ref string ServerName, ref string FileName)
        public static void GetConnectionInformation(string UserName, string PassWord, string ServerName, string FileName)
        {
            Console.WriteLine("\nPlease provide the following information: ");

            // get ServerName
            Console.WriteLine("\nServer Name: ");
            Console.WriteLine("\nExample serverName.xx.com ");
            ServerName = Console.ReadLine();
            Console.WriteLine($"\nYou Entered Server Name: {ServerName}");

            // get UserName
            Console.WriteLine("\nUser Name : ");
            UserName = Console.ReadLine();
            Console.WriteLine($"\nYou Entered User Name: {UserName}");

            // get PassWord
            Console.WriteLine("\nPassword : ");
            PassWord = Console.ReadLine();
            //PassWord = ReadPassword();
            Console.WriteLine($"\nYou Entered Password: {PassWord}");

            // get FileName
            Console.WriteLine("\nFileName : ");
            FileName = Console.ReadLine();
            Console.WriteLine($"\nYou Entered FileName: {FileName}");

        } // end getConnectionInformation

    } // end class Program
} // end namespace ftpClientConApp
