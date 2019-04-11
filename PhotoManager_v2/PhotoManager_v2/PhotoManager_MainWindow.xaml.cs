using System;
using System.ComponentModel;
using System.Windows;
using PhotoManager_v2.Class;
using PhotoManager_v2.Class.DirectoryTree;
using PhotoManager_v2.Class.Open;
using PhotoManager_v2.Class.Workers.Slider;

namespace PhotoManager_v2
{
    public partial class MainWindow : Window
    {
        private Tree tree = new Tree();

        private string pathToPhoto = @"C:\Users\filap\Desktop\_DSC8277.jpg";        //do usunięcia jak już nie będzie potrzebne
        public MainWindow()
        {
            InitializeComponent();
            tree.LoadDirectories(@"C:\Users\filap\Desktop", DirectoryTreeView);       //błąd wywołania
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
                foreach (string filename in files.FileNames)
                {
                    Slider slider = new Slider();
                    slider.AddElement(filename, Slider);
                    slider.button.Click += ButtonWithPhoto_Click;
                }
            }
        }
        private async void EditInOtherProgramMenuItem_ClickAsync(object sender, RoutedEventArgs e)
        {
            await OpenEditingProgram.Open(pathToPhoto);        //Dodać event który będzie odwoływać się do ścieżki danego zdjęcia i będzie przekazywał do programu w którym będzie edytowane zdjęcie
        }
        private void OptionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Option_Window option = new Option_Window();
            option.Show();
        }

        private void ButtonWithPhoto_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("tak");
        }

    }
}
