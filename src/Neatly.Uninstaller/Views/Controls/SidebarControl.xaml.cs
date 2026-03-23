using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Neatly.Uninstaller.Models;

namespace Neatly.Uninstaller.Views.Controls;

public partial class SidebarControl : UserControl
{
    public List<InstalledApp> Apps { get; } = Uninstaller.Instance.GetInstalledApps();

    public ICollectionView FilteredApps { get; }

    private string _searchText = "";

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

        FilteredApps = CollectionViewSource.GetDefaultView(Apps);
        FilteredApps.Filter = FilterApps;

        DataContext = this;

        if (Apps.Count > 0)
        {
            SelectedApp = Apps[0];
        }
    }

    private bool FilterApps(object obj)
    {
        if (obj is not InstalledApp app)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(_searchText))
        {
            return true;
        }

        return app.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase);
    }

    public void UpdateSearch(string text)
    {
        _searchText = text;
        FilteredApps.Refresh();
    }
}