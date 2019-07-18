using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using ftpClientConApp;

namespace ftpClientTests
{
    [TestClass]
    public class ftpClientConAppTests
    {
        private const string USERNAME = "anonymous";
        private const string PASSWORD = "anonymous";
        private const string SERVERNAME = "ftp://speedtest.tele2.net";

        [TestMethod]
        public void TestuserName()
        {
            ftpClientConApp.ServerConnectionInformation MyConnection = new ftpClientConApp.ServerConnectionInformation();
            MyConnection.UserName = "anonymous";

            Assert.AreEqual(USERNAME, MyConnection.UserName);
        }
        [TestMethod]
        public void TestPassWord()
        {
            ftpClientConApp.ServerConnectionInformation MyConnection = new ftpClientConApp.ServerConnectionInformation();
            MyConnection.PassWord = "anonymous";

            Assert.AreEqual(PASSWORD, MyConnection.PassWord);
        }
        [TestMethod]
        public void TestServerName()
        {
            ftpClientConApp.ServerConnectionInformation MyConnection = new ftpClientConApp.ServerConnectionInformation();
            MyConnection.ServerName = "ftp://speedtest.tele2.net";

            Assert.AreEqual(SERVERNAME, MyConnection.ServerName);
        }
    }
}
