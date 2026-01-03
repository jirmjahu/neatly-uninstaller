using Microsoft.Win32;
using neatly.uninstaller.Models;

namespace neatly.uninstaller.Services;

public class Win32AppScanner : IAppScanner
{
    
    private readonly string[] _registryPaths =
    [
        @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
        @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
    ];
    
    public List<InstalledApp> FindInstalledApps()
    {
        var apps = new List<InstalledApp>();

        foreach (var path in _registryPaths)
        {
            ReadRegistryKey(apps, Registry.LocalMachine.OpenSubKey(path));
        }

        foreach (var path in _registryPaths)
        {
            ReadRegistryKey(apps, Registry.CurrentUser.OpenSubKey(path));
        }

        return apps;
    }
    
    private void ReadRegistryKey(List<InstalledApp> apps, RegistryKey? key)
    {
        if (key == null)
        {
            return;
        }

        foreach (var sub in key.GetSubKeyNames())
        {
            using var subkey = key.OpenSubKey(sub);
            if (subkey == null)
            {
                continue;
            }

            var name = subkey.GetValue("DisplayName") as string;
            if (string.IsNullOrEmpty(name))
            {
                continue;
            }

            var publisher = subkey.GetValue("Publisher") as string;
            var version = subkey.GetValue("DisplayVersion") as string;
            var installLocation = subkey.GetValue("InstallLocation") as string;
            var uninstallString = subkey.GetValue("UninstallString") as string;
            var displayIcon = subkey.GetValue("DisplayIcon") as string; 
            
            apps.Add(new InstalledApp(name, publisher, version, installLocation, uninstallString, displayIcon));
        }
    }
}