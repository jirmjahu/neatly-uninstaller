using System.Windows;

namespace Neatly.Uninstaller;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Uninstaller uninstaller = new();
    }
}