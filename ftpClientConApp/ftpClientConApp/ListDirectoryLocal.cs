using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ftpClientConApp
{
    class ListDirectoryLocal
    {
        public static void ListDirectory()
        {
            string dir = GetUserInputFileOrDirectory();
            Console.WriteLine("\n");

            if (File.Exists(dir))
            {
                ListFileLocal(dir);
            }
            else if (Directory.Exists(dir))
            {
                string[] files = Directory.GetFiles(dir);
                foreach (string fileName in files)
                {
                    ListFileLocal(fileName);
                }
            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory", dir);
            }

        }
        public static void ListFileLocal(string file)
        {
            FileInfo info = new FileInfo(file);
            FileAttributes attributes = info.Attributes;
            DateTime creationTime = info.CreationTime;
            Console.WriteLine("{2:-15} {0:100}\n    {3:-15} {1:30}\n", file, creationTime.ToString("f"), "File:", "Date Created:");
        }
        public static string GetUserInputFileOrDirectory()
        {
            string dir = @"c:\";
            bool found = false;

            while (!found)
            {
                Console.WriteLine("Enter a directory");
                dir = Console.ReadLine();
                if (File.Exists(dir) || Directory.Exists(dir))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Sorry that is not a directory or file\n");
                }
            }
            return dir;
        }
    }
}
