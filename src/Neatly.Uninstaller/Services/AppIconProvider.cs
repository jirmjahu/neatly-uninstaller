using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Neatly.Uninstaller.Services;

public class AppIconProvider
{
    private const string FallbackIconPath =
        "pack://application:,,,/neatly.uninstaller;component/Resources/app_fallback.png";

    public ImageSource GetIcon(string filePath)
    {
        return ExtractIcon(filePath) ?? new BitmapImage(new Uri(FallbackIconPath));
    }

    private static ImageSource? ExtractIcon(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return null;
        }

        // Remove commas from the filename
        var parts = filePath.Split(',');
        var finalName = parts[0].Trim();

        if (!File.Exists(finalName))
        {
            return null;
        }

        using var icon = Icon.ExtractAssociatedIcon(finalName);
        if (icon == null)
        {
            return null;
        }

        return Imaging.CreateBitmapSourceFromHIcon(
            icon.Handle,
            Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());
    }
}