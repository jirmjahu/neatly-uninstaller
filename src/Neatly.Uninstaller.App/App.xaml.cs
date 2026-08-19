using System.Windows;
using Neatly.Uninstaller.App.Themes;
using Neatly.Uninstaller.Win32.Applications;

namespace Neatly.Uninstaller.App;

public partial class App : Application
{
    
    protected override void OnStartup(StartupEventArgs e)
    {
        var themeManager = new ThemeManager(Resources);
        themeManager.Set(Theme.Dark);

        var scanner = new RegistryApplicationScanner();
        
        foreach (var installedApplication in scanner.Scan())
        {
            Console.WriteLine(installedApplication.Name);           
        }

        base.OnStartup(e);
    }
}
