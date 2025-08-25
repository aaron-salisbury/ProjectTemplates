using DotNetFramework.Business.Modules.Sample.ApplicationServices;
using System.Linq;
using System.Windows.Forms;
using WinXPApp.Base.MVP;
using WinXPApp.Views.SampleTools;

namespace WinXPApp.Presenters.SampleTools;

internal class FlatUIColorPickerPresenter : Presenter
{
    private readonly ISampleToolsService _sampleToolsService;

    private FlatUIColorPickerView _view;

    public FlatUIColorPickerPresenter(ISampleToolsService sampleToolsService)
    {
        _sampleToolsService = sampleToolsService;
    }

    internal override void Setup(UserControl view)
    {
        _view = (FlatUIColorPickerView)view;

        _view.Initialize(_sampleToolsService.GetFlatColors().ToList());
    }
}
