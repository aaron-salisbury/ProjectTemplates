using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Media;

namespace Win10App.Base.Helpers
{
    public static class PlatformShim
    {
        public static void CopyToClipboard(string text)
        {
            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(text ?? string.Empty);
            Clipboard.SetContent(dataPackage);
        }

        public static SolidColorBrush BrushConverterConvertFrom(string hex)
        {
            SolidColorBrush brush = null;

            if (!string.IsNullOrEmpty(hex))
            {
                hex = hex.Replace("#", string.Empty);

                byte a, r, g, b;

                if (hex.Length == 6)
                {
                    a = 255;
                    r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
                    g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
                    b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));

                    brush = new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
                }
                else if (hex.Length == 8)
                {
                    a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
                    r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
                    g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
                    b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));

                    brush = new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
                }
            }

            return brush;
        }
    }
}
