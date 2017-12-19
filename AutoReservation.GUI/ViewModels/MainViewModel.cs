using System.Collections.ObjectModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Gui.ViewModels;
using AutoReservation.Service.Wcf;

namespace AutoReservation.GUI.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private readonly AutoReservationService _service;
        public ObservableCollection<AutoDto> Autos = new ObservableCollection<AutoDto>();
        public ObservableCollection<KundeDto> Kunden = new ObservableCollection<KundeDto>();

        public MainViewModel(AutoReservationService service)
        {
            _service = service;

            //fill lists
            UpdateLists();
        }
        
        private void UpdateLists()
        {
            Autos = new ObservableCollection<AutoDto>(_service.GetAutos());
            Kunden = new ObservableCollection<KundeDto>(_service.GetKunden());
        }
    }
}
