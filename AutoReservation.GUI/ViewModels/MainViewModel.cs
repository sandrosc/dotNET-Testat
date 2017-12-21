using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Gui.ViewModels;
using AutoReservation.Service.Wcf;

namespace AutoReservation.GUI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly AutoReservationService _service;

        public ObservableCollection<AutoDto> Autos { get; } = new ObservableCollection<AutoDto>();
        public ObservableCollection<KundeDto> Kunden { get; } = new ObservableCollection<KundeDto>();
        public ObservableCollection<ReservationDto> Reservationen { get; } = new ObservableCollection<ReservationDto>();

        public RelayCommand AddAutoCommand { get; }
        public RelayCommand AddKundeCommand { get; }
        public RelayCommand AddReservationCommand { get; }
        public RelayCommand<int> RemoveKundeCommand { get; }
        public RelayCommand<int> RemoveAutoCommand { get; }
        public RelayCommand<int> RemoveReservationCommand { get; }

        private DispatcherTimer DispatcherTimer { get; }

        public MainViewModel(AutoReservationService service)
        {
            _service = service;

            AddAutoCommand = new RelayCommand(AddAuto);
            AddKundeCommand = new RelayCommand(AddKunde);
            AddReservationCommand = new RelayCommand(AddReservation);
            RemoveKundeCommand = new RelayCommand<int>(RemoveKunde);
            RemoveReservationCommand = new RelayCommand<int>(RemoveReservation);
            RemoveAutoCommand = new RelayCommand<int>(RemoveAuto);

            UpdateLists();
            //update lists every 5 seconds
            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Tick += UpdateLists;
            DispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            DispatcherTimer.Start();
        }

        private void RemoveKunde(int id)
        {
            _service.RemoveKunde(Kunden.FirstOrDefault(k => k.Id == id));
            UpdateLists();
        }

        private void RemoveReservation(int id)
        {
            _service.RemoveReservation(Reservationen.FirstOrDefault(r => r.ReservationsNr == id));
            UpdateLists();
        }

        private void RemoveAuto(int id)
        {
            _service.RemoveAuto(Autos.FirstOrDefault(a => a.Id == id));
            UpdateLists();
        }


        private void AddAuto()
        {
            var addAutoWindow = new AddAutoWindow(_service);
            addAutoWindow.ShowDialog();
        }

        private void AddKunde()
        {
            var addKundeWindow = new AddKundeWindow(_service);
            addKundeWindow.ShowDialog();
        }

        private void AddReservation()
        {
            var addReservationWindow = new AddReservationWindow(_service);
            addReservationWindow.ShowDialog();
        }

        private void UpdateLists(object sender = null, EventArgs e = null)
        {
            Autos.Clear();
            foreach (var auto in _service.GetAutos())
            {
                Autos.Add(auto);
            }
            Kunden.Clear();
            foreach (var kunde in _service.GetKunden())
            {
                Kunden.Add(kunde);
            }
            Reservationen.Clear();
            foreach (var reservation in _service.GetReservationen())
            {
                Reservationen.Add(reservation);
            }
        }
    }
}