using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PhotoManager_v2.Class.Workers.Slider
{
    class Slider
    {
        private Image image = new Image();
        private Label label = new Label();
        private Grid grid = new Grid();

        public Slider()
        {
            Grid.SetRow(image, 0);
            Grid.SetRow(label, 1);

            grid.Margin = new Thickness(10);
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.Margin = new Thickness(5);
            image.Stretch = Stretch.Uniform;

            label.HorizontalContentAlignment = HorizontalAlignment.Center;

            grid.Children.Add(image);
            grid.Children.Add(label);
        }

        public void AddElement(string fileName, StackPanel stackPanel)
        {
            image.Source = new BitmapImage(new Uri(fileName));
            label.Content = Path.GetFileName(fileName);

            stackPanel.Children.Add(grid);
        }
    }
}
