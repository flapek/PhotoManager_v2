using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PhotoManager_v2.Class
{
    class OpenEditingProgram
    {
        private string pathToPS = @"C:\Users\filap\Desktop\pscs6\PhotoshopCS6Portable.exe";
        private string pathToPaint = "mspaint.exe";

        public OpenEditingProgram()
        {
        }

        public async Task Open()
        {
            await Task.Run(() => {
                try
                {
                    ProcessStartInfo start_info = new ProcessStartInfo(pathToPaint);       //jak automatycznie otworzyć program z plikiem
                    Process process = new Process();

                    start_info.WindowStyle = ProcessWindowStyle.Maximized;
                    process.StartInfo = start_info;

                    process.Start();

                    //ShowInTaskbar = false;
                    //Hide();

                    process.WaitForExit();

                    //ShowInTaskbar = true;
                    //Show();
                }
                catch (Exception)
                {
                    MessageBox.Show(Constants.ErrorProgramFileNameIsFail, "Error", MessageBoxButton.OK);
                }
            });
            
        }
    }
}
