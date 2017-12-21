using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AutoReservation.Common.DataTransferObjects;

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

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void SetValue(object dto, string name, object value, string notifyProperty = null)
        {
            notifyProperty = notifyProperty ?? name;

            var prop = dto.GetType().GetProperty(name);
            if (prop != null)
            {
                var field = prop.GetValue(dto);

                if (Equals(field, value))
                    return;

                prop.SetValue(dto, value);
            }

            OnPropertyChanged(notifyProperty);
        }
    }
}