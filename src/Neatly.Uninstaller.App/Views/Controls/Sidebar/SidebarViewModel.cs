using Neatly.Uninstaller.Core.Applications;

namespace Neatly.Uninstaller.App.Views.Controls.Sidebar;

public sealed class SidebarViewModel
{
    public SidebarViewModel(List<InstalledApplication> applications)
    {
        Applications = applications
            .OrderBy(application => application.Name, StringComparer.CurrentCultureIgnoreCase)
            .ToList();
    }

    public List<InstalledApplication> Applications { get; }
}
