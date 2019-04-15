using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PhotoManager_v2.Class.Photo;

namespace PhotoManager_v2.Class.Workers.Slider
{
    class Slider
    {
        internal Image image = new Image();
        private Label label = new Label();
        internal Grid grid = new Grid();
        internal Button button = new Button();

        public Slider()
        {
            grid.Margin = new Thickness(2);
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions[1].Height = new GridLength(30);
            Grid.SetRow(image, 0);
            Grid.SetRow(label, 1);

            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.Margin = new Thickness(3);
            image.Stretch = Stretch.Uniform;

            label.HorizontalContentAlignment = HorizontalAlignment.Center;

            grid.Children.Add(image);
            grid.Children.Add(label);
        }

        public Image AddElement(string fileName, StackPanel stackPanel)
        {
            //FileInfo fileInfo = new FileInfo(fileName);
            label.Content = Path.GetFileName(fileName);
            image.Source = new BitmapImage(new Uri(fileName));
            
            stackPanel.Children.Add(grid);
            return image;
        }
    }
}
