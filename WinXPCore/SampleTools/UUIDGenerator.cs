using System;
using System.ComponentModel.DataAnnotations;
using WinXPCore.Base;
using WinXPCore.Base.ValidationAttributes;

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
        // If we wanted, these validations could be combined, but this is to demonstrate different options for writting data annotation validations.
        // System.ComponentModel.DataAnnotations comes with standard ones like StringLength and the ability to write regular expressions directly.
        // Or we can also write our own custom rules like LettersNumbersDashes by inheriting from ValidationAttribute and overwriting the IsValid method.
        [StringLength(36, ErrorMessage = "The {0} must be {1} characters long.")]
        [RegularExpression(@"^[^\s\,]+$", ErrorMessage = "The {0} cannot have spaces.")]
        [LettersNumbersDashes(ErrorMessage = "The {0} may only contain letters, numbers, and dashes.")]
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
