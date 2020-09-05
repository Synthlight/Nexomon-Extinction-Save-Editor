using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Save_Editor.Models {
    // TODO: https://www.codeproject.com/Articles/38865/INotifyPropertyChanged-Auto-Wiring-or-How-to-Get-R
    public class NotifyPropertyChangedBase : INotifyPropertyChanged {
        protected bool SetProperty<T>(ref T storage, T value, string[] dependentPropertyNames = null, [CallerMemberName] string propertyName = null) {
            if (Equals(storage, value)) {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);

            if (dependentPropertyNames != null) {
                foreach (var dependentPropertyName in dependentPropertyNames) {
                    OnPropertyChanged(dependentPropertyName);
                }
            }

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}