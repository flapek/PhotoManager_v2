using PhotoManager_v2.Class.Open;
using System;
using System.Windows;


namespace PhotoManager_v2
{
    /// <summary>
    /// Logika interakcji dla klasy Option_Window.xaml
    /// </summary>
    public partial class Option_Window : Window
    {
        UserSettings userSettings = new UserSettings();

        bool isDataDirty = false;

        public Option_Window()
        {
            InitializeComponent();
            SourceEditingProgramTextBox.Text = userSettings.PathToEditingProgram;
            HintOpenProgramTextBlock.Text = Constants.OptionHintForOpenEditingProgram + "\n" + Constants.OptionHintForChoosePathToDirectory;
        }

        private void SaveOptionButton_Click(object sender, RoutedEventArgs e)
        {
            if (isDataDirty)
            {
                userSettings.PathToEditingProgram = SourceEditingProgramTextBox.Text;
                userSettings.PathToMainFolder = SourceToMainFolderTextBox.Text;
                userSettings.Save();
                SaveOptionButton.IsEnabled = false;
            }
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
        }       //skończone

        private void SearchProgramPathButton_Click(object sender, RoutedEventArgs e)
        {
            FileExplorer file = new FileExplorer("EXE (*.exe)|*.exe", false);
            file.Open(SourceEditingProgramTextBox, Environment.SpecialFolder.MyComputer);
            if (file.verificate)
            {
                isDataDirty = true;
                SaveOptionButton.IsEnabled = true;
            }

        }
        private void SearchMainFolderPathButton_Click(object sender, RoutedEventArgs e)
        {
            FileExplorer file = new FileExplorer("", false); //dokończyć ------ jak zrobić aby można było wybierać folder
            file.Open(SourceToMainFolderTextBox, Environment.SpecialFolder.UserProfile);
        }
        private void OkOptionButton_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }    /* Zrobić obsługę sprawdzającą czy jakieś zmiany nie zostały wprowadzone w opcjach. Jeżeli tak to powinno wyskakiwać okno dialogowe czy na pewno zapisać zmiany.*/
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
