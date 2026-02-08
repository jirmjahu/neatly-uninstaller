using Neatly.Uninstaller.Models;

namespace Neatly.Uninstaller.Services;

public interface IAppScanner
{
    List<InstalledApp> FindInstalledApps();
}