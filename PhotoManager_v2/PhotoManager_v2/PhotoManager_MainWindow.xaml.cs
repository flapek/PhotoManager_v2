﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PhotoManager_v2.Class;
using PhotoManager_v2.Class.Open;
using PhotoManager_v2.Class.Workers;

namespace PhotoManager_v2
{
    public partial class MainWindow : Window
    {
        #region Variables

        /// <summary>
        /// All local variables
        /// </summary>

        private UserSettings userSettings = new UserSettings();

        private Point? lastCenterPositionOnTarget;
        private Point? lastMousePositionOnTarget;
        private Point? lastDragPoint;

        double scale = 1;
        double scaleStep = 0.1;

        private string PathToImage { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //LoadDirectoriesTree();

            ImageHandlerScroolViewer.ScrollChanged += OnImageHandlerScroolViewerScrollChanged;
            ImageHandlerScroolViewer.MouseLeftButtonUp += OnMouseLeftButtonUp;
            ImageHandlerScroolViewer.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
            ImageHandlerScroolViewer.PreviewMouseWheel += OnPreviewMouseWheel;
            ImageHandlerScroolViewer.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
            ImageHandlerScroolViewer.MouseMove += OnMouseMove;
            ScrollViewerImage.ValueChanged += OnSliderValueChanged;

            ZoomThePhotoButton.MouseLeftButtonDown += ZoomThePhotoButton_Click;
            ZoomThePhotoButton.PreviewMouseLeftButtonDown += ZoomThePhotoButton_Click;

            PathToImage = @"C:\Users\filap\Desktop\_DSC8277.jpg";
            ImageHandler.Source = new BitmapImage(new Uri(PathToImage));
        }
        #endregion

        #region On Loaded

        /// <summary>
        /// When the application first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Get every logical drive on the machine
            foreach (string folderName in Directory.GetDirectories(userSettings.PathToMainFolder))
            {
                // Create a new item for it
                TreeViewItem item = new TreeViewItem()
                {
                    // Set the header
                    Header = GetName.GetFileFolderName(folderName),
                    // And the full path
                    Tag = folderName
                };

                // Add a dummy item
                item.Items.Add(null);

                // Listen out for item being expanded
                item.Expanded += Folder_Expanded;

                // Add it to the main tree-view
                FolderView.Items.Add(item);

            }
        }

        #endregion

        #region Folder Expanded

        /// <summary>
        /// When a folder is expanded, find the sub folders/files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial Checks

            TreeViewItem item = (TreeViewItem)sender;

            // If the item only contains the dummy data
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            // Clear dummy data
            item.Items.Clear();

            // Get full path
            string fullPath = (string)item.Tag;

            #endregion

            #region Get Folders

            // Create a blank list for directories
            var directories = new List<string>();

            // Try and get directories from the folder
            // ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch { }

            // For each directory...
            directories.ForEach(directoryPath =>
            {
                // Create directory item
                var subItem = new TreeViewItem()
                {
                    // Set header as folder name
                    Header = GetName.GetFileFolderName(directoryPath),
                    // And tag as full path
                    Tag = directoryPath
                };

                // Add dummy item so we can expand folder
                subItem.Items.Add(null);

                // Handle expanding
                subItem.Expanded += Folder_Expanded;

                // Add this item to the parent
                item.Items.Add(subItem);
            });

            #endregion

            #region Get Files

            // Create a blank list for files
            var files = new List<string>();

