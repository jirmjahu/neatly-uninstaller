using System.Windows;
using neatly.uninstaller.Theming;

namespace neatly.uninstaller;

public partial class App : Application
{

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        Uninstaller uninstaller = new();
        
        uninstaller.ThemeManager.SetTheme(AppTheme.System);
    }
}