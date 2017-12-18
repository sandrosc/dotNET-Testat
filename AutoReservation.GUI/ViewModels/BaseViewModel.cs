using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoReservation.Gui.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string name = null,
            params string[] otherNames)
        {
            if (Equals(field, value))
                return false;

            field = value;

            OnPropertyChanged(name);
            foreach (var n in otherNames)
            {
                OnPropertyChanged(n);
            }

            return true;
        }

        //protected bool SetProperty<T>(ref T dto, T field, T value)
        //{
        //    
        //}
    

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}