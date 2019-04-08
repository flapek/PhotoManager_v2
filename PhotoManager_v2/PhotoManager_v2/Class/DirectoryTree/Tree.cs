using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PhotoManager_v2.Class.DirectoryTree
{
    class Tree
    {
        public async Task DeleteTreeItem(string source)
        {
            await Task.Run(() =>
            {
                try
                {
                    string[] pathListItems = Directory.GetFiles(source, "*.*");

                    foreach (var item in pathListItems)
                    {
                        File.Delete(item);
                    }
                }
                catch (DirectoryNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
    }
}
