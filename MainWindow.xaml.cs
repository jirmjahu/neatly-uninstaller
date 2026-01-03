using System.Windows;

namespace neatly.uninstaller;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var uninstaller = new Uninstaller();
    }
}