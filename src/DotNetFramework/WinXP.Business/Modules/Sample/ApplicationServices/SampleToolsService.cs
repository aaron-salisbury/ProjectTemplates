using System.Collections.Generic;
using WinXP.Business.Modules.Sample.DomainServices;
using WinXP.Business.Modules.Sample.DTOs;
using static WinXP.Business.Modules.Sample.DomainServices.LineSorter;

namespace WinXP.Business.Modules.Sample.ApplicationServices
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

        public string InitializeLineSorting(SortTypes _selectedSortType, string? textToSort)
        {
            return _lineSorter.Initiate(textToSort, _selectedSortType);
        }

        public string InitializeGUIDGeneration(bool shouldCapitalize = true)
        {
            return _guidGenerator.Initiate(shouldCapitalize);
        }
    }
}
