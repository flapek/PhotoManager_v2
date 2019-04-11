using System;
using System.IO;
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
        internal Button button = new Button();

        public Slider()
        {
            Grid.SetRow(button, 0);
            Grid.SetRow(label, 1);

            grid.Margin = new Thickness(10);
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.Margin = new Thickness(5);
            image.Stretch = Stretch.Uniform;

            label.HorizontalContentAlignment = HorizontalAlignment.Center;

            grid.Children.Add(button);
            grid.Children.Add(label);
        }

        public void AddElement(string fileName, StackPanel stackPanel)
        {
            label.Content = Path.GetFileName(fileName);
            button.Background = new ImageBrush(image.Source = new BitmapImage(new Uri(fileName)));

            stackPanel.Children.Add(grid);
        }
    }
}
