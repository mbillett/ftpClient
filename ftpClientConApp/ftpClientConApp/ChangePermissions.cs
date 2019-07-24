using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ftpClientConApp
{
    class ChangePermissions
    {
        private ServerConnectionInformation connection;

        //A constructor for the class which takes in a ServerconnectionInformation to set up for its use.
        public ChangePermissions(ServerConnectionInformation toUse)
        {
            this.connection = toUse;

        }
       
        public String change(String dir, int perms)
        {
            try
            {
                FluentFTP.FtpClient client = new FluentFTP.FtpClient(this.connection.ServerName);
               
                client.Credentials = new NetworkCredential(this.connection.UserName, this.connection.PassWord);
                client.Chmod(dir, perms);
            } catch(FluentFTP.FtpAuthenticationException)
            {

            } catch(FluentFTP.Ftp)

            
            return "";
        }

    }
}
