using DotNetFramework.Business.Modules.Sample.DomainServices;
using DotNetFramework.Business.Modules.Sample.DTOs;
using System.Collections.Generic;
using static DotNetFramework.Business.Modules.Sample.DomainServices.LineSorter;

namespace DotNetFramework.Business.Modules.Sample.ApplicationServices
{
    public class SampleToolsService : ISampleToolsService
    {
        private readonly FlatUIColorProvider _flatColorProvider;
        private readonly LineSorter _lineSorter;
        private readonly UUIDGenerator _guidGenerator;

        public SampleToolsService(FlatUIColorProvider flatColorProvider, LineSorter lineSorter, UUIDGenerator guidGenerator)
        {
            _flatColorProvider = flatColorProvider;
            _lineSorter = lineSorter;
            _guidGenerator = guidGenerator;
        }

        public IEnumerable<FlatColorDto> GetFlatColors()
        {
            return _flatColorProvider.GetFlatColors();
        }

        public string InitializeLineSorting(SortTypes _selectedSortType, string textToSort)
        {
            return _lineSorter.Initiate(textToSort, _selectedSortType);
        }

        public string InitializeGUIDGeneration(bool shouldCapitalize = true)
        {
            return _guidGenerator.Initiate(shouldCapitalize);
        }
    }
}
