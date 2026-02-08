using System.Windows;
using System.Windows.Controls;
using Neatly.Uninstaller.Models;

namespace Neatly.Uninstaller.Views.Controls;

public partial class AppDetailsControl : UserControl
{
    
    public static readonly DependencyProperty SelectedAppProperty =
        DependencyProperty.Register(
            nameof(SelectedApp),
            typeof(InstalledApp),
            typeof(AppDetailsControl),
            new PropertyMetadata(null));

    public InstalledApp SelectedApp
    {
        get => (InstalledApp)GetValue(SelectedAppProperty);
        set => SetValue(SelectedAppProperty, value);
    }
    
    public AppDetailsControl()
    {
        InitializeComponent();
        DataContext = this;
    }
}