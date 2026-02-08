using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Neatly.Uninstaller.Views.Controls;

public partial class AppItemControl : UserControl
{
    public static readonly DependencyProperty AppNameProperty =
        DependencyProperty.Register(nameof(AppName), typeof(string), typeof(AppItemControl));

    public static readonly DependencyProperty AppIconProperty =
        DependencyProperty.Register(nameof(AppIcon), typeof(ImageSource), typeof(AppItemControl));

    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(AppItemControl));
    
    public string AppName
    {
        get => (string)GetValue(AppNameProperty);
        set => SetValue(AppNameProperty, value);
    }

    public ImageSource AppIcon
    {
        get => (ImageSource)GetValue(AppIconProperty);
        set => SetValue(AppIconProperty, value);
    }

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }
    
    public AppItemControl()
    {
        InitializeComponent();
    }
}