using PhotoManager_v2.Class.Photo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        public List<Folder> LoadDirectories(DirectoryInfo root)
        {
            DirectoryInfo[] directoryInfos = root.GetDirectories();
            List<Folder> folders = new List<Folder>();

            try
            {
                foreach (var directory in directoryInfos.OrderBy(x => x.Name))
                {
                    folders.Add(new Folder
                    {
                        FullName = directory.FullName,
                        Name = directory.Name
                    });
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return folders;
        }
        public List<Picture> LoadFile(DirectoryInfo root)
        {
            FileInfo[] files = null;
            List<Picture> pictures = new List<Picture>();

            try
            {
                //files = root.GetFiles("*.*");
                files = root.GetFiles("*.jpg");
                //files = root.GetFiles("*.png");
                //files = root.GetFiles("*.tiff");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (FileInfo file in files)
                {
                    pictures.Add(new Picture
                    {
                        FullName = file.FullName,
                        Name = file.Name
                    });
                }

                return pictures;
            }
            else return null;
        }

    }
}
