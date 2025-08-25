using System.Windows.Forms;

namespace Win98App.Base.Helpers;

internal class StandardErrorProvider : ErrorProvider
{
    private const int DEFAULT_ICON_PADDING = 5;

    internal StandardErrorProvider()
    {
        BlinkStyle = ErrorBlinkStyle.NeverBlink;
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
}
