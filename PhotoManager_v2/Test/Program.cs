using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();

        static void Main(string[] args)
        {
            //try
            //{
            //    string[] dirs = Directory.GetDirectories(@"c:\", "*", SearchOption.TopDirectoryOnly);

            //    DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Users\filap\Desktop");
            //    DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();

            //    Console.WriteLine("The number of directories starting with p is {0}.", directoryInfos.Length);
            //    foreach (DirectoryInfo dir in directoryInfos)
            //    {
            //        Console.WriteLine(dir.Name + "\t" + dir.FullName + "\t" + dir.Attributes);
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("The process failed: {0}", e.ToString());
            //}


            DirectoryInfo rootDir = new DirectoryInfo(@"C:\Users\filap\Desktop\zdjęcia\Włochy 2018 Toskania");
            WalkDirectoryTree(rootDir);
            foreach (string s in log)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
        static void WalkDirectoryTree(DirectoryInfo root)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            try
            {
                files = root.GetFiles("*.jpg");
            }
            catch (UnauthorizedAccessException e)
            {
                log.Add(e.Message);
            }

            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (FileInfo fi in files)
                {
                    Console.WriteLine(fi.Name);
                }

                subDirs = root.GetDirectories();

                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    WalkDirectoryTree(dirInfo);
                }
            }
        }
    }
}
