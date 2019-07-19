using System;
using System.Linq;
using System.Net;

namespace ftpClientConApp
{
    //Class for creating a directory on the remote FTP server with its necessary functionality and data.
    //Works off of the functions provided in Program.cs
    class CreateRemoteDirectory
    {
        //A ServerConnectionInformation variable to keep track of the user and server for the use of an instance of this class.
        private ServerConnectionInformation connection;

        //A constructor for the class which takes in a ServerconnectionInformation to set up for its use.
        public CreateRemoteDirectory(ServerConnectionInformation toUse)
        {
            this.connection = new ServerConnectionInformation();
            this.connection.UserName = toUse.UserName;
            this.connection.PassWord = toUse.PassWord;
            this.connection.ServerName = toUse.ServerName;
        }

        //A function to get a name of a directory to create from the user. Returns the string that the user enters as the name.
        public String getDirectoryName()
        {
            Console.WriteLine("\nName of Directory to Create : ");
            String dir = Console.ReadLine();
            return dir;
        }

        //create() is the main function of the class. It takes in a string to add as a directory to the remote server
        //and returns a String which will be 'success' in the case that the directory was successfully created.
        //Otherwise the string will contain a relevant error message.
        public String create(String dir)
        {
            //check for invalid characters
            char[] invalid = new char[] { ',', '<', '>', '|', '.' };

            for (int i = 0; i < invalid.Length; i++)
            {
                if (dir.Contains(invalid[i]))
                {
                    return "Directory name contained invalid character: " + invalid[i];
                }
            }

            //make request
            String remoteDir = this.connection.ServerName + '/' + dir;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(remoteDir);
            request.Credentials = new NetworkCredential(this.connection.UserName, this.connection.PassWord);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;

            //Handle response
            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            } catch (WebException e)
            { 
                return e.Message;
            } catch (UriFormatException e)
            {
                return "Poorly formatted URI. Please enter a valid directory name";
            }
            
            return "success";
            
        }
    }
}
