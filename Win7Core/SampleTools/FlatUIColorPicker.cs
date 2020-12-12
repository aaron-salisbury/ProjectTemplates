using Win7Core.Base;
using Win7Core.SampleDataAccess;
using Serilog;
using System;
using System.Collections.Generic;

namespace Win7Core.SampleTools
{
    public class FlatUIColorPicker : ValidatableModel
    {
        private List<FlatColor> _flatColors;
        public List<FlatColor> FlatColors
        {
            get => _flatColors;
            set { _flatColors = value; RaisePropertyChanged(nameof(FlatColors)); }
        }

        public bool SetFlatColors(ILogger logger)
        {
            try
            {
                logger.Information("Attempting to read colors from resource.");

                FlatColors = CRUD.ReadAllFlatColors();

                logger.Information("Successfully retrieved colors from resource.");
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return false;
            }
        }
    }
}
