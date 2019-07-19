using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using ftpClientConApp;

namespace ftpClientConAppTest
{
    [TestClass]
    public class createRemoteDirectoryTests
    {
        private const string USERNAME = "anonymous";
        private const string PASSWORD = "anonymous";
        private const string SERVERNAME = "ftp://localhost";
        private const string FAILNAME = "fakeShouldFail";
        private const string FAILPASS = "fakeShouldFail";
        private const string NOACCESS = "noaccess";
        private const string NOACCESSPASS = "noaccess";

        //For this test, be sure server has "anonymous" user with "anonymous" password set up and able to make directory
        //The default location should not have a directory named "ForCreateDirectoryTest". For now will need to manually remove.
        [TestMethod]
        public void canCreateDirectoryIfValid()
        {
            ServerConnectionInformation info = new ServerConnectionInformation();
            info.UserName = USERNAME;
            info.PassWord = PASSWORD;
            info.ServerName = SERVERNAME;
            CreateRemoteDirectory crd = new CreateRemoteDirectory(info);
            String resp = crd.create("ForCreateDirectoryTest");
            Assert.AreEqual(resp, "success");

        }

        //Same set up as first test
        [TestMethod]
        public void ifValidUserButInvalidCharacterDirectoryIsNotCreated()
        {

        }

        public void ifAlreadyExistsDirectoryIsNotCreated()
        {

        }


        //Don't need to worry about servers for this test, other than not having such a named server running
        [TestMethod]
        public void ifNoServerConnectionDirectoryNotCreated()
        {

        }

        [TestMethod]
        public void ifUserDoesNotExistDirectoryIsNotCreated()
        {

        }

        [TestMethod]
        public void ifInvalidUserDirectoryIsNotCreated()
        {

        }

        
    }
}
