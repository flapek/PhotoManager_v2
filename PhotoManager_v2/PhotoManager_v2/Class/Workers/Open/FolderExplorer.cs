using System.Windows.Controls;

namespace PhotoManager_v2.Class.Open
{
    class FolderExplorer
    {
        public string Open()
        {
            System.Windows.Forms.FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult dialogResult  = folder.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                return folder.SelectedPath;
            }
            else return string.Empty;
        }
    }
}
