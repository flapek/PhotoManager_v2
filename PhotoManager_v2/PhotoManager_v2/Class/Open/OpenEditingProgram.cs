using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace PhotoManager_v2.Class
{
    class OpenEditingProgram
    {
        public static async Task Open(string pathToPhoto = null)
        {
            Process process = new Process();
            UserSettings userSettings = new UserSettings();
            await Task.Run(() =>
            {
                try
                {
                    process.StartInfo.FileName = userSettings.PathToEditingProgram;
                    process.StartInfo.Arguments = pathToPhoto;       //Dlaczego niektóre ścieżki do zdjęcia nie działają jak np.: @"C:\Users\filap\Desktop\zdjęcia\studenckie otrzęsiny 26-10-2018\_DSC8277.jpg"
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
