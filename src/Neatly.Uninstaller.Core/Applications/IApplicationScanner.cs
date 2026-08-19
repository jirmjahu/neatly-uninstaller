namespace Neatly.Uninstaller.Core.Applications;

public interface IApplicationScanner
{
    List<InstalledApplication> Scan();
}
