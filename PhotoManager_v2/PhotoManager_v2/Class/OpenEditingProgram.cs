﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace PhotoManager_v2.Class
{
    class OpenEditingProgram
    {       
        public static async Task Open()
        {
            Process process = new Process();
            UserSettings userSettings = new UserSettings();
            await Task.Run(() => {
                try
                {       
                    process.StartInfo.FileName = userSettings.PathToEditingProgram;
                    // process.StartInfo.Arguments = @"C:\Users\filap\Desktop\zdjęcia\studenckie otrzęsiny 26-10-2018\_DSC8277.jpg";       //ścieżka do zdjęcia 
                    process.Start();
                    process.WaitForExit();
                }
                catch (Exception)
                {
                    MessageBox.Show(Constants.ErrorProgramFileNameIsFail, "Error", MessageBoxButton.OK);
                }
            });
           
        }
    }
}
