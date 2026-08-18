using System.Windows;
using Neatly.Uninstaller.App.Themes;

namespace Neatly.Uninstaller.App;

public partial class App : Application
{

    protected override void OnStartup(StartupEventArgs e)
    {
        var themeManager = new ThemeManager(Resources);
        themeManager.Set(Theme.Dark);

        base.OnStartup(e);
    }
}
