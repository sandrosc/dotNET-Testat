using System.Threading.Tasks;
using System.Windows;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Gui.ViewModels;
using AutoReservation.Service.Wcf;

namespace AutoReservation.GUI.ViewModels
{
    public class AutoViewModel : BaseViewModel
    {
        private readonly AddAutoWindow _view;
        private readonly AutoReservationService _service;
        public RelayCommand SaveCommand { get; }
        public AutoDto AutoDto { get; } = new AutoDto();

        public AutoViewModel(AddAutoWindow view, AutoReservationService service)
        {
            _view = view;
            _service = service;

            // Todo check if save is possible
            SaveCommand = new RelayCommand(Save, () => (AutoDto.Basistarif != 0));
        }

        private void Save()
        {
            Task.Run(() =>
            {
                _service.AddAuto(AutoDto);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _view.Close();
                });
            });
        }

        public int BasistarifHeight => AutoDto.AutoKlasse == AutoKlasse.Luxusklasse ? 32 : 0;

        public string Marke
        {
            get => AutoDto.Marke;
            set => SetValue(nameof(Marke), value);
        }

        public AutoKlasse AutoKlasse
        {
            get => AutoDto.AutoKlasse;
            set => SetValue(nameof(AutoKlasse), value);
        }

        public string Tagestarif
        {
            get => AutoDto.Marke;
            set => SetValue(nameof(Tagestarif), value);
        }

        public string Basistarif
        {
            get => AutoDto.Marke;
            set => SetValue(nameof(Basistarif), value);
        }

        private void SetValue(string name, object value)
        {
            SetValue(AutoDto, name, value);
        }
    }
}
