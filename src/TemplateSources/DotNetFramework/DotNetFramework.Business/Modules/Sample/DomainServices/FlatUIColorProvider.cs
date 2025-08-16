using DotNetFramework.Business.Modules.Sample.DTOs;
using DotNetFramework.Data;
using DotNetFramework.Data.Entities;
using System.Collections.Generic;

namespace DotNetFramework.Business.Modules.Sample.DomainServices
{
    public class FlatUIColorProvider
    {
        private readonly IEmbeddedDataAccess _embeddedDataAccess;

        public FlatUIColorProvider(IEmbeddedDataAccess embeddedDataAccess)
        {
            _embeddedDataAccess = embeddedDataAccess;
        }

        public IEnumerable<FlatColorDto> GetFlatColors()
        {
            List<FlatColorDto> flatColors = [];

            foreach (FlatColor colorEntity in _embeddedDataAccess.ReadFlatColors())
            {
                flatColors.Add(FlatColorDto.MapToDto(colorEntity));
            }

            return flatColors;
        }
    }
}
