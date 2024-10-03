using DotNetFramework.Business.Modules.Sample.DTOs;
using System.Collections.Generic;
using static DotNetFramework.Business.Modules.Sample.DomainServices.LineSorter;

namespace DotNetFramework.Business.Modules.Sample.ApplicationServices
{
    public interface ISampleToolsService
    {
        IEnumerable<FlatColorDto> GetFlatColors();

        string InitializeLineSorting(SortTypes _selectedSortType, string textToSort);

        string InitializeGUIDGeneration(bool shouldCapitalize = true);
    }
}
