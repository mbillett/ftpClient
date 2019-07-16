using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ftpClientConApp
{
    class CreateRemoteDirectory
    {
        private ServerConnectionInformation connection;

        public CreateRemoteDirectory(ServerConnectionInformation toUse)
        {
            this.connection = new ServerConnectionInformation();
            this.connection.UserName = toUse.UserName;
            this.connection.PassWord = toUse.PassWord;
            this.connection.ServerName = toUse.ServerName;
        }

        public Boolean create()
        {
            Console.WriteLine("\nName of Directory to Create : ");
            String dir = Console.ReadLine();
            String remoteDir = this.connection.ServerName + '/' + dir;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(remoteDir);
            request.Credentials = new NetworkCredential(this.connection.UserName, this.connection.PassWord);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            } catch (WebException e)
            {
                if(e.Status == WebExceptionStatus.ServerProtocolViolation)
                {
                    if(((FtpWebResponse)e.Response).StatusCode.Equals("550") == true)
                    {
                        Console.WriteLine("You did not have permission to create the directory");
                    } 
                }
                else
                {
                    Console.WriteLine(e.Message);
                }



                return false;
            }
            
            return true;
            
        }
    }
}
