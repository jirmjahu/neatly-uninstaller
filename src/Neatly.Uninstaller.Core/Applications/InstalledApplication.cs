namespace Neatly.Uninstaller.Core.Applications;

public sealed class InstalledApplication(
    string name,
    string? publisher,
    string? version,
    string? installLocation,
    string? uninstallCommand)
{
    public string Name { get; } = name;
    public string? Publisher { get; } = publisher;
    public string? Version { get; } = version;

    public string? InstallLocation { get; } = installLocation;
    public string? UninstallCommand { get; } = uninstallCommand;
}
