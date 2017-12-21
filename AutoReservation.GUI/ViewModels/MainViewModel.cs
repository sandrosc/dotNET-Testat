using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Gui.ViewModels;
using AutoReservation.Service.Wcf;

namespace AutoReservation.GUI.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private readonly AutoReservationService _service;
        
        public ObservableCollection<AutoDto> Autos { get; } = new ObservableCollection<AutoDto>();
        public ObservableCollection<KundeDto> Kunden { get; } = new ObservableCollection<KundeDto>();
        public ObservableCollection<ReservationDto> Reservationen { get; } = new ObservableCollection<ReservationDto>();

        public RelayCommand AddAutoCommand { get; }
        public RelayCommand AddKundeCommand { get; }
        public RelayCommand AddReservationCommand { get; }
        public RelayCommand<Int32> DeleteKundeCommand { get; }

        private DispatcherTimer DispatcherTimer { get; }

        public MainViewModel(AutoReservationService service)
        {
            _service = service;

            AddAutoCommand = new RelayCommand(AddAuto);
            AddKundeCommand = new RelayCommand(AddKunde);
            AddReservationCommand = new RelayCommand(AddReservation);
            DeleteKundeCommand = new RelayCommand<Int32>(DeleteKunde);

            //update lists every 5 seconds
            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Tick += UpdateLists;
            DispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            DispatcherTimer.Start();
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

        private void DeleteKunde(int id)
        {
            System.Console.WriteLine("Wahbadabadaba: " + id);
        }

        private void UpdateLists(object sender, EventArgs e)
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
