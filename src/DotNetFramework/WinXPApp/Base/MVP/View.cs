using System;
using System.Windows.Forms;

namespace WinXPApp.Base.MVP
{
    public abstract class View : UserControl
    {
        private readonly Presenter _presenter;

        public View()
        {
            string presenterName = this.GetType().FullName!.Replace("View", "Presenter");
            Type presenterType = Type.GetType(presenterName);
            _presenter = Program.Services.GetService(presenterType) as Presenter;

            this.Load += View_Load;
        }

        private void View_Load(object sender, EventArgs e)
        {
            _presenter.Setup(this);
        }
    }
}
