using Microsoft.Win32;
using Neatly.Uninstaller.Core.Applications;

namespace Neatly.Uninstaller.Win32.Applications;

public sealed class RegistryApplicationScanner : IApplicationScanner
{
    private static readonly string[] RegistryPaths =
    [
        @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
        @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
    ];

    public List<InstalledApplication> Scan()
    {
        var applications = new List<InstalledApplication>();

        foreach (var path in RegistryPaths)
        {
            ReadApplications(applications, Registry.LocalMachine.OpenSubKey(path));
        }

        foreach (var path in RegistryPaths)
        {
            ReadApplications(applications, Registry.CurrentUser.OpenSubKey(path));
        }

        return applications;
    }

    private static void ReadApplications(List<InstalledApplication> applications, RegistryKey? uninstallKey)
    {
        if (uninstallKey == null)
        {
            return;
        }

        using var key = uninstallKey;

        foreach (var subKeyName in key.GetSubKeyNames())
        {
            using var applicationKey = key.OpenSubKey(subKeyName);
            if (applicationKey is null)
            {
                continue;
            }

            var name = ReadString(applicationKey, "DisplayName");
            if (name is null)
            {
                continue;
            }

            applications.Add(new InstalledApplication(
                name,
                ReadString(applicationKey, "Publisher"),
                ReadString(applicationKey, "DisplayVersion"),
                ReadString(applicationKey, "InstallLocation"),
                ReadString(applicationKey, "UninstallString")));
        }
    }

    private static string? ReadString(RegistryKey key, string valueName)
    {
        return key.GetValue(valueName) is string value && !string.IsNullOrWhiteSpace(value) ? value.Trim() : null;
    }
}
