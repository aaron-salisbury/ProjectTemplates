using DotNet.Business.Modules.Sample.DTOs;
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
