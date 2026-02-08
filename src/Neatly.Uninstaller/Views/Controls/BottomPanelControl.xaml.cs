using System.Windows.Controls;

namespace Neatly.Uninstaller.Views.Controls;

public partial class BottomPanelControl : UserControl
{
    
    public int AppCount => Uninstaller.Instance.AppScanner.FindInstalledApps().Count;
    
    public BottomPanelControl()
    {
        InitializeComponent();
        DataContext = this;
    }
}