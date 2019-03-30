using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace PhotoManager_v2
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //MessageBox.Show("Hello! \nThere you can organize your own photo.");
            LoadDirectories();
        }

        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            Image image = new Image();
            Label label = new Label();


            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png|TIFF (*.tiff)|*.tiff|All Files (*.*)|*.*"; ;
            openFileDialog.Multiselect = true;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.MaxHeight = 150;
            image.MinHeight = 60;
            image.MaxWidth = 150;
            image.MinWidth = 60;
            image.Stretch = Stretch.Uniform;



            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    image.Source = new BitmapImage(new Uri(filename));
                    label.Content = Path.GetFileName(filename);
                    Slider.Children.Add(image);                     //wywala błąd podczas dodawania więcej niż jednego zdjęcia
                    Slider.Children.Add(label);
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string pathToPS = @"C:\Users\filap\Desktop\pscs6\PhotoshopCS6Portable.exe";
            string pathToPaint = "mspaint.exe";

            try
            {
                ProcessStartInfo start_info = new ProcessStartInfo(pathToPS);       //ProcessStartInfo(Scieżka do otwieranego programu, plik który otwieramy od razu w programie)
                Process process = new Process();

                start_info.WindowStyle = ProcessWindowStyle.Maximized;
                process.StartInfo = start_info;

                process.Start();

                ShowInTaskbar = false;
                Hide();

                process.WaitForExit();

                ShowInTaskbar = true;
                Show();
            }
            catch (Exception)
            {
                MessageBox.Show(this, Constants.ErrorProgramFileNameIsFail, "Error", MessageBoxButton.OK);
            }
        }

        private void LoadDirectoryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DirectoryPathButton_Click(object sender, RoutedEventArgs e)
        {

        }



        /*------------------------------------------------------------------------------------------------------------------------*/
        /*                                              Przenieść do osobnej klasy                                              */
        private void LoadDirectories()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                treeView.Items.Add(GetItem(drive));
            }
        }

        private TreeViewItem GetItem(DriveInfo drive)
        {
            var item = new TreeViewItem
            {
                Header = drive.Name,
                DataContext = drive,
                Tag = drive
            };
            AddDummy(item);
            item.Expanded += new RoutedEventHandler(item_Expanded);
            return item;
        }
        public class DummyTreeViewItem : TreeViewItem
        {
            public DummyTreeViewItem()
                : base()
            {
                Header = "Dummy";
                Tag = "Dummy";
            }
        }

        private TreeViewItem GetItem(DirectoryInfo directory)
        {
            var item = new TreeViewItem
            {
                Header = directory.Name,
                DataContext = directory,
                Tag = directory
            };
            this.AddDummy(item);
            item.Expanded += new RoutedEventHandler(item_Expanded);
            return item;
        }

        private TreeViewItem GetItem(FileInfo file)
        {
            var item = new TreeViewItem
            {
                Header = file.Name,
                DataContext = file,
                Tag = file
            };
            return item;
        }

        private void AddDummy(TreeViewItem item)
        {
            item.Items.Add(new DummyTreeViewItem());
        }

        private bool HasDummy(TreeViewItem item)
        {
            return item.HasItems && (item.Items.OfType<TreeViewItem>().ToList().FindAll(tvi => tvi is DummyTreeViewItem).Count > 0);
        }

        private void RemoveDummy(TreeViewItem item)
        {
            var dummies = item.Items.OfType<TreeViewItem>().ToList().FindAll(tvi => tvi is DummyTreeViewItem);
            foreach (var dummy in dummies)
            {
                item.Items.Remove(dummy);
            }
        }
        private void ExploreDirectories(TreeViewItem item)
        {
            var directoryInfo = (DirectoryInfo)null;
            if (item.Tag is DriveInfo)
            {
                directoryInfo = ((DriveInfo)item.Tag).RootDirectory;
            }
            else if (item.Tag is DirectoryInfo)
            {
                directoryInfo = (DirectoryInfo)item.Tag;
            }
            else if (item.Tag is FileInfo)
            {
                directoryInfo = ((FileInfo)item.Tag).Directory;
            }
            if (ReferenceEquals(directoryInfo, null)) return;
            foreach (var directory in directoryInfo.GetDirectories())
            {
                var isHidden = (directory.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                var isSystem = (directory.Attributes & FileAttributes.System) == FileAttributes.System;
                if (!isHidden && !isSystem)
                {
                    item.Items.Add(GetItem(directory));
                }
            }
        }

        private void ExploreFiles(TreeViewItem item)
        {
            var directoryInfo = (DirectoryInfo)null;
            if (item.Tag is DriveInfo)
            {
                directoryInfo = ((DriveInfo)item.Tag).RootDirectory;
            }
            else if (item.Tag is DirectoryInfo)
            {
                directoryInfo = (DirectoryInfo)item.Tag;
            }
            else if (item.Tag is FileInfo)
            {
                directoryInfo = ((FileInfo)item.Tag).Directory;
            }
            if (ReferenceEquals(directoryInfo, null)) return;
            foreach (var file in directoryInfo.GetFiles())
            {
                var isHidden = (file.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                var isSystem = (file.Attributes & FileAttributes.System) == FileAttributes.System;
                if (!isHidden && !isSystem)
                {
                    item.Items.Add(this.GetItem(file));
                }
            }
        }

        void item_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;
            if (HasDummy(item))
            {
                Cursor = Cursors.Wait;
                RemoveDummy(item);
                ExploreDirectories(item);
                ExploreFiles(item);
                Cursor = Cursors.Arrow;
            }
        }

        /*------------------------------------------------------------------------------------------------------------------------*/
        //public class FileSystemObjectInfo : BaseObject
        //{
        //    public FileSystemObjectInfo(FileSystemInfo info)
        //    {
        //    }

        //    public FileSystemObjectInfo(DriveInfo drive)
        //        : this(drive.RootDirectory)
        //    {
        //    }
        //}

        //private class DummyFileSystemObjectInfo : FileSystemObjectInfo
        //{
        //    public DummyFileSystemObjectInfo()
        //        : base(new DirectoryInfo("DummyFileSystemObjectInfo"))
        //    {
        //    }
        //}

        //public ObservableCollection<FileSystemObjectInfo> Children
        //{
        //    get { return base.GetValue<ObservableCollection<FileSystemObjectInfo>>("Children"); }
        //    private set { base.SetValue("Children", value); }
        //}

        //public ImageSource ImageSource
        //{
        //    get { return base.GetValue<ImageSource>("ImageSource"); }
        //    private set { base.SetValue("ImageSource", value); }
        //}

        //public bool IsExpanded
        //{
        //    get { return base.GetValue<bool>("IsExpanded"); }
        //    set { base.SetValue("IsExpanded", value); }
        //}

        //public FileSystemInfo FileSystemInfo
        //{
        //    get { return base.GetValue<FileSystemInfo>("FileSystemInfo"); }
        //    private set { base.SetValue("FileSystemInfo", value); }
        //}

        //private DriveInfo Drive
        //{
        //    get { return base.GetValue<DriveInfo>("Drive"); }
        //    set { base.SetValue("Drive", value); }
        //}

        //private void AddDummy()
        //{
        //    this.Children.Add(new DummyFileSystemObjectInfo());
        //}

        //private bool HasDummy()
        //{
        //    return !object.ReferenceEquals(this.GetDummy(), null);
        //}

        //private DummyFileSystemObjectInfo GetDummy()
        //{
        //    var list = this.Children.OfType<DummyFileSystemObjectInfo>().ToList();
        //    if (list.Count > 0) return list.First();
        //    return null;
        //}

        //private void RemoveDummy()
        //{
        //    this.Children.Remove(this.GetDummy());
        //}

        //public FileSystemObjectInfo(FileSystemInfo info)
        //{
        //    this.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(FileSystemObjectInfo_PropertyChanged);
        //}

        //void FileSystemObjectInfo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (this.FileSystemInfo is DirectoryInfo)
        //    {
        //        if (string.Equals(e.PropertyName, "IsExpanded", StringComparison.CurrentCultureIgnoreCase))
        //        {
        //            if (this.IsExpanded)
        //            {
        //                this.ImageSource = FolderManager.GetImageSource(this.FileSystemInfo.FullName, ItemState.Open);
        //                if (this.HasDummy())
        //                {
        //                    this.RemoveDummy();
        //                    this.ExploreDirectories();
        //                    this.ExploreFiles();
        //                }
        //            }
        //            else
        //            {
        //                this.ImageSource = FolderManager.GetImageSource(this.FileSystemInfo.FullName, ItemState.Close);
        //            }
        //        }
        //    }
        //}

        //private void ExploreDirectories()
        //{
        //    if (!object.ReferenceEquals(this.Drive, null))
        //    {
        //        if (!this.Drive.IsReady) return;
        //    }
        //    try
        //    {
        //        if (this.FileSystemInfo is DirectoryInfo)
        //        {
        //            var directories = ((DirectoryInfo)this.FileSystemInfo).GetDirectories();
        //            foreach (var directory in directories.OrderBy(d => d.Name))
        //            {
        //                if (!object.Equals((directory.Attributes & FileAttributes.System), FileAttributes.System) &&
        //                    !object.Equals((directory.Attributes & FileAttributes.Hidden), FileAttributes.Hidden))
        //                {
        //                    this.Children.Add(new FileSystemObjectInfo(directory));
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        /*throw;*/
        //    }
        //}

        //private void ExploreFiles()
        //{
        //    if (!object.ReferenceEquals(this.Drive, null))
        //    {
        //        if (!this.Drive.IsReady) return;
        //    }
        //    try
        //    {
        //        if (this.FileSystemInfo is DirectoryInfo)
        //        {
        //            var files = ((DirectoryInfo)this.FileSystemInfo).GetFiles();
        //            foreach (var file in files.OrderBy(d => d.Name))
        //            {
        //                if (!object.Equals((file.Attributes & FileAttributes.System), FileAttributes.System) &&
        //                    !object.Equals((file.Attributes & FileAttributes.Hidden), FileAttributes.Hidden))
        //                {
        //                    this.Children.Add(new FileSystemObjectInfo(file));
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        /*throw;*/
        //    }
        //}

        //public FileSystemObjectInfo(FileSystemInfo info)
        //{
        //    if (this is DummyFileSystemObjectInfo) return;
        //    this.Children = new ObservableCollection<FileSystemObjectInfo>();
        //    this.FileSystemInfo = info;
        //    if (info is DirectoryInfo)
        //    {
        //        this.ImageSource = FolderManager.GetImageSource(info.FullName, ItemState.Close);
        //        this.AddDummy();
        //    }
        //    else if (info is FileInfo)
        //    {
        //        this.ImageSource = FileManager.GetImageSource(info.FullName);
        //    }
        //    this.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(FileSystemObjectInfo_PropertyChanged);
        //}
        /*------------------------------------------------------------------------------------------------------------------------*/
    }
}
