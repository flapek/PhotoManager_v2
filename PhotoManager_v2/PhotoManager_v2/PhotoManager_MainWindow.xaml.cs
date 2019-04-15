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
using PhotoManager_v2.Class.Workers.Slider;

namespace PhotoManager_v2
{
    public partial class MainWindow : Window
    {
        private Tree tree = new Tree();
        private UserSettings userSettings = new UserSettings();

        private string pathToPhoto = @"C:\Users\filap\Desktop\_DSC8277.jpg";        //do usunięcia jak już nie będzie potrzebne
        public MainWindow()
        {
            InitializeComponent();
            tree.LoadDirectories(userSettings.PathToMainFolder, DirectoryTreeView);
            //ImageHandler.Source = new BitmapImage(new Uri(pathToPhoto));
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
                    //Slider slider = new Slider();
                    //var picture = slider.AddElement(fileName, SliderStackPanel);
                    //picture.MouseLeftButtonDown += Image_MouseDown;//new MouseButtonEventHandler(Image_MouseDown);
                    //slider.grid.MouseEnter += BacklightSliderElement_MouseEnter;

                    Image image = new Image();
                    Label label = new Label();
                    Grid grid = new Grid();
                    Button button = new Button();

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

                    image.MouseDown += Image_MouseDown;

                    SliderStackPanel.Children.Add(grid);
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

    }
}
