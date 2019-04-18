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
        static void Main(string[] args)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(@"c:\", "*", SearchOption.TopDirectoryOnly);

                DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Users\filap\Desktop");
                DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();

                Console.WriteLine("The number of directories starting with p is {0}.", directoryInfos.Length);
                foreach (DirectoryInfo dir in directoryInfos)
                {
                    Console.WriteLine(dir.Name + "\t" + dir.FullName + "\t" + dir.Attributes);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }
}
