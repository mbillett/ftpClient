using System;
using System.Collections.Generic;
using System.Linq;
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

     

       
        public String change(String dir, String perms)
        {
            return "";

        }
    }
}
