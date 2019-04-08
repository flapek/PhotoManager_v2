using PhotoManager_v2.Class.Open;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PhotoManager_v2
{
    /// <summary>
    /// Logika interakcji dla klasy Option_Window.xaml
    /// </summary>
    public partial class Option_Window : Window
    {
        UserSettings userSettings = new UserSettings();

        public Option_Window()
        {
            InitializeComponent();
            SourceEditingProgramTextBox.Text = userSettings.PathToEditingProgram;
            HintOpenProgramTextBlock.Text = Constants.OptionHintForOpenEditingProgram + "\n" + Constants.OptionHintForChoosePathToDirectory;
        }

        private void SaveOptionButton_Click(object sender, RoutedEventArgs e)
        {
            userSettings.PathToEditingProgram = SourceEditingProgramTextBox.Text;
            userSettings.PathToMainFolder = SourceToMainFolderTextBox.Text;
            userSettings.Save();
        }

        private void DefaultOptionButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want set default option?", Constants.CaptionNameOption, MessageBoxButton.YesNo);

            if (MessageBoxResult.Yes == messageBoxResult)
            {
                userSettings.Reset();
                SourceEditingProgramTextBox.Text = userSettings.PathToEditingProgram;
                SourceToMainFolderTextBox.Text = userSettings.PathToMainFolder;
            }
        }

        private void SearchProgramPathButton_Click(object sender, RoutedEventArgs e)
        {
            FileExplorer file = new FileExplorer(SourceEditingProgramTextBox, "EXE (*.exe)|*.exe", false);
            file.OpenProgram();
        }
        private void SearchMainFolderPathButton_Click(object sender, RoutedEventArgs e)
        {
            FileExplorer file = new FileExplorer(SourceToMainFolderTextBox); //dokończyć
            file.OpenProgram();
        }
        private void OkOptionButton_Click(object sender, RoutedEventArgs e)
        {
            /* Zrobić obsługę sprawdzającą czy jakieś zmiany nie zostały wprowadzone w opcjach. Jeżeli tak to powinno wyskakiwać okno dialogowe czy na pewno zapisać zmiany.*/
            this.Close();
        }

        private void GeneralSettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            GeneralOptionGrid.Visibility = Visibility.Visible;
            SourceOptionGrid.Visibility = Visibility.Hidden;
        }

        private void SourceSettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SourceOptionGrid.Visibility = Visibility.Visible;
            GeneralOptionGrid.Visibility = Visibility.Hidden;
        }


    }
}
