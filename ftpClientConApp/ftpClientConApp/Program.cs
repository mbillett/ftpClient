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
    public class FtpClientMain
    {
        static void Main(string[] args)
        {
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("  Welcome to FTPClient \n");

            bool LetsContinueLoop=true;
            int running = 1;
            String username = "";
            String password = "";
            String server = "";

            while(running == 1)
            {
                Console.WriteLine("Press 1 to log in, Press 2 to exit");
                String response = Console.ReadLine();
                if (response == "1")
                {
                    //get credentials for user
                    Console.WriteLine("Enter your FTP server (localhost, an IP address, etc.\n");
                    server = Console.ReadLine();
                    Console.WriteLine("Enter your FTP server username\n");
                    username = Console.ReadLine();
                    Console.WriteLine("Enter your FTP server password\n");
                    password = Console.ReadLine();

                    bool timeout = false;
                    /**
                     * TO IMPLEMENT: LOG - IN
                     * Use the above info to try making a request to the server
                     * The request should be something like dirsize where only read permissions are necessary
                     * Only matters if the response code indicates success, if so proceed.
                     * 
                     * 
                     * TO IMPLEMENT: Time out
                     * Repeat similar to the above with the request to the server. If the response code
                     * doesn't indicate success, set timeout to true.
                     * 
                     * 
                     * TO IMPLEMENT: Log out
                     * if the input option is set to the log out option, set timeout to false
                     */

                    while(timeout == false)
                    {
                        DisplayMenu();
                        timeout = GetResponce(username, password, server);

                        //TIMEOUT -- probably want to do timeout check here
                    }
                }
                else if(response == "2")
                {
                    running = 0;
                } else
                {
                    continue;
                }
            }

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

        public static bool GetResponce(String username, String password, String server)
        {
            string getAnswer = "";
            bool MyAnswer = true;
            getAnswer = Console.ReadLine();
            ServerConnectionInformation conn = new ServerConnectionInformation(username, password, server);
            switch (getAnswer)
            {
                case "9":
                    Console.WriteLine(" Not Implemented Yet \n");
                    //Log out -> set to true
                    MyAnswer = false;
                    break;
                case "8":
                    Console.WriteLine(" Not Implemented Yet  \n");
                    MyAnswer = false;
                    break;
                case "7":
                    Console.WriteLine(" Not Implemented Yet  \n");
                    //Rename remote file
                    MyAnswer = false;
                    break;
                case "6":
                    Console.WriteLine(" Not Implemented Yet  \n");
                    //Change file permissions
                    MyAnswer = false;
                    break;
                case "5":
                    Console.WriteLine(" Not Implemented Yet  \n");
                    //Delete file on remote server
                    MyAnswer = false;
                    break;
                case "4":
                    Console.WriteLine(" You choose 4, Create Directory:  \n");
                    //create remote directory
                    
            
                    CreateRemoteDirectory createRemDir = new CreateRemoteDirectory(conn);
                    String directory = createRemDir.getDirectoryName();
                    String response = createRemDir.create(directory);
                    if (response == "success")
                    {
                        Console.Write("Directory Created\n");
                    }
                    else if (response == "disconnect")
                    {
                        //If lost connection to server, log out
                        MyAnswer = true;
                        break;
                    }
                    else
                    {
                        Console.Write("Could not create directory due to an error.\n" + response + "\n");
                    }
                    MyAnswer = false;
                    break;
                case "3":
                    Console.WriteLine(" Not Implemented Yet \n");
                    //list remote directory
                    MyAnswer = false; 
                    break;
                case "2":
                    Console.WriteLine(" Not Implemented Yet \n");
                    MyAnswer = false;
                    break;
                case "1":
                    Console.WriteLine(" Not Implemented Yet  \n");
                    //File upload
                    break;
                default:
                    Console.WriteLine("\n That was not a valid input, Please try again \n");
                    //File download
                    break;
            }
            return MyAnswer;
        } // end getResponce()
    } // end class MainMenu
} // end namespace ftpClientConApp
