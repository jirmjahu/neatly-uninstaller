using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Neatly.Uninstaller.Theming;
using Neatly.Uninstaller.Views.Pages;

namespace Neatly.Uninstaller;

public partial class MainWindow : Window
{
    private const int DwmwaCaptionColor = 35;
    private const int DwmwaTextColor = 36;

    [DllImport("dwmapi.dll", PreserveSig = true)]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref uint attrValue, int attrSize);

    public MainWindow()
    {
        InitializeComponent();

        MainFrame.Navigate(new MainPage());

        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        SetTitlebarColors();
    }

    private void SetTitlebarColors()
    {
        var hwnd = new WindowInteropHelper(this).Handle;
        
        var titlebarBackgroundColor = Uninstaller.Instance.ThemeManager.CurrentTheme.Equals(AppTheme.Dark) ? 0x000000u : 0xFFFFFFu;
        
        DwmSetWindowAttribute(hwnd, DwmwaCaptionColor, ref titlebarBackgroundColor, sizeof(uint));

        var titlebarTextColor = Uninstaller.Instance.ThemeManager.CurrentTheme.Equals(AppTheme.Light) ? 0x000000u : 0xFFFFFFu;
        DwmSetWindowAttribute(hwnd, DwmwaTextColor, ref titlebarTextColor, sizeof(uint));
    }
    
}