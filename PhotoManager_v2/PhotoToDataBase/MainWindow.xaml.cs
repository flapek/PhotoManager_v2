using Microsoft.Win32;
using PhotoToDataBase.data;
using PhotoToDataBase.Workers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhotoToDataBase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string fileName;
        private IQueryable<ImageInfo> imageInfos;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JPG|*jpg",
                Multiselect = false,
                ValidateNames = true,
            };

            if (openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.FileName;
                ImageHandler.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }


            //using (ImageEntities image = new ImageEntities())
            //{
            //    IQueryable<ImageInfo> data = image.ImageInfo.Where(i => i.Id == 1);
            //    foreach (ImageInfo item in data)
            //    {
            //        ImageHandler.Source = ImageConverter.ConvertByteArrayToBitmapImage(item.Image);
            //    }
            //}

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FileInfo file = new FileInfo(fileName);


            using (ImageEntities imageEntities = new ImageEntities())
            {
                ImageInfo my = new ImageInfo
                {
                    Name = file.Name,
                    ShortName = file.Name,
                    Image = ImageConverter.ConvertImageToByteArray(ImageHandler.Source as BitmapImage),
                    Text = "Zdjęcie"
                };
                imageEntities.ImageInfo.Add(my);
                await imageEntities.SaveChangesAsync();
                MessageBox.Show("Done", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            CollectionViewSource imageEntitiesViewSource = ((CollectionViewSource)(FindResource("imageEntitiesViewSource")));
        }
    }
}
