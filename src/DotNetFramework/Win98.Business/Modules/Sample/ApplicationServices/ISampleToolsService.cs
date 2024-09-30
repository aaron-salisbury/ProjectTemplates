using System.Collections.Generic;
using Win98.Business.Modules.Sample.DTOs;
using static Win98.Business.Modules.Sample.DomainServices.LineSorter;

namespace Win98.Business.Modules.Sample.ApplicationServices
{
    public interface ISampleToolsService
    {
        IEnumerable<FlatColorDto> GetFlatColors();

        string InitializeLineSorting(SortTypes _selectedSortType, string textToSort);

        string InitializeGUIDGeneration(bool shouldCapitalize = true);
    }
}
