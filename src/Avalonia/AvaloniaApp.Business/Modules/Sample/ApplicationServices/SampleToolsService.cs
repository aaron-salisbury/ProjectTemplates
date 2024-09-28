using AvaloniaApp.Business.Modules.Sample.DomainServices;
using AvaloniaApp.Business.Modules.Sample.DTOs;
using static AvaloniaApp.Business.Modules.Sample.DomainServices.LineSorter;

namespace AvaloniaApp.Business.Modules.Sample.ApplicationServices
{
    public class SampleToolsService : ISampleToolsService
    {
        private readonly FlatUIColorPicker _flatColorProvider;
        private readonly LineSorter _lineSorter;
        private readonly UUIDGenerator _guidGenerator;

        public SampleToolsService(FlatUIColorPicker flatColorProvider, LineSorter lineSorter, UUIDGenerator guidGenerator)
        {
            _flatColorProvider = flatColorProvider;
            _lineSorter = lineSorter;
            _guidGenerator = guidGenerator;
        }

        public IEnumerable<FlatColorDto> GetFlatColors()
        {
            return _flatColorProvider.GetFlatColors();
        }

        public async Task InitializeLineSortingAsync(SortTypes _selectedSortType, string? textToSort)
        {
            await _lineSorter.InitiateAsync(_selectedSortType, textToSort);
        }

        public async Task InitializeGUIDGenerationAsync(bool shouldCapitalize = true)
        {
            await _guidGenerator.InitiateAsync(shouldCapitalize);
        }
    }
}
