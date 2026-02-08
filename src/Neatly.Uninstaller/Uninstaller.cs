using Neatly.Uninstaller.Services;
using Neatly.Uninstaller.Theming;

namespace Neatly.Uninstaller;

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