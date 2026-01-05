using System.Windows;
using neatly.uninstaller.Views.Pages;

namespace neatly.uninstaller;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.Navigate(new MainPage());
    }
}