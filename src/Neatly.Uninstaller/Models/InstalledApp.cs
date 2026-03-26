using System.Windows.Media;

namespace Neatly.Uninstaller.Models;

public class InstalledApp(
    string name,
    string? publisher,
    string? version,
    string? installLocation,
    string? uninstallCommand,
    string? displayIconPath,
    ImageSource? icon)
{
    public string Name { get; set; } = name;
    public string? Publisher { get; set; } = publisher;
    public string? Version { get; set; } = version;

    public string? InstallLocation { get; set; } = installLocation;
    public string? UninstallCommand { get; set; } = uninstallCommand;
    public string? DisplayIconPath { get; set; } = displayIconPath;

    public ImageSource? Icon { get; set; } = icon;
}