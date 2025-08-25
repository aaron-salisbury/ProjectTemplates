using DotNetFramework.Business.Modules.Sample.ApplicationServices;
using DotNetFramework.Business.Modules.Sample.DomainServices;
using DotNetFrameworkToolkit.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinXPApp.Base.Helpers;
using WinXPApp.Base.MVP;
using WinXPApp.Views.SampleTools;

namespace WinXPApp.Presenters.SampleTools;

internal class LineSorterPresenter : Presenter
{
    private readonly ISampleToolsService _sampleToolsService;
    private readonly List<LineSorter.SortTypes> _sortTypes;
    private readonly List<ComboBoxEnumItem> _sortTypeItems;

    private LineSorterView _view;

    public LineSorterPresenter(ISampleToolsService sampleToolsService)
    {
        _sampleToolsService = sampleToolsService;
        _sortTypes = Enum.GetValues(typeof(LineSorter.SortTypes)).Cast<LineSorter.SortTypes>().ToList();

        _sortTypeItems = [];
        foreach (LineSorter.SortTypes sortType in _sortTypes)
        {
            _sortTypeItems.Add(new ComboBoxEnumItem()
            {
                Value = (int)sortType,
                Text = StringExtensions.SplitPascalCase(sortType.ToString())
            });
        }
    }

    internal override void Setup(UserControl view)
    {
        _view = (LineSorterView)view;

        _view.Initialize(_sortTypeItems);

        _view.SortCommand += View_SortCommand;
    }

    internal override void Dismiss()
    {
        if (_view != null)
        {
            _view.SortCommand -= View_SortCommand;
        }
    }

    private void View_SortCommand(object sender, SortCommandEventArgs e)
    {
        if (_view != null)
        {
            string sortedText = _sampleToolsService.InitializeLineSorting(_sortTypes[e.SortTypeIndex], e.TextToSort);

            _view.TextSorted(sortedText);
        }
    }
}
