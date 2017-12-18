using System;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Gui.ViewModels;

namespace AutoReservation.GUI.ViewModels
{
    class KundeViewModel : BaseViewModel
    {
        private readonly KundeDto _dto = new KundeDto();

        public string Vorname
        {
            get => _dto.Vorname;
            set => setValue(nameof(Vorname), value);
        }

        public string Nachname
        {
            get => _dto.Nachname;
            set => setValue(nameof(Nachname), value);
        }

        public DateTime Geburtsdatum
        {
            get => _dto.Geburtsdatum;
            set => setValue(nameof(Geburtsdatum), value);
        }

        public byte[] RowVersion
        {
            get => _dto.RowVersion;
            set => setValue(nameof(RowVersion), value);
        }

        public int Id
        {
            get => _dto.Id;
            set => setValue(nameof(Id), value);
        }

        public string GanzerName => _dto.Vorname + " " + _dto.Nachname;

        private bool setValue(string name, object value)
        {
            var prop = _dto.GetType().GetProperty(name);
            var field = prop.GetValue(_dto);

            if (Equals(field, value))
                return false;

            prop.SetValue(_dto, value);

            OnPropertyChanged(name);

            return true;
        }
    }
}