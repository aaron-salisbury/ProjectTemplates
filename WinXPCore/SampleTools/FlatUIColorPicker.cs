using NLog;
using System;
using System.Collections.Generic;
using WinXPCore.Base;
using WinXPCore.SampleDataAccess;

namespace WinXPCore.SampleTools
{
    public class FlatUIColorPicker : ValidatableModel
    {
        private List<FlatColor> _flatColors;
        public List<FlatColor> FlatColors
        {
            get => _flatColors;
            set =>_flatColors = value;
        }

        public bool SetFlatColors()
        {
            try
            {
                Logger.Info("Attempting to read colors from resource.");

                FlatColors = CRUD.ReadAllFlatColors();

                Logger.Info("Successfully retrieved colors from resource.");
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return false;
            }
        }
    }
}
