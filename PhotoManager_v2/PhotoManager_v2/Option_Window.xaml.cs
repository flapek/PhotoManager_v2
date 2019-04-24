using PhotoManager_v2.Class.Open;
using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace PhotoManager_v2
{
    public partial class Option_Window : Window
    {
        private UserSettings userSettings = new UserSettings();
        private bool isDataDirty = false;

        public Option_Window()
        {
            InitializeComponent();
            SourceEditingProgramTextBox.Text = userSettings.PathToEditingProgram;
            SourceToMainFolderTextBox.Text = userSettings.PathToMainFolder;
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
                isDataDirty = false;
            }
        }          //skończone
        private void DefaultOptionButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(Constants.MessageBoxStringDefaultOption, Constants.CaptionNameOption, MessageBoxButton.YesNo);

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
            var filename = file.Open(Environment.SpecialFolder.MyComputer);
            if (file.verificate)
            {
                isDataDirty = true;
                SaveOptionButton.IsEnabled = true;
                SourceEditingProgramTextBox.Text = filename.FileName;
            }
        }
        private void SearchMainFolderPathButton_Click(object sender, RoutedEventArgs e)
        {
            FolderExplorer folder = new FolderExplorer();
            string path = folder.Open();

            if (!path.Equals(string.Empty))
            {
                SourceToMainFolderTextBox.Text = path;
                isDataDirty = true;
            }

        }
        private void OkOptionButton_Click(object sender, RoutedEventArgs e)
        {
            if (isDataDirty)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(Constants.MessageBoxStringClose, Constants.CaptionNameClose, MessageBoxButton.YesNo);
                if (MessageBoxResult.Yes == messageBoxResult)
                {
                    Close();
                }
            }
            else
                Close();
        }       //poprawić
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
        private void SourceEditingProgramTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(userSettings.PathToEditingProgram == SourceEditingProgramTextBox.Text &&
                userSettings.PathToMainFolder == SourceToMainFolderTextBox.Text))
            {
                SaveOptionButton.IsEnabled = true;
                isDataDirty = true;
            }
            else
            {
                SaveOptionButton.IsEnabled = false;
                isDataDirty = false;
            }
        }//poprawić funkcjonalność 
        private void SourceToMainFolderTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(userSettings.PathToMainFolder == SourceToMainFolderTextBox.Text &&
                userSettings.PathToEditingProgram == SourceEditingProgramTextBox.Text))
            {
                SaveOptionButton.IsEnabled = true;
                isDataDirty = true;
            }
            else
            {
                SaveOptionButton.IsEnabled = false;
                isDataDirty = false;
            }
        }//poprawić funkcjonalność
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (isDataDirty)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(Constants.MessageBoxStringClose, Constants.CaptionNameClose, MessageBoxButton.YesNo);
                if (MessageBoxResult.No == messageBoxResult)
                {
                    e.Cancel = true;
                }
            }
            else
                Close();
        }    //poprawić
    }
}
