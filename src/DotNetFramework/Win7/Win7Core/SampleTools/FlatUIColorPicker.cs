using Serilog;
using System;
using System.Collections.Generic;
using Win7Core.Base;
using Win7Core.SampleDataAccess;

namespace Win7Core.SampleTools
{
    public class FlatUIColorPicker : ValidatableModel
    {
        private readonly ILogger _logger;

        private List<FlatColor> _flatColors;
        public List<FlatColor> FlatColors
        {
            get { return _flatColors; }
            set { _flatColors = value; RaisePropertyChanged("FlatColors"); }
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
