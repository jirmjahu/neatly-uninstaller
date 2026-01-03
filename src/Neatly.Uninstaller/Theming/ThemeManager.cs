using System.Windows;
using Microsoft.Win32;

namespace neatly.uninstaller.Theming;

public class ThemeManager
{
    private const string DarkThemePath = "Themes/DarkTheme.xaml";
    private const string LightThemePath = "Themes/LightTheme.xaml";

    public AppTheme CurrentTheme { get; private set; } = AppTheme.System;

    public void SetTheme(AppTheme theme)
    {
        CurrentTheme = theme;

        var themeFile = theme switch
        {
            AppTheme.Light => LightThemePath,
            AppTheme.Dark => DarkThemePath,
            AppTheme.System => IsSystemDark() ? DarkThemePath : LightThemePath,
            _ => LightThemePath
        };
        
        if (Application.Current.Resources.MergedDictionaries.Count > 0)
        {
            Application.Current.Resources.MergedDictionaries.RemoveAt(0);
        }
        
        Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri(themeFile, UriKind.Relative)
        });
    }

    private bool IsSystemDark()
    {
        using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");

        var value = key?.GetValue("AppsUseLightTheme");
        return value != null && (int)value == 0;
    }
}