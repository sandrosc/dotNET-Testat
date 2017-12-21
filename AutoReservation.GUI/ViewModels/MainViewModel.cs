using System.Collections.ObjectModel;
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

        public MainViewModel(AutoReservationService service)
        {
            _service = service;

            //fill lists
            UpdateLists();
        }
        
        private void UpdateLists()
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
        }
    }
}
