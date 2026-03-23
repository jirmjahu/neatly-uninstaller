using System.Windows.Controls;

namespace Neatly.Uninstaller.Views.Controls;

public partial class TopBarControl : UserControl
{
    public event Action<string>? SearchChanged;

    public TopBarControl()
    {
        InitializeComponent();
    }

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        SearchChanged?.Invoke(SearchBox.Text);
    }
}