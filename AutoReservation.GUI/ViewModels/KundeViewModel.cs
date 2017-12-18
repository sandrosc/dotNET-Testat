using System;
using System.Windows.Input;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Gui.ViewModels;

namespace AutoReservation.GUI.ViewModels
{
    public class KundeViewModel : BaseViewModel
    {
        public class SaveCommand : ICommand
        {
            private KundeViewModel ViewModel { get; }

            public SaveCommand(KundeViewModel vm)
            {
                ViewModel = vm;
            }

            public bool CanExecute(object parameter)
            {
                return !(ViewModel.Nachname == "" || ViewModel.Vorname == "" || ViewModel.Geburtsdatum == null);
            }

            public void Execute(object parameter)
            {
                Console.WriteLine("asdf");
            }

            public event EventHandler CanExecuteChanged;
        }

        private readonly KundeDto _dto = new KundeDto();

        public SaveCommand Save { get; }

        public KundeViewModel()
        {
            Save = new SaveCommand(this);
        }

        public string Vorname
        {
            get => _dto.Vorname;
            set => SetValue(nameof(Vorname), value);
        }

        public string Nachname
        {
            get => _dto.Nachname;
            set => SetValue(nameof(Nachname), value);
        }

        public DateTime? Geburtsdatum
        {
            get => _dto.Geburtsdatum;
            set => SetValue(nameof(Geburtsdatum), value);
        }

        public byte[] RowVersion
        {
            get => _dto.RowVersion;
            set => SetValue(nameof(RowVersion), value);
        }

        public int Id
        {
            get => _dto.Id;
            set => SetValue(nameof(Id), value);
        }

        public string GanzerName => _dto.Vorname + " " + _dto.Nachname;

        private void SetValue(string name, object value)
        {
            var prop = _dto.GetType().GetProperty(name);
            if (prop != null)
            {
                var field = prop.GetValue(_dto);

                if (Equals(field, value))
                    return;

                prop.SetValue(_dto, value);
            }

            OnPropertyChanged(name);
        }
    }
}