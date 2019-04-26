using PhotoManager_v2.Class.Open;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Slider
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            FileExplorer fileExplorer = new FileExplorer("JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png|TIFF (*.tiff)|*.tiff", true);
            var files = fileExplorer.Open(Environment.SpecialFolder.Desktop);
            if (fileExplorer.verificate == true)
            {
                foreach (var fileName in files.FileNames)
                {
                    FileInfo file = new FileInfo(fileName);

                    Image image = new Image();

                    image.HorizontalAlignment = HorizontalAlignment.Center;
                    image.VerticalAlignment = VerticalAlignment.Center;
                    image.Margin = new Thickness(3);
                    image.Stretch = Stretch.Uniform;

                    image.Source = new BitmapImage(new Uri(file.FullName));

                    SliderStackPanel.Children.Add(image);
                }
            }
        }       //dokończyć
    }
}
