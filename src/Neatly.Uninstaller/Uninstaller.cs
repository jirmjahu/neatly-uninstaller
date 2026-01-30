using neatly.uninstaller.Services;
using neatly.uninstaller.Theming;

namespace neatly.uninstaller;

public class Uninstaller
{
    public static Uninstaller Instance;

    public IAppScanner AppScanner { get; }
    public ThemeManager ThemeManager { get; }
    
    public Uninstaller()
    {
        Instance = this;
        AppScanner = new Win32AppScanner();
        ThemeManager = new ThemeManager();
        ThemeManager.SetTheme(AppTheme.Dark);
    }
}