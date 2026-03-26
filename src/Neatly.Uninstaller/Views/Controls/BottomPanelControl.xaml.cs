using System.Windows.Controls;

namespace Neatly.Uninstaller.Views.Controls;

public partial class BottomPanelControl : UserControl
{
    
    public int AppCount => Uninstaller.Instance.GetInstalledApps().Count;
    
    public BottomPanelControl()
    {
        InitializeComponent();
        DataContext = this;
    }
}