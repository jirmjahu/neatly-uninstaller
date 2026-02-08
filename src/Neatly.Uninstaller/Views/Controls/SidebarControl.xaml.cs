using System.Windows;
using System.Windows.Controls;
using Neatly.Uninstaller.Models;

namespace Neatly.Uninstaller.Views.Controls;

public partial class SidebarControl : UserControl
{
    
    public List<InstalledApp> Apps { get; } = Uninstaller.Instance.AppScanner.FindInstalledApps();
    
    public static readonly DependencyProperty SelectedAppProperty =
        DependencyProperty.Register(
            nameof(SelectedApp),
            typeof(InstalledApp),
            typeof(SidebarControl),
            new PropertyMetadata(null));

    public InstalledApp SelectedApp
    {
        get => (InstalledApp)GetValue(SelectedAppProperty);
        set => SetValue(SelectedAppProperty, value);
    }
    
    public SidebarControl()
    {
        InitializeComponent();
        DataContext = this;
        
        if (Apps.Count > 0)
        {
            SelectedApp = Apps[0];
        }
    }
}