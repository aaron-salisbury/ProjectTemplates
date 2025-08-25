using DotNetFramework.Business.Modules.Sample.ApplicationServices;
using System.Linq;
using System.Windows.Forms;
using Win98App.Base.MVP;
using Win98App.Views.SampleTools;
using static System.Windows.Forms.Control;

namespace Win98App.Presenters.SampleTools;

internal class FlatUIColorPickerPresenter : Presenter
{
    private readonly ISampleToolsService _sampleToolsService;

    private FlatUIColorPickerView _view;

    public FlatUIColorPickerPresenter(Navigator navigator, ISampleToolsService sampleToolsService) : base(navigator)
    {
        _sampleToolsService = sampleToolsService;
    }

    internal override void Display(Control view, ControlCollection window)
    {
        _view = (FlatUIColorPickerView)view;

        _view.Initialize(_sampleToolsService.GetFlatColors().ToList());

        window.Clear();
        window.Add(_view);
    }
}
