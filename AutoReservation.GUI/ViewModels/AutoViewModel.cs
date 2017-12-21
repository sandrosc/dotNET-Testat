using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Gui.ViewModels;
using AutoReservation.Service.Wcf;

namespace AutoReservation.GUI.ViewModels
{
    class AutoViewModel : BaseViewModel
    {
        private readonly AddAutoWindow _view;
        private readonly AutoReservationService _service;
        public RelayCommand SaveCommand { get; }
        public AutoDto AutoDto { get; } = new AutoDto();

        public AutoViewModel(AddAutoWindow view, AutoReservationService service)
        {
            _view = view;
            _service = service;
        }

        private void Save()
        {
            
        }
    }
}
