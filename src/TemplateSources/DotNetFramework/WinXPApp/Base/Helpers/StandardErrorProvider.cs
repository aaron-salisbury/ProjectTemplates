using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WinXPApp.Base.Helpers;

internal class StandardErrorProvider : ErrorProvider
{
    private const int DEFAULT_ICON_PADDING = 5;

    internal StandardErrorProvider()
    {
        BlinkStyle = ErrorBlinkStyle.NeverBlink;
        Icon = Properties.Resources.ErrorSymbol;
    }

    internal void UpdateError(Control control, string errorMessage)
    {
        if (!string.IsNullOrEmpty(errorMessage))
        {
            SetIconPadding(control, DEFAULT_ICON_PADDING);
            SetError(control, errorMessage);
        }
        else
        {
            SetError(control, string.Empty);
        }
    }

    private void SetIconFromEmbeddedImageFile(string embeddedImageFilePath)
    {
        using (Stream iconStream = GetType().Assembly.GetManifestResourceStream(embeddedImageFilePath))
        {
            Bitmap iconBitmap = new Bitmap(iconStream);
            iconBitmap = (Bitmap)iconBitmap.GetThumbnailImage(16, 16, null, IntPtr.Zero);
            Icon = Icon.FromHandle(iconBitmap.GetHicon());
        }
    }
}
