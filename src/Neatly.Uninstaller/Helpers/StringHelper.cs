namespace Neatly.Uninstaller.Helpers;

public static class StringHelper
{
    public static string NormalizeString(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return "";
        }

        input = input.ToLower();

        var invalidChars = new[] { ' ', '-', '_', '.', ',' };
        foreach (var c in invalidChars)
        {
            input = input.Replace(c.ToString(), "");
        }

        return input;
    }
}