using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ftpClientConApp
{
    class FileUpload
    {
        public void UpLoadRemoteFile(ServerConnectionInformation myConnection)
        {
            try
            {
                //Note from Bryan: The below seems to be extensively copied from an example in the Microsoft docs. It should be changed.
                // Get the object used to communicate with the server.
                System.Net.FtpWebRequest request = (FtpWebRequest)WebRequest.Create(myConnection.ServerName);
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
    }
}
