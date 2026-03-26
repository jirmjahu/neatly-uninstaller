using Neatly.Uninstaller.Models;
using Neatly.Uninstaller.Services.Scanners;

namespace Neatly.Uninstaller.Services;

public static class AppScannerRunner
{
    private static readonly List<IAppScanner> AppScanners = [];

    public static void RegisterScanner(IAppScanner scanner)
    {
        AppScanners.Add(scanner);
    }

    public static List<InstalledApp> RunAll()
    {
        var apps = new List<InstalledApp>();

        foreach (var scanner in AppScanners)
        {
            foreach (var installedApp in scanner.Scan(apps))
            {
                apps.Add(installedApp);
            }
        }

        return apps;
    }
}