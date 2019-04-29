using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PhotoManager_v2.Class.Workers.Slider
{
    class SliderElement
    {
        public Image image = new Image();
        private Label label = new Label();
        private Grid grid = new Grid();
        private Button button = new Button();

        public Grid AddElement(string fileName)
        {
            grid.Margin = new Thickness(2);
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions[1].Height = new GridLength(30);
            Grid.SetRow(image, 0);
            Grid.SetRow(label, 1);

            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;
            image.Margin = new Thickness(3);
            image.Stretch = Stretch.Uniform;

            label.HorizontalContentAlignment = HorizontalAlignment.Center;

            grid.Children.Add(image);
            grid.Children.Add(label);

            label.Content = Path.GetFileName(fileName);
            image.Source = new BitmapImage(new Uri(fileName));

            return grid;
        }
    }
}
