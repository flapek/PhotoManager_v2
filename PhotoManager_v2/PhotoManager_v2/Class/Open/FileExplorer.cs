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
        internal bool verificate = false;

        public FileExplorer(string openFileDialogFilter, bool openFileDialogMultiselect)
        {
            openFileDialog.Filter = openFileDialogFilter;
            openFileDialog.Multiselect = openFileDialogMultiselect;
        }
        public void Open(TextBox textBox, Environment.SpecialFolder specialFolder)
        {
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(specialFolder);
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            if (openFileDialog.ShowDialog() == true)
            {
                textBox.Text = openFileDialog.FileName;
            }

        }
        public OpenFileDialog Open(Environment.SpecialFolder specialFolder)
        {
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(specialFolder);
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            if (openFileDialog.ShowDialog() == true)
            {
                verificate = true;
                return openFileDialog;
            }
            else return null;
        }
    }
}
