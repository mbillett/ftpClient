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
        private FtpWebRequest connection;

        public CreateRemoteDirectory(FtpWebRequest toUse)
        {
            this.connection = toUse;
        }

        public Boolean create()
        {
            return true;
        }
    }
}
