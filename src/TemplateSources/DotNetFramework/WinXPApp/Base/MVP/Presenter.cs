using System.Windows.Forms;

namespace WinXPApp.Base.MVP;

internal class Presenter
{
    // This class is not abstract and has a constructor so that the Forms Designer can instantiate it.
    public Presenter() { }

    internal virtual void Setup(UserControl view) { }

    internal virtual void Dismiss() { }
}
