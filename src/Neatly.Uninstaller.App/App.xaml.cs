using System.Windows;
using Neatly.Uninstaller.App.Themes;
using Neatly.Uninstaller.App.Views.Controls.Sidebar;
using Neatly.Uninstaller.Win32.Applications;

namespace Neatly.Uninstaller.App;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var themeManager = new ThemeManager(Resources);
        themeManager.Set(Theme.Dark);

        var applications = new RegistryApplicationScanner().Scan();
        var sidebarViewModel = new SidebarViewModel(applications);
        
        new MainWindow(sidebarViewModel).Show();
    }
}
