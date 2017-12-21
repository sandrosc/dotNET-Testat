using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Gui.ViewModels;
using AutoReservation.Service.Wcf;

namespace AutoReservation.GUI.ViewModels
{
    public class ReservationViewModel : BaseViewModel
    {
        //private readonly AddReservationWindow _view;
        private readonly AutoReservationService _service;

        public RelayCommand SaveCommand { get; }
        public ReservationDto ReservationDto { get; } = new ReservationDto();

        public ReservationViewModel(AutoReservationService service)
        {
            _service = service;
            SaveCommand = new RelayCommand(Save, () => !(Von == null || Bis == null || Auto == null || Kunde == null));
        }

        private void Save()
        {
            _service.AddReservation(ReservationDto);
//            _view.Close();
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