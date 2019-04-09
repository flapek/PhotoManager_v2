using System;
using System.ComponentModel;
using System.Windows;
using PhotoManager_v2.Class;
using PhotoManager_v2.Class.DirectoryTree;
using PhotoManager_v2.Class.Open;
using PhotoManager_v2.Class.Workers.Slider;

namespace PhotoManager_v2
{
    public partial class MainWindow : Window
    {
        private Tree tree = new Tree();
        private string pathToPhoto = @"C:\Users\filap\Desktop\_DSC8277.jpg";        //do usunięcia jak już nie będzie potrzebne
        public MainWindow()
        {
            InitializeComponent();
            //LoadDirectories();
            tree.LoadDirectories(@"C:\Users\filap\Desktop", DirectoryTreeView);       //błąd wywołania
        }
        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want close app?", "Close", MessageBoxButton.YesNo);
            if (MessageBoxResult.No == result)
            {
                e.Cancel = true;
            }
            else
            {
                WindowCollection windowCollection = OwnedWindows;
                foreach (Window win in windowCollection)
                {
                    win.Close();
                }
            }

        }
        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            FileExplorer fileExplorer = new FileExplorer("JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png|TIFF (*.tiff)|*.tiff|All Files (*.*)|*.*", true);
            var files = fileExplorer.Open(Environment.SpecialFolder.Desktop);
            if (fileExplorer.verificate == true)
            {
                foreach (string filename in files.FileNames)
                {
                    SliderElements sliderElements = new SliderElements();
                    sliderElements.SliderElement(filename, Slider);
                }
            }

        }
        private async void EditInOtherProgramMenuItem_ClickAsync(object sender, RoutedEventArgs e)
        {
            await OpenEditingProgram.Open(pathToPhoto);        //Dodać event który będzie odwoływać się do ścieżki danego zdjęcia i będzie przekazywał do programu w którym będzie edytowane zdjęcie
        }
        private void OptionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Option_Window option = new Option_Window();
            option.Show();
        }




        /*------------------------------------------------------------------------------------------------------------------------*/
        /*                                              Przenieść do osobnej klasy                                              */
        //private void LoadDirectories()
        //{
        //    DriveInfo[] drives = DriveInfo.GetDrives();
        //    foreach (var drive in drives)
        //    {
        //        DirectoryTreeView.Items.Add(GetItem(drive));
        //    }
        //}
        //private TreeViewItem GetItem(DriveInfo drive)
        //{
        //    var item = new TreeViewItem
        //    {
        //        Header = drive.Name,
        //        DataContext = drive,
        //        Tag = drive
        //    };
        //    AddDummy(item);
        //    item.Expanded += new RoutedEventHandler(item_Expanded);
        //    return item;
        //}
        //public class DummyTreeViewItem : TreeViewItem
        //{
        //    public DummyTreeViewItem()
        //        : base()
        //    {
        //        Header = "Dummy";
        //        Tag = "Dummy";
        //    }
        //}
        //private TreeViewItem GetItem(DirectoryInfo directory)
        //{
        //    var item = new TreeViewItem
        //    {
        //        Header = directory.Name,
        //        DataContext = directory,
        //        Tag = directory
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
        /*------------------------------------------------------------------------------------------------------------------------*/
    }
}
