using System;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Gui.ViewModels;
using AutoReservation.Service.Wcf;

namespace AutoReservation.GUI.ViewModels
{
    public class KundeViewModel : BaseViewModel
    {
        private readonly AddKundeWindow _view;
        private readonly AutoReservationService _service;
        public RelayCommand SaveCommand { get; }
        public KundeDto KundeDto { get; } = new KundeDto();

        public KundeViewModel(AddKundeWindow view, AutoReservationService service)
        {
            _view = view;
            _service = service;
            SaveCommand = new RelayCommand(Save, () => !(Nachname == string.Empty || Vorname == string.Empty || Geburtsdatum == null));
            KundeDto.Geburtsdatum = DateTime.Now - new TimeSpan(365 * 30, 0, 0, 0);
        }

        private void Save()
        {
            _service.AddKunde(KundeDto);
            _view.Close();
        }

        public string Vorname
        {
            get => KundeDto.Vorname;
            set => SetValue(nameof(Vorname), value);
        }

        public string Nachname
        {
            get => KundeDto.Nachname;
            set => SetValue(nameof(Nachname), value);
        }

        public DateTime? Geburtsdatum
        {
            get => KundeDto.Geburtsdatum;
            set => SetValue(nameof(Geburtsdatum), value);
        }

        public byte[] RowVersion
        {
            get => KundeDto.RowVersion;
            set => SetValue(nameof(RowVersion), value);
        }

        public int Id
        {
            get => KundeDto.Id;
            set => SetValue(nameof(Id), value);
        }

        public string GanzerName => KundeDto.Vorname + " " + KundeDto.Nachname;

        private void SetValue(string name, object value)
        {
            SetValue(KundeDto, name, value);
        }
    }
}