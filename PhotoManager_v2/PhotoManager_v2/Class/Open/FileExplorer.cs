using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PhotoManager_v2.Class.Open
{
    class FileExplorer
    {
        private TextBox textBox = new TextBox();
        public FileExplorer(TextBox textBox)
        {
            this.textBox = textBox;
        }

        public async void OpenProgram()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "EXE (*.exe)|*.exe"; ;
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            if (openFileDialog.ShowDialog() == true)
            {
                textBox.Text = openFileDialog.FileName;
            }

        }

    }
}
