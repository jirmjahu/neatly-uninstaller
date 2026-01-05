using System.Collections.ObjectModel;
using System.Windows.Controls;
using neatly.uninstaller.Models;

namespace neatly.uninstaller.Views.Controls;

public partial class SidebarControl : UserControl
{
    
    public List<InstalledApp> Apps { get; } = Uninstaller.Instance.AppScanner.FindInstalledApps();
    
    public SidebarControl()
    {
        InitializeComponent();
        DataContext = this;
    }
}