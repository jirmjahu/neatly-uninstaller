using System.Windows.Media;

namespace neatly.uninstaller.Models;

public class InstalledApp
{
    
    public string Name { get;}
    public string Publisher { get; }
    public string Version { get; }
    public string InstallLocation { get; }
    public string UninstallCommand { get; }
    public string DisplayIconPath { get; }
    
    public ImageSource? Icon { get; set; }

    public InstalledApp(string name, string publisher, string version, string installLocation, string uninstallCommand, string displayIconPath)
    {
        Name = name;
        Publisher = publisher;
        Version = version;
        InstallLocation = installLocation;
        UninstallCommand = uninstallCommand;
        DisplayIconPath = displayIconPath; ;
    }
}