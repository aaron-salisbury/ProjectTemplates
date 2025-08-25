using System.Windows.Forms;
using WinXPApp.Base.MVP;
using WinXPApp.Views;

namespace WinXPApp.Presenters;

internal class HomePresenter : Presenter
{
    private HomeView _view;

    internal override void Setup(UserControl view)
    {
        _view = (HomeView)view;

        //_view.Initialize(new HomeModel()
        //{
        //    //TODO: Use resources.
        //    IntroductionBlurb = "This is a sample application generated from Aaron Salisbury's Windows 98 App Solution Template.",
        //    LoggingBlurb = "The application shares an implementation of the pluggable and reusable Logging application block of the Microsoft Enterprise Library."
        //});
    }
}
