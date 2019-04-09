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
    class SliderElements
    {
        public void SliderElement(string fileName,StackPanel stackPanel)
        {
            
                Image image = new Image();
                Label label = new Label();
                Grid grid = new Grid();
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

                image.Source = new BitmapImage(new Uri(fileName));
                label.Content = Path.GetFileName(fileName);

                stackPanel.Children.Add(grid);
        }
    }
}
