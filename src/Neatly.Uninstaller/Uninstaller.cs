using System.IO.Packaging;
using Neatly.Uninstaller.Models;
using Neatly.Uninstaller.Services;
using Neatly.Uninstaller.Services.Scanners;
using Neatly.Uninstaller.Theming;

namespace Neatly.Uninstaller;

public class Uninstaller
{
    public static Uninstaller Instance;

    public ThemeManager ThemeManager { get; }

    private List<InstalledApp> InstalledApps { get; }

    public Uninstaller()
    {
        Instance = this;
        ThemeManager = new ThemeManager();
        ThemeManager.SetTheme(AppTheme.Dark);

        AppScannerRunner.RegisterScanner(new Win32AppScanner());

        InstalledApps = AppScannerRunner.RunAll();
    }

    public List<InstalledApp> GetInstalledApps()
    {
        return InstalledApps;
    }
}