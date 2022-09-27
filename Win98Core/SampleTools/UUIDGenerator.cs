using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Win98Core.Base;

namespace Win98Core.SampleTools
{
    public class UUIDGenerator : ValidatableModel
    {
        private bool _capitalize;
        public bool Capitalize
        {
            get { return _capitalize; }
            set { _capitalize = value; }
        }

        private string _uUID;
        public string UUID
        {
            get { return _uUID; }
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

                AppLogger.Write(string.Format("Generated new UUID, {0}", UUID), AppLogger.LogCategories.Information);
                return true;
            }
            catch (Exception e)
            {
                AppLogger.Write(e.Message, AppLogger.LogCategories.Error);
                return false;
            }
        }

        public override bool Validate()
        {
            Errors = new List<string>();

            if (!string.IsNullOrEmpty(UUID))
            {
                if (UUID.Length != 36)
                {
                    Errors.Add("The UUID must be 36 characters long.");
                }

                if (!Regex.IsMatch(UUID, @"^[^\s\,]+$"))
                {
                    Errors.Add("The UUID cannot have spaces.");
                }

                if (!Regex.IsMatch(UUID, @"^[a-zA-Z0-9-]+$"))
                {
                    Errors.Add("The UUID may only contain letters, numbers, and dashes.");
                }
            }

            return Errors.Count == 0;
        }
    }
}
