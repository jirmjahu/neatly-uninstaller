using Neatly.Uninstaller.Models;

namespace Neatly.Uninstaller.Services.Scanners;

public interface IAppScanner
{
    List<InstalledApp> Scan(List<InstalledApp> installedApps);
}