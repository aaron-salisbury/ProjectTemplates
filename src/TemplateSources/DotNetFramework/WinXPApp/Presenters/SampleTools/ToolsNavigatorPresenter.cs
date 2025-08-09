using System.Windows.Forms;
using WinXPApp.Base.MVP;
using WinXPApp.Views.SampleTools;

namespace WinXPApp.Presenters.SampleTools
{
    internal class ToolsNavigatorPresenter : Presenter
    {
        private ToolsNavigatorView _view;

        internal override void Setup(UserControl view)
        {
            _view = (ToolsNavigatorView)view;
        }
    }
}
