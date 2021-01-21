using System;
using Win98Core.Base;

namespace Win98Core.SampleTools
{
    public class UUIDGenerator
    {
        private bool _capitalize;
        public bool Capitalize
        {
            get => _capitalize;
            set { _capitalize = value; }
        }

        private string _uUID;
        public string UUID
        {
            get => _uUID;
            set { _uUID = value; }
        }

        public bool Initiate()
        {
            try
            {
                AppLogger.Write("Beginning to generate new UUID.", AppLogger.LogCategories.Information);

                Guid newGuid = Guid.NewGuid();

                UUID = Capitalize
                    ? newGuid.ToString().ToUpper()
                    : newGuid.ToString();

                AppLogger.Write($"Generated new UUID, {UUID}", AppLogger.LogCategories.Information);
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
