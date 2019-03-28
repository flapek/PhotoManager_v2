using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
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
                    Slider.Children.Add(image);
                    Slider.Children.Add(label);
                }
            }
        }

        private  void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string pathToPS = @"C:\Users\filap\Desktop\pscs6\PhotoshopCS6Portable.exe";
            string pathToPaint = "mspaint.exe";

            try
            {
                ProcessStartInfo start_info = new ProcessStartInfo(pathToPS);      //jak ustawić otwieranie innego programu bez błędu              //ProcessStartInfo(Scieżka do otwieranego programu, plik który otwieramy od razu w programie)
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
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
        }
    }
}
