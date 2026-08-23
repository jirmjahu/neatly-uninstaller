using System.Windows;
using Neatly.Uninstaller.App.Views.Controls.Sidebar;

namespace Neatly.Uninstaller.App;

public partial class MainWindow : Window
{
    public MainWindow(SidebarViewModel sidebarViewModel)
    {
        InitializeComponent();
        SidebarView.DataContext = sidebarViewModel;
    }
}