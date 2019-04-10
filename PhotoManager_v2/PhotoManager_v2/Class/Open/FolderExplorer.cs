using System.Windows.Controls;

namespace PhotoManager_v2.Class.Open
{
    class FolderExplorer
    {
        public void Open(TextBox textBox)
        {
            System.Windows.Forms.FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();
            folder.ShowDialog();
            textBox.Text = folder.SelectedPath;
        }
    }
}
