using System.ComponentModel;

namespace WinXPCore.Base
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        public static void StaticRaisePropertyChanged(string propertyName)
        {
            if (StaticPropertyChanged != null)
            {
                StaticPropertyChanged.Invoke(null, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
