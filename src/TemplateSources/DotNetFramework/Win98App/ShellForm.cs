using System;
using System.Windows.Forms;
using Win98App.Base.MVP;
using Win98App.Forms;
using Win98App.Presenters;
using Win98App.Presenters.SampleTools;

namespace Win98App;

public partial class ShellForm : Form
{
    private readonly Padding _defaultContentAreaPadding = new(15);
    private readonly Navigator _navigator;

    public ShellForm(Navigator navigator)
    {
        InitializeComponent();

        Text = Properties.Settings.Default.ApplicationFriendlyName;

        MainContentPanel.ControlAdded += MainContentPanel_ControlAdded;
        _navigator = navigator;
        _navigator.Window = MainContentPanel.Controls;
        _navigator.NavigateTo(typeof(HomePresenter));
    }

    private void MainContentPanel_ControlAdded(object sender, ControlEventArgs e)
    {
        if (e != null && e.Control != null)
        {
            e.Control.Padding = _defaultContentAreaPadding;
            e.Control.Dock = DockStyle.Fill;
        }
    }

    private void HomeMenuItem_Click(object sender, EventArgs e)
    {
        _navigator.NavigateTo(typeof(HomePresenter));
    }

    private void LogMenuItem_Click(object sender, EventArgs e)
    {
        _navigator.NavigateTo(typeof(LogsPresenter));
    }

    private void AboutMenuItem_Click(object sender, EventArgs e)
    {
        //TODO: About form still doesn't use mode/view/presenter.
        using (AboutForm aboutForm = new())
        {
            aboutForm.ShowDialog(this);
        }
    }

    private void ExitMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void UUIDGeneratorMenuItem_Click(object sender, EventArgs e)
    {
        _navigator.NavigateTo(typeof(UUIDGeneratorPresenter));
    }

    private void FlatUIColorPickerMenuItem_Click(object sender, EventArgs e)
    {
        _navigator.NavigateTo(typeof(FlatUIColorPickerPresenter));
    }

    private void LineSorterMenuItem_Click(object sender, EventArgs e)
    {
        _navigator.NavigateTo(typeof(LineSorterPresenter));
    }
}
