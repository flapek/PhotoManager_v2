using System.Windows.Controls;

namespace PhotoManager_v2.Class.DirectoryTree
{
    partial class DirectoryTreeWithoutIcon
    {
        private class DummyTreeViewItem : TreeViewItem
        {
            public DummyTreeViewItem()
                : base()
            {
                Header = "Dummy";
                Tag = "Dummy";
            }
        }
    }
}
