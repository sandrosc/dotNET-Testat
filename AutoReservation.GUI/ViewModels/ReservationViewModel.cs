using System;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Gui.ViewModels;
using AutoReservation.Service.Wcf;
using System.Collections.ObjectModel;

namespace AutoReservation.GUI.ViewModels
{
    public class ReservationViewModel : BaseViewModel
    {
        private readonly AddReservationWindow _view;
        private readonly AutoReservationService _service;

        public RelayCommand SaveCommand { get; }
        public ReservationDto ReservationDto { get; } = new ReservationDto()
        {
            Von = DateTime.Now + new TimeSpan(1, 0, 0, 0),
            Bis = DateTime.Now + new TimeSpan(2, 0, 0, 0)
        };

        public ObservableCollection<AutoDto> Autos { get; } = new ObservableCollection<AutoDto>();
        public ObservableCollection<KundeDto> Kunden { get; } = new ObservableCollection<KundeDto>();
        public ReservationViewModel(AddReservationWindow view, AutoReservationService service)
        {
            _view = view;
            _service = service;
            foreach (var autoDto in service.GetAutos())
            {
                Autos.Add(autoDto);
            }
            foreach (var kundeDto in service.GetKunden())
            {
                Kunden.Add(kundeDto);
            }
            SaveCommand = new RelayCommand(Save, () => !(Von == null || Bis == null || Auto == null || Kunde == null));
        }

        private void Save()
        {
            _service.AddReservation(ReservationDto);
            _view.Close();
        }

        public int ReservationsNr
        {
            get => ReservationDto.ReservationsNr;
            set => SetValue(nameof(ReservationsNr), value);
        }

        public DateTime Von
        {
            get => ReservationDto.Von;
            set => SetValue(nameof(Von), value);
        }

        public DateTime Bis
        {
            get => ReservationDto.Bis;
            set => SetValue(nameof(Bis), value);
        }

        public AutoDto Auto
        {
            get => ReservationDto.Auto;
            set => SetValue(nameof(Auto), value);
        }

        public KundeDto Kunde
        {
            get => ReservationDto.Kunde;
            set => SetValue(nameof(Kunde), value);
        }

        public byte[] RowVersion
        {
            get => ReservationDto.RowVersion;
            set => SetValue(nameof(RowVersion), value);
        }

        private void SetValue(string name, object value)
        {
            SetValue(ReservationDto, name, value);
        }
    }
}