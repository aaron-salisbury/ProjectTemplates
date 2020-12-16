using Win7Core.Base;
using Win7Core.Base.ValidationAttributes;
using Serilog;
using System;
using System.ComponentModel.DataAnnotations;

namespace Win7Core.SampleTools
{
    public class UUIDGenerator : ValidatableModel
    {
        private ILogger _logger { get; set; }

        private bool _capitalize;
        public bool Capitalize
        {
            get => _capitalize;
            set { _capitalize = value; RaisePropertyChanged(nameof(Capitalize)); }
        }

        private string _uUID;
        // If we wanted, these validations could be combined, but this is to demonstrate different options for writting data annotation validations.
        // System.ComponentModel.DataAnnotations comes with standard ones like StringLength and the ability to write regular expressions directly.
        // Or we can also write our own custom rules like LettersNumbersDashes by inheriting from ValidationAttribute and overwriting the IsValid method.
        [StringLength(36, ErrorMessage = "The {0} must be {1} characters long.", MinimumLength = 36)]
        [RegularExpression(@"^[^\s\,]+$", ErrorMessage = "The {0} cannot have spaces.")]
        [LettersNumbersDashes(ErrorMessage = "The {0} may only contain letters, numbers, and dashes.")]
        public string UUID
        {
            get => _uUID;
            set { _uUID = value; RaisePropertyChanged(nameof(UUID)); }
        }

        public UUIDGenerator(AppLogger appLogger)
        {
            _logger = appLogger.Logger;
        }

        public bool Initiate()
        {
            try
            {
                _logger.Information("Beginning to generate new UUID.");

                Guid newGuid = Guid.NewGuid();

                UUID = Capitalize
                    ? newGuid.ToString().ToUpper()
                    : newGuid.ToString();

                _logger.Information($"Generated new UUID, {UUID}");
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
