using DotNetFramework.Business.Modules.Sample.ApplicationServices;
using System.Windows.Forms;
using Win98App.Base.MVP;
using Win98App.Views.SampleTools;
using static System.Windows.Forms.Control;

namespace Win98App.Presenters.SampleTools;

internal class UUIDGeneratorPresenter : Presenter
{
    private readonly ISampleToolsService _sampleToolsService;

    private UUIDGeneratorView _view;

    public UUIDGeneratorPresenter(Navigator navigator, ISampleToolsService sampleToolsService) : base(navigator)
    {
        _sampleToolsService = sampleToolsService;
    }

    internal override void Display(Control view, ControlCollection window)
    {
        _view = (UUIDGeneratorView)view;

        _view.GenerateCommand += View_GenerateCommand;

        window.Clear();
        window.Add(_view);
    }

    internal override void Dismiss()
    {
        if (_view != null)
        {
            _view.GenerateCommand -= View_GenerateCommand;
        }
    }

    private void View_GenerateCommand(object sender, GenerateCommandEventArgs e)
    {
        if (_view != null)
        {
            string generatedUUID = _sampleToolsService.InitializeGUIDGeneration(e.ShouldCapitalize);

            _view.UUIDGenerated(generatedUUID);
        }
    }
}
