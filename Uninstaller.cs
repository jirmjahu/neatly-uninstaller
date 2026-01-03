using neatly.uninstaller.Services;

namespace neatly.uninstaller;

public class Uninstaller
{
    public static Uninstaller Instance;

    public IAppScanner AppScanner { get; }
    
    public Uninstaller()
    {
        Instance = this;
        AppScanner = new Win32AppScanner();
        
        foreach (var installedApp in AppScanner.FindInstalledApps())
        {
            Console.WriteLine(installedApp.Name);
        }
    }
}