            // Try and get files from the folder
            // ignoring any issues doing so
            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    files.AddRange(fs);
            }
            catch { }

            // For each file...
            files.ForEach(filePath =>
            {
                // Create file item
                var subItem = new TreeViewItem()
                {
                    // Set header as file name
                    Header = GetName.GetFileFolderName(filePath),
                    // And tag as full path
                    Tag = filePath
                };

                // Add this item to the parent
                item.Items.Add(subItem);
            });

            #endregion
        }

        #endregion

        #region Helpers

        

        #endregion

        #region On Close

        /// <summary>
        /// When the application is closing
        /// </summary>
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
        }                    //jak zamykać wszystkie otwarte okna na raz

        #endregion

        #region Option

        /// <summary>
        /// When user want change option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void OptionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Option_Window option = new Option_Window();
            option.Show();
        }   //skończone

        #endregion

        #region Image Handler Events

        /// <summary>
        /// When user interact with image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (lastDragPoint.HasValue)
            {
                Point posNow = e.GetPosition(ImageHandlerScroolViewer);

                double dX = posNow.X - lastDragPoint.Value.X;
                double dY = posNow.Y - lastDragPoint.Value.Y;

                lastDragPoint = posNow;

                ImageHandlerScroolViewer.ScrollToHorizontalOffset(ImageHandlerScroolViewer.HorizontalOffset - dX);
                ImageHandlerScroolViewer.ScrollToVerticalOffset(ImageHandlerScroolViewer.VerticalOffset - dY);
            }
        }
        void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition(ImageHandlerScroolViewer);
            if (mousePos.X <= ImageHandlerScroolViewer.ViewportWidth && mousePos.Y <
                ImageHandlerScroolViewer.ViewportHeight) //make sure we still can use the scrollbars
            {
                ImageHandlerScroolViewer.Cursor = Cursors.SizeAll;
                lastDragPoint = mousePos;
                Mouse.Capture(ImageHandlerScroolViewer);
            }
        }
        void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            lastMousePositionOnTarget = Mouse.GetPosition(ImageHandler);

            if (e.Delta > 0)
            {
                ScrollViewerImage.Value += scaleStep;
            }
            if (e.Delta < 0)
            {
                ScrollViewerImage.Value -= scaleStep;
            }

            e.Handled = true;
        }
        void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ImageHandlerScroolViewer.Cursor = Cursors.Arrow;
            ImageHandlerScroolViewer.ReleaseMouseCapture();
            lastDragPoint = null;
        }
        void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ScaleTransformImage.ScaleX = e.NewValue;
            ScaleTransformImage.ScaleY = e.NewValue;

            var centerOfViewport = new Point(ScrollViewerImage.Width / 2,
                                             ScrollViewerImage.Height / 2);
            lastCenterPositionOnTarget = ScrollViewerImage.TranslatePoint(centerOfViewport, ImageHandler);
        }
        void OnImageHandlerScroolViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0 || e.ExtentWidthChange != 0)
            {
                Point? targetBefore = null;
                Point? targetNow = null;

                if (!lastMousePositionOnTarget.HasValue)
                {
                    if (lastCenterPositionOnTarget.HasValue)
                    {
                        var centerOfViewport = new Point(ImageHandlerScroolViewer.ViewportWidth / 2,
                                                         ImageHandlerScroolViewer.ViewportHeight / 2);
                        Point centerOfTargetNow =
                              ImageHandlerScroolViewer.TranslatePoint(centerOfViewport, ImageHandler);

                        targetBefore = lastCenterPositionOnTarget;
                        targetNow = centerOfTargetNow;
                    }
                }
                else
                {
                    targetBefore = lastMousePositionOnTarget;
                    targetNow = Mouse.GetPosition(ImageHandler);

                    lastMousePositionOnTarget = null;
                }

                if (targetBefore.HasValue)
                {
                    double dXInTargetPixels = targetNow.Value.X - targetBefore.Value.X;
                    double dYInTargetPixels = targetNow.Value.Y - targetBefore.Value.Y;

                    double multiplicatorX = e.ExtentWidth / ImageHandler.Width;
                    double multiplicatorY = e.ExtentHeight / ImageHandler.Height;

                    double newOffsetX = ImageHandlerScroolViewer.HorizontalOffset -
                                        dXInTargetPixels * multiplicatorX;
                    double newOffsetY = ImageHandlerScroolViewer.VerticalOffset -
                                        dYInTargetPixels * multiplicatorY;

                    if (double.IsNaN(newOffsetX) || double.IsNaN(newOffsetY))
                    {
                        return;
                    }

                    ImageHandlerScroolViewer.ScrollToHorizontalOffset(newOffsetX);
                    ImageHandlerScroolViewer.ScrollToVerticalOffset(newOffsetY);
                }
            }
        }
        private void ZoomThePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            if (scale <= 2)
            {
                ScaleTransformImage.ScaleX = scale;
                ScaleTransformImage.ScaleY = scale;
                scale += scaleStep;
                ScrollViewerImage.Value += scaleStep;
            }
        }
        private void ZoomOutThePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            if (scale >= 0.2)
            {
                ScaleTransformImage.ScaleX = scale;
                ScaleTransformImage.ScaleY = scale;
                scale -= scaleStep;
                ScrollViewerImage.Value -= scaleStep;
            }
        }
        private void RotateRightPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            RotateTransformImage.Angle += 90;
        }
        private void RotateLeftPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            RotateTransformImage.Angle -= 90;
        }

        #endregion

        #region Open Editing Program

        /// <summary>
        /// When user want edit photo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private async void EditInOtherProgram_ClickAsync(object sender, RoutedEventArgs e)
        {
            await OpenEditingProgram.Open(PathToImage);
        }

        #endregion


        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            FileExplorer fileExplorer = new FileExplorer("JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png|TIFF (*.tiff)|*.tiff", true);
            var files = fileExplorer.Open(Environment.SpecialFolder.Desktop);
            if (fileExplorer.verificate == true)
            {
                foreach (string fileName in files.FileNames)
                {
                    //Class.Workers.Slider.Slider slider = new Class.Workers.Slider.Slider();
                    //Grid imageSliderHandler = slider.AddElement(fileName);
                    Image image = new Image();

                    image.HorizontalAlignment = HorizontalAlignment.Center;
                    image.VerticalAlignment = VerticalAlignment.Center;
                    image.Margin = new Thickness(3);
                    image.Stretch = Stretch.Uniform;

                    image.Source = new BitmapImage(new Uri(fileName));

                    SliderStackPanel.Children.Add(image);
                    //picture.MouseLeftButtonDown += Image_MouseDown;//new MouseButtonEventHandler(Image_MouseDown);
                    //slider.ImageHandler.MouseEnter += BacklightSliderElement_MouseEnter;
                }
            }
        }       //dokończyć
    }
}

