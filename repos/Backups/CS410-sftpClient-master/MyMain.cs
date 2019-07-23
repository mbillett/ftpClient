/**
* FILE: MyMain.cs
*  Author Marcella
* Date: June 27 2019
**/
#region Using
Uusing System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using System.Configuration;
using System.Net.Socket;
using System.Threading;  // password
#endregion

namespace SFTP
{
   class MyMain
  {
      #region Public Declarations
      public static string RunOption = System.Configuration.ConfigurationManager.AppSettings["RunOption"];
      public static string MyDateTime = DateTime.Now.ToString("yyyMMddHHmm");
      public static string UserName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
      public static string PassWord = System.Configuration.ConfigurationManager.AppSettings["PassWord"];
      public static string ServerName = System.Configuration.ConfigurationManager.AppSettings["ServerName"];
      public static string MyAnswer = System.Configuration.ConfigurationManager.AppSettings["MyAnswer"];

      public static string LogName =  "SFTPLog_" + MyDateTime + ".log";
      public static StreamWriter PrepLog = File.AppendText(LogName);

      // Command Line args
      public static CmdArgs CmdArgs = null;

      #endregion

      #region Public Method
      static void Main( string[] args )
      {
         Debug.StartLog();
         Debug.WriteLog("Begin MyMain.Main ");
         Debug.WriteLog("RunOption :  " + RunOption);

         Run();

         Debug.WriteLog("End MyMain.Main ");
         Debug.EndLog();
       } // end Main

       static void getConnectionInformation( ref string UserName, ref string PassWord, ref string ServerName )
       {
            // get ServerName
            Console.WriteLine("Server Name: ");
            ServerName = Console.ReadLine();

            // get UserName
            Console.WriteLine("User Name : ");
            UserName = Console.ReadLine();
            //String UserName = Request.LogonUserIdentity.Name;

            // get PassWord
            Console.WriteLine("Password : ");
            // PassWord = Console.ReadLine();
            PassWord = ReadPassword();

        } // end getConnectionInformation

        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);

            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }
   
    #endregion

    #region Private Methods
    private static void Run()
       {
            Debug.WriteLog("BEGIN MyMain.Run");

            try
            {
               /* if (!MigDoc.Open(CmdArgs.MigDocFull))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("*** ERROR failed  ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return;
                }

                */
            }
            catch (Exception ex)
            { /*
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*** ERROR " + ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            */
            }
            finally
            { /*

               */
            }

            Debug.WriteLog("END MyMain.Run");
        }
        #endregion

        class Debug
        {
            public static void StartLog()
            {
                TextWriter w = MyMain.PrepLog;
                w.WriteLine("=============================================================");
                w.WriteLine("{0} === BEGIN LOG", DateTime.Now.ToString("yyyyMMdd-HH:mm:ss"));

                if (MyMain.RunOption == "DEBUGGING")
                {
                    Console.WriteLine("=============================================================");
                    Console.WriteLine("{0} === BEGIN LOG", DateTime.Now.ToString("yyyyMMdd-HH:mm:ss"));
                }
            } // end StartLog

            public static void WriteLog(string logMessage)
            {
                TextWriter w = MyMain.PrepLog;
                w.WriteLine("{0} === {1}", DateTime.Now.ToString("yyyyMMdd-HH:mm:ss"), logMessage);

                if (MyMain.RunOption == "DEBUGGING")
                    Console.WriteLine("{0} === {1}", DateTime.Now.ToString("yyyyMMdd-HH:mm:ss"), logMessage);
            } // end WriteLog

            public static void EndLog()
            {
                TextWriter w = MyMain.PrepLog;
                w.WriteLine("{0} === END LOG", DateTime.Now.ToString("yyyyMMdd-HH:mm:ss"));
                w.WriteLine("=============================================================");

                if (MyMain.RunOption == "DEBUGGING")
                {
                    Console.WriteLine("{0} === END LOG", DateTime.Now.ToString("yyyyMMdd-HH:mm:ss"));
                    Console.WriteLine("=============================================================");
                }

                w.Close();
            } // End Endlog
        } // end Debug

  } // End MyMain

} // End Namespace SFTP




