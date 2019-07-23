using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ftpClientConApp
{
    public class ServerConnectionInformation
    {
        #region Private Member Variables
        private string userName;
        private string passWord;
        private string serverName;
        #endregion

        #region Public Declarations
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
        
        public ServerConnectionInformation()
        {
            this.userName = null;
            this.passWord = null;
            this.serverName = null;
        }

        public ServerConnectionInformation(String user, String pass, String server)
        {
            this.userName = user;
            this.passWord = pass;
            this.serverName = server;
        }
    }
    
   

    #endregion
}
