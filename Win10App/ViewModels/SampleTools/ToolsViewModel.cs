using System;
using System.Collections.Generic;
using Win10App.Views;

namespace Win10App.ViewModels
{
    public class ToolsViewModel : BaseViewModel
    {
        public readonly List<(string Content, Type Page)> ToolPages = new List<(string Content, Type Page)>
        {
            ("UUID Generator", typeof(UUIDGeneratorPage)),
            ("Flat UI Color Picker", typeof(FlatUIColorPickerPage)),
            ("Line Sorter", typeof(LineSorterPage))
        };
    }
}
