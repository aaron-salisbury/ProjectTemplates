using System;
using System.Collections.Generic;
using Win98Core.Base;
using Win98Core.SampleDataAccess;

namespace Win98Core.SampleTools
{
    public class FlatUIColorPicker
    {
        private List<FlatColor> _flatColors;
        public List<FlatColor> FlatColors
        {
            get { return _flatColors; }
            set { _flatColors = value; }
        }

        public bool SetFlatColors()
        {
            try
            {
                AppLogger.Write("Attempting to read colors from resource.", AppLogger.LogCategories.Information);

                FlatColors = CRUD.ReadAllFlatColors();

                AppLogger.Write("Successfully retrieved colors from resource.", AppLogger.LogCategories.Information);
                return true;
            }
            catch (Exception e)
            {
                AppLogger.Write(e.Message, AppLogger.LogCategories.Error);
                return false;
            }
        }
    }
}
