using System.Windows.Forms;
using Win98App.Base.MVP;
using Win98App.Models;
using Win98App.Views;
using static System.Windows.Forms.Control;

namespace Win98App.Presenters
{
    internal class HomePresenter : Presenter
    {
        private HomeView _view;

        public HomePresenter(Navigator navigator) : base(navigator) { }

        internal override void Display(Control view, ControlCollection window)
        {
            _view = (HomeView)view;

            _view.Initialize(new HomeModel()
            {
                //TODO: Use resources.
                IntroductionBlurb = "This is a sample application generated from Aaron Salisbury's Windows 98 App Solution Template.",
                LoggingBlurb = "The application shares an implementation of the pluggable and reusable Logging application block of the Microsoft Enterprise Library."
            });

            window.Clear();
            window.Add(_view);
        }
    }
}
