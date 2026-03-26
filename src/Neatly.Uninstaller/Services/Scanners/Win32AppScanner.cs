using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Neatly.Uninstaller.Helpers;
using Neatly.Uninstaller.Models;

namespace Neatly.Uninstaller.Services.Scanners;

public class Win32AppScanner : IAppScanner
{
    private const string FallbackIconPath =
        "pack://application:,,,/neatly.uninstaller;component/Resources/app_fallback.png";

    private readonly string[] _registryPaths =
    [
        @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
        @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
    ];

    private readonly string[] _programFolders =
    [
        Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
        Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Programs")
    ];

    public List<InstalledApp> Scan(List<InstalledApp> installedApps)
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
            if (string.IsNullOrWhiteSpace(name))
            {
                continue;
            }

            var publisher = subkey.GetValue("Publisher") as string;
            var version = subkey.GetValue("DisplayVersion") as string;
            var installLocation = subkey.GetValue("InstallLocation") as string;
            var uninstallString = subkey.GetValue("UninstallString") as string;
            var displayIcon = subkey.GetValue("DisplayIcon") as string;

            apps.Add(new InstalledApp(
                name,
                publisher,
                version,
                GetInstallLocation(name, installLocation),
                uninstallString,
                displayIcon,
                GetIcon(displayIcon, installLocation)
            ));
        }
    }

    private string? GetInstallLocation(string name, string? installLocation)
    {
        if (!string.IsNullOrWhiteSpace(installLocation))
        {
            return installLocation;
        }

        foreach (var folder in _programFolders)
        {
            if (!Directory.Exists(folder))
            {
                continue;
            }

            var appFolder = Directory.EnumerateDirectories(folder, "*", SearchOption.TopDirectoryOnly)
                .FirstOrDefault(dir => StringHelper.NormalizeString(Path.GetFileName(dir)).Contains(StringHelper.NormalizeString(name), StringComparison.OrdinalIgnoreCase));

            if (appFolder != null)
            {
                return appFolder;
            }

            appFolder = Directory.EnumerateDirectories(folder, "*", SearchOption.TopDirectoryOnly)
                .FirstOrDefault(dir => Path.GetFileName(dir).Contains(name.Split(" ")[0], StringComparison.OrdinalIgnoreCase));
            
            if (appFolder != null)
            {
                return appFolder;
            }
        }

        return null;
    }

    private ImageSource GetIcon(string filePath, string installLocation)
    {
        var image = ExtractIcon(filePath);
        if (image != null)
        {
            return image;
        }

        if (!string.IsNullOrWhiteSpace(installLocation) && Directory.Exists(installLocation))
        {
            // TODO: Maybe change to TopDirectoryOnly 
            var icoFile = Directory.EnumerateFiles(installLocation, "*.ico", SearchOption.AllDirectories)
                .FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(icoFile) && File.Exists(icoFile))
            {
                try
                {
                    return new BitmapImage(new Uri(icoFile));
                }
                catch
                {
                    // ignored
                }
            }

            var exeFile = Directory.EnumerateFiles(installLocation, "*.exe", SearchOption.AllDirectories)
                .FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(exeFile) && File.Exists(exeFile))
            {
                image = ExtractIcon(exeFile);
                if (image != null)
                {
                    return image;
                }
            }
        }

        return new BitmapImage(new Uri(FallbackIconPath));
    }

    private ImageSource? ExtractIcon(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return null;
        }

        // Remove commas from the filename
        var parts = filePath.Split(',');
        var finalName = parts[0].Trim();

        if (!File.Exists(finalName))
        {
            return null;
        }

        using var icon = Icon.ExtractAssociatedIcon(finalName);
        if (icon == null)
        {
            return null;
        }

        return Imaging.CreateBitmapSourceFromHIcon(
            icon.Handle,
            Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());
    }
}