using System;
using System.Windows.Forms;

namespace WinXPApp.Base.MVP
{
    internal abstract class Presenter
    {
        internal virtual void Setup(UserControl view) { throw new NotImplementedException(); }

        internal virtual void Dismiss() { }
    }
}
