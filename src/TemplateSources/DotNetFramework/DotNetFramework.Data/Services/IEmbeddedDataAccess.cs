using DotNetFramework.Data.Entities;
using System.Collections.Generic;

namespace DotNetFramework.Data
{
    /// <summary>
    /// Read Embedded data.
    /// </summary>
    public interface IEmbeddedDataAccess
    {
        List<FlatColor> ReadFlatColors();
    }
}
