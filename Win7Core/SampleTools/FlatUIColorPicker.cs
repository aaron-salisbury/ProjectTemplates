using Serilog;
using System;
using System.Collections.Generic;
using Win7Core.Base;
using Win7Core.SampleDataAccess;

namespace Win7Core.SampleTools
{
    public class FlatUIColorPicker : ValidatableModel
    {
        private ILogger _logger { get; set; }

        private List<FlatColor> _flatColors;
        public List<FlatColor> FlatColors
        {
            get => _flatColors;
            set { _flatColors = value; RaisePropertyChanged(nameof(FlatColors)); }
        }

        public FlatUIColorPicker(AppLogger appLogger)
        {
            _logger = appLogger.Logger;
        }

        public bool SetFlatColors()
        {
            try
            {
                _logger.Information("Attempting to read colors from resource.");

                FlatColors = CRUD.ReadAllFlatColors();

                _logger.Information("Successfully retrieved colors from resource.");
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return false;
            }
        }
    }
}
