using MetroFramework.Forms;
using WinXPApp.Base;

namespace WinXPApp.Forms
{
    public partial class BaseForm : MetroForm
    {
        public BaseForm()
        {
            InitializeComponent();

            AppearanceManager.LoadBaseSettings(this);
        }
    }
}
