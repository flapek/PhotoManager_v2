﻿using Microsoft.Win32;
using System;
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
