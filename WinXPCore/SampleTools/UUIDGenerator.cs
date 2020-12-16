using System;
using System.ComponentModel.DataAnnotations;
using WinXPCore.Base;

namespace WinXPCore.SampleTools
{
    public class UUIDGenerator : ValidatableModel
    {
        private bool _capitalize;
        public bool Capitalize
        {
            get => _capitalize;
            set { _capitalize = value; }
        }

        private string _uUID;
        [StringLength(36, ErrorMessage = "The {0} must be {1} characters long.")]
        [RegularExpression(@"^[^\s\,]+$", ErrorMessage = "The {0} cannot have spaces.")]
        public string UUID
        {
            get => _uUID;
            set { _uUID = value; }
        }

        public bool Initiate()
        {
            try
            {
                Logger.Info("Beginning to generate new UUID.");

                Guid newGuid = System.Guid.NewGuid();

                UUID = Capitalize
                    ? newGuid.ToString().ToUpper()
                    : newGuid.ToString();

                Logger.Info($"Generated new UUID, {UUID}");
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return false;
            }
        }
    }
}
