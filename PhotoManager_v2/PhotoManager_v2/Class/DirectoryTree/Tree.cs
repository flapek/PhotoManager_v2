﻿using System.Collections.Generic;
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
        public void LoadDirectories(string source, TreeView treeView)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(source);
                DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
                List<Folder> folders = new List<Folder>();

                foreach (var directory in directoryInfos.OrderBy(x => x.Name))
                {
                    folders.Add(new Folder
                    {
                        FullName = directory.FullName,
                        Name = directory.Name
                    });
                }

                foreach (Folder folder in folders)
                {
                    //treeView.Items.Add(folder.Name);
                    treeView.Items.Add(GetItem(folder));
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private TreeViewItem GetItem(Folder folder)
        {
            var item = new TreeViewItem
            {
                Header = folder.Name,
                DataContext = folder,
                Tag = folder,
            };
            //this.AddDummy(folder);
            //item.Expanded += new RoutedEventHandler(item_Expanded);
            return item;
        }


        //private TreeViewItem GetItem(DirectoryInfo info)
        //{
        //    var item = new TreeViewItem
        //    {
        //        Header = info.Name,
        //        DataContext = info,
        //        Tag = info,
        //    };
        //    this.AddDummy(item);
        //    item.Expanded += new RoutedEventHandler(item_Expanded);
        //    return item;
        //}
        //private TreeViewItem GetItem(FileInfo file)
        //{
        //    var item = new TreeViewItem
        //    {
        //        Header = file.Name,
        //        DataContext = file,
        //        Tag = file
        //    };
        //    return item;
        //}
        //private void AddDummy(TreeViewItem item)
        //{
        //    item.Items.Add(new DummyTreeViewItem());
        //}
        //private bool HasDummy(TreeViewItem item)
        //{
        //    return item.HasItems && (item.Items.OfType<TreeViewItem>().ToList().FindAll(tvi => tvi is DummyTreeViewItem).Count > 0);
        //}
        //private void RemoveDummy(TreeViewItem item)
        //{
        //    var dummies = item.Items.OfType<TreeViewItem>().ToList().FindAll(tvi => tvi is DummyTreeViewItem);
        //    foreach (var dummy in dummies)
        //    {
        //        item.Items.Remove(dummy);
        //    }
        //}
        //private void ExploreDirectories(TreeViewItem item)
        //{
        //    var directoryInfo = (DirectoryInfo)null;
        //    if (item.Tag is DriveInfo)
        //    {
        //        directoryInfo = ((DriveInfo)item.Tag).RootDirectory;
        //    }
        //    else if (item.Tag is DirectoryInfo)
        //    {
        //        directoryInfo = (DirectoryInfo)item.Tag;
        //    }
        //    else if (item.Tag is FileInfo)
        //    {
        //        directoryInfo = ((FileInfo)item.Tag).Directory;
        //    }
        //    if (ReferenceEquals(directoryInfo, null)) return;
        //    foreach (var directory in directoryInfo.GetDirectories())
        //    {
        //        var isHidden = (directory.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
        //        var isSystem = (directory.Attributes & FileAttributes.System) == FileAttributes.System;
        //        if (!isHidden && !isSystem)
        //        {
        //            item.Items.Add(GetItem(directory));
        //        }
        //    }
        //}
        //private void ExploreFiles(TreeViewItem item)
        //{
        //    var directoryInfo = (DirectoryInfo)null;
        //    if (item.Tag is DriveInfo)
        //    {
        //        directoryInfo = ((DriveInfo)item.Tag).RootDirectory;
        //    }
        //    else if (item.Tag is DirectoryInfo)
        //    {
        //        directoryInfo = (DirectoryInfo)item.Tag;
        //    }
        //    else if (item.Tag is FileInfo)
        //    {
        //        directoryInfo = ((FileInfo)item.Tag).Directory;
        //    }
        //    if (ReferenceEquals(directoryInfo, null)) return;
        //    foreach (var file in directoryInfo.GetFiles())
        //    {
        //        var isHidden = (file.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
        //        var isSystem = (file.Attributes & FileAttributes.System) == FileAttributes.System;
        //        if (!isHidden && !isSystem)
        //        {
        //            item.Items.Add(this.GetItem(file));
        //        }
        //    }
        //}
        //void item_Expanded(object sender, RoutedEventArgs e)
        //{
        //    var item = (TreeViewItem)sender;
        //    if (HasDummy(item))
        //    {
        //        Cursor = Cursors.Wait;
        //        RemoveDummy(item);
        //        ExploreDirectories(item);
        //        ExploreFiles(item);
        //        Cursor = Cursors.Arrow;
        //    }
        //}

    }
}