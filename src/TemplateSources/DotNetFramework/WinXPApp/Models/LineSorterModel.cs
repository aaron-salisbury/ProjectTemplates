using System.Collections.Generic;
using WinXPApp.Base.Helpers;

namespace WinXPApp.Models;

internal class LineSorterModel
{
    private List<ComboBoxEnumItem> _sortTypes;
    public List<ComboBoxEnumItem> SortTypes
    {
        get { return _sortTypes; }
        set { _sortTypes = value; }
    }

    private int _selectedSortTypeIndex;
    public int SelectedSortTypeIndex
    {
        get { return _selectedSortTypeIndex; }
        set { _selectedSortTypeIndex = value; }
    }

    private string _text;
    public string Text
    {
        get { return _text; }
        set { _text = value; }
    }
}
