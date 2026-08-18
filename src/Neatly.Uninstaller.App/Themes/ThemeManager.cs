using System.Windows;

namespace Neatly.Uninstaller.App.Themes;

public sealed class ThemeManager(ResourceDictionary resources)
{
    private ResourceDictionary? _currentTheme;

    public void Set(Theme theme)
    {
        var themeResources = new ResourceDictionary
        {
            Source = new Uri($"Themes/{theme}.xaml", UriKind.Relative)
        };

        if (_currentTheme != null)
        {
            resources.MergedDictionaries.Remove(_currentTheme);
        }

        resources.MergedDictionaries.Add(themeResources);
        _currentTheme = themeResources;
    }
}
