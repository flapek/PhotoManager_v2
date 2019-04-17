using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PhotoManager_v2.Class;
using PhotoManager_v2.Class.DirectoryTree;
using PhotoManager_v2.Class.Open;

namespace PhotoManager_v2
{
    public partial class MainWindow : Window
    {
        private Tree tree = new Tree();
        private UserSettings userSettings = new UserSettings();

        private Point? lastCenterPositionOnTarget;
        private Point? lastMousePositionOnTarget;
        private Point? lastDragPoint;

        private string pathToPhoto = @"C:\Users\filap\Desktop\_DSC8277.jpg";        //do usunięcia jak już nie będzie potrzebne
        public MainWindow()
        {
            InitializeComponent();
            tree.LoadDirectories(userSettings.PathToMainFolder, DirectoryTreeView);

            ImageHandlerScroolViewer.ScrollChanged += OnImageHandlerScroolViewerScrollChanged;
            ImageHandlerScroolViewer.MouseLeftButtonUp += OnMouseLeftButtonUp;
            ImageHandlerScroolViewer.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
            ImageHandlerScroolViewer.PreviewMouseWheel += OnPreviewMouseWheel;
            ImageHandlerScroolViewer.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
            ImageHandlerScroolViewer.MouseMove += OnMouseMove;
            ScrollViewerImage.ValueChanged += OnSliderValueChanged;

            ImageHandler.Source = new BitmapImage(new Uri(pathToPhoto));
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
            }           //jak zamykać wszystkie otwarte okna na raz
        }
        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            FileExplorer fileExplorer = new FileExplorer("JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png|TIFF (*.tiff)|*.tiff|All Files (*.*)|*.*", true);
            var files = fileExplorer.Open(Environment.SpecialFolder.Desktop);
            if (fileExplorer.verificate == true)
            {
                foreach (string fileName in files.FileNames)
                {
                    Class.Workers.Slider.Slider slider = new Class.Workers.Slider.Slider();
                    var picture = slider.AddElement(fileName, SliderStackPanel);
                    //picture.MouseLeftButtonDown += Image_MouseDown;//new MouseButtonEventHandler(Image_MouseDown);
                    //slider.ImageHandler.MouseEnter += BacklightSliderElement_MouseEnter;
                }
            }
        }
        private async void EditInOtherProgram_ClickAsync(object sender, RoutedEventArgs e)
        {
            await OpenEditingProgram.Open(pathToPhoto);        //Dodać event który będzie odwoływać się do ścieżki danego zdjęcia i będzie przekazywał do programu w którym będzie edytowane zdjęcie
        }
        private void OptionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Option_Window option = new Option_Window();
            option.Show();
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var v = sender.GetType().Name;
            FileInfo file = new FileInfo(v);

            //ImageHandler.Source = new BitmapImage(new Uri();

            MessageBoxResult message = MessageBox.Show(file.Attributes.ToString());

        }       //działa, dodać obsługo pobierającą ścieżke do zdjęcia aby wyświetlało się na panelu 


        double scale = 1;
        double scaleStep = 0.1;

        private void ZoomThePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            if (scale <= 2)
            {
                scale += scaleStep;
                ImageHandler.RenderTransform = new ScaleTransform(scale, scale);
            }
        }
        private void ZoomOutThePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            if (scale >= 0.2)
            {
                scale -= scaleStep;
                ImageHandler.RenderTransform = new ScaleTransform(scale, scale);
            }
        }

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
                ScrollViewerImage.Value += 0.2;
            }
            if (e.Delta < 0)
            {
                ScrollViewerImage.Value -= 0.2;
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
    }

}

