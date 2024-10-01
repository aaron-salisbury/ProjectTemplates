using System;
using System.Windows.Forms;
using static System.Windows.Forms.Control;
using IServiceProvider = DotNetFramework.Core.DependencyInjection.IServiceProvider;

namespace Win98App.Base.MVP
{
    public class Navigator
    {
        internal ControlCollection Window { get; set; }

        private readonly IServiceProvider _serviceProvider;
        private Presenter _current;

        public Navigator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        internal void NavigateTo(Type nextPresenterType)
        {
            if (typeof(Presenter).IsAssignableFrom(nextPresenterType) &&
                _serviceProvider.GetService(nextPresenterType) is Presenter nextPresenter)
            {
                NavigateTo(nextPresenter);
            }
        }

        private void NavigateTo(Presenter nextPresenter)
        {
            if (Window == null)
            {
                throw new ApplicationException($"{nameof(Window)} property not set on {nameof(Navigator)}. It is required for views (Controls) to be added to.");
            }

            if (_current != null)
            {
                _current.Dismiss();
            }

            _current = nextPresenter;
            _current.Display(FindView(nextPresenter), Window);
        }

        private static Control FindView(Presenter presenter)
        {
            string name = presenter.GetType().FullName!.Replace("Presenter", "View");
            Type type = Type.GetType(name);

            if (type != null && Activator.CreateInstance(type) is Control view)
            {
                return view;
            }

            throw new ApplicationException("View not found for presenter.");
        }
    }
}
