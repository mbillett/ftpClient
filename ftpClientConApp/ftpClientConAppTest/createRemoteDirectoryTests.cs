/*using System;
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

        //Same set up as first test, but with "For,Create,Directory,Test" not present
        [TestMethod]
        public void ifValidUserButInvalidCharacterDirectoryIsNotCreated()
        {
            ServerConnectionInformation info = new ServerConnectionInformation();
            info.UserName = USERNAME;
            info.PassWord = PASSWORD;
            info.ServerName = SERVERNAME;
            CreateRemoteDirectory crd = new CreateRemoteDirectory(info);
            String resp = crd.create("For,Create,Directory,Test");
            Assert.AreNotEqual(resp, "success");
        }

        //Same set up as first, but "CRDTest2" as the directory name
        public void ifAlreadyExistsDirectoryIsNotCreated()
        {
            ServerConnectionInformation info = new ServerConnectionInformation();
            info.UserName = USERNAME;
            info.PassWord = PASSWORD;
            info.ServerName = SERVERNAME;
            CreateRemoteDirectory crd = new CreateRemoteDirectory(info);
            String resp1 = crd.create("CRDTest2");
            String resp2 = crd.create("CRDTest2");
            Assert.AreNotEqual(resp2, "success");
        }


        //Don't need to worry about servers for this test, other than not having such a named server running
        //There should not be a CRDTest3 on the server.
        [TestMethod]
        public void ifNoServerConnectionDirectoryNotCreated()
        {
            ServerConnectionInformation info = new ServerConnectionInformation();
            info.UserName = USERNAME;
            info.PassWord = PASSWORD;
            info.ServerName = "ftp://notARealServerThisIsFakeForATest12398724324";
            CreateRemoteDirectory crd = new CreateRemoteDirectory(info);
            String resp1 = crd.create("CRDTest3");
            String resp2 = crd.create("CRDTest3");
            Assert.AreNotEqual(resp2, "success");
        }

        //There should be the given username + password combo on the server. The folder should not have
        //a CRDTest4
        [TestMethod]
        public void ifUserDoesNotExistDirectoryIsNotCreated()
        {
            ServerConnectionInformation info = new ServerConnectionInformation();
            info.UserName = "thisusershouldnotexist";
            info.PassWord = "thisshouldnotbeapassword";
            info.ServerName = SERVERNAME;
            CreateRemoteDirectory crd = new CreateRemoteDirectory(info);
            String resp1 = crd.create("CRDTest4");
            String resp2 = crd.create("CRDTest4");
            Assert.AreNotEqual(resp2, "success");
        }


        //For this test, the user should be "permdenied" user with "permdenied" password.
        //The user should not have create directory permissions.
        //There should not be a "CRDTest5" directory on the server
        [TestMethod]
        public void ifInvalidUserDirectoryIsNotCreated()
        {
            ServerConnectionInformation info = new ServerConnectionInformation();
            info.UserName = "permdenied";
            info.PassWord = "permdenied";
            info.ServerName = SERVERNAME;
            CreateRemoteDirectory crd = new CreateRemoteDirectory(info);
            String resp1 = crd.create("CRDTest5");
            String resp2 = crd.create("CRDTest5");
            Assert.AreNotEqual(resp2, "success");
        }

        
    }
}*/
