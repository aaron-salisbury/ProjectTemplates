using System;

namespace WinXPApp.Views;

public partial class HomeView : Base.MVP.View
{
    public HomeView()
    {
        InitializeComponent();
    }

    private void NlogLink_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Process.Start("https://nlog-project.org/");
    }

    private void ModernUILink_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Process.Start("https://github.com/peters/winforms-modernui");
    }

    private void LicenseLink_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Process.Start("https://github.com/aaron-salisbury/ProjectTemplates/blob/master/LICENSE");
    }
}
