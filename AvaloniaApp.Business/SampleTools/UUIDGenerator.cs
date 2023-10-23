using AvaloniaApp.Business.Base;
using System.Diagnostics;

namespace AvaloniaApp.Business.SampleTools
{
    public class UUIDGenerator : ObservableModel
    {
        private bool _capitalize;
        public bool Capitalize
        {
            get => _capitalize;
            set => SetProperty(ref _capitalize, value);
        }

        private string _uUID;
        public string UUID
        {
            get => _uUID;
            set => SetProperty(ref _uUID, value);
        }

        public bool Initiate()
        {
            try
            {
                Debug.WriteLine("Beginning to generate new UUID.", "INFO");

                Guid newGuid = Guid.NewGuid();

                UUID = Capitalize
                    ? newGuid.ToString().ToUpper()
                    : newGuid.ToString();

                Debug.WriteLine($"Generated new UUID: {UUID}", "INFO");
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message, "ERROR");
                return false;
            }
        }
    }
}
