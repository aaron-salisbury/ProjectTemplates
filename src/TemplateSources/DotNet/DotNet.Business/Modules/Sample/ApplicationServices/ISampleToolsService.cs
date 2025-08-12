using DotNet.Business.Modules.Sample.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DotNet.Business.Modules.Sample.DomainServices.LineSorter;

namespace DotNet.Business.Modules.Sample.ApplicationServices
{
    public interface ISampleToolsService
    {
        IEnumerable<FlatColorDto> GetFlatColors();

        Task InitializeLineSortingAsync(SortTypes _selectedSortType, string? textToSort);

        Task InitializeGUIDGenerationAsync(bool shouldCapitalize = true);
    }
}
