using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AvaloniaApp.Presentation.Base
{
    //TODO: When this gets resolved, replace this hack with official version: https://github.com/AvaloniaUI/Avalonia/issues/7640
    //      Otherwise this is an option: https://github.com/AvaloniaUtils/HyperText.Avalonia

    internal static class Browser
    {
        internal static void Open(string url)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                Debug.WriteLine("Browser launching only supported on desktop.", "ERROR");
            }
        }
    }
}
