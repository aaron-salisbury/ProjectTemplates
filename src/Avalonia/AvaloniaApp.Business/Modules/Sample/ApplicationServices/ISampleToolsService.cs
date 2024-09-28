using AvaloniaApp.Business.Modules.Sample.DTOs;
using static AvaloniaApp.Business.Modules.Sample.DomainServices.LineSorter;

namespace AvaloniaApp.Business.Modules.Sample.ApplicationServices
{
    public interface ISampleToolsService
    {
        IEnumerable<FlatColorDto> GetFlatColors();

        Task InitializeLineSortingAsync(SortTypes _selectedSortType, string? textToSort);

        Task InitializeGUIDGenerationAsync(bool shouldCapitalize = true);
    }
}
