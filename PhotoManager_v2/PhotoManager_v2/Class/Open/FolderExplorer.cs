using System.Windows.Controls;

namespace PhotoManager_v2.Class.Open
{
    class FolderExplorer
    {
        public void Open(TextBox textBox)
        {
            System.Windows.Forms.FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult dialogResult  = folder.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                textBox.Text = folder.SelectedPath;
            } 
        }
    }
}
