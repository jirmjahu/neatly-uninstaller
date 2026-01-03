using neatly.uninstaller.Models;

namespace neatly.uninstaller.Services;

public interface IAppScanner
{
    List<InstalledApp> FindInstalledApps();
}