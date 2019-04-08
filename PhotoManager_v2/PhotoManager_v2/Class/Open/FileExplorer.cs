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
        private OpenFileDialog openFileDialog = new OpenFileDialog();
        public FileExplorer(TextBox textBox, string openFileDialogFilter,bool openFileDialogMultiselect)
        {
            this.textBox = textBox;
            openFileDialog.Filter = openFileDialogFilter;
            openFileDialog.Multiselect = openFileDialogMultiselect;
        }
        public FileExplorer(TextBox textBox) //jak zrobić aby można było wybierać folder zamiast pliku 
        {
            this.textBox = textBox;
            openFileDialog.Multiselect = false;
        }

        public void OpenProgram()
        {
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            if (openFileDialog.ShowDialog() == true)
            {
                textBox.Text = openFileDialog.FileName;
            }

        }

    }
}
