using System.Collections.Generic;
using WinXP.Business.Modules.Sample.DTOs;
using static WinXP.Business.Modules.Sample.DomainServices.LineSorter;

namespace WinXP.Business.Modules.Sample.ApplicationServices
{
    public interface ISampleToolsService
    {
        IEnumerable<FlatColorDto> GetFlatColors();

        string InitializeLineSorting(SortTypes _selectedSortType, string textToSort);

        string InitializeGUIDGeneration(bool shouldCapitalize = true);
    }
}
