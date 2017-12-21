using System;
using System.Collections;
using System.Linq;
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
        public IEnumerable AutoKlassen { get; } = Enum.GetValues(typeof(AutoKlasse)).Cast<AutoKlasse>();

        public AutoViewModel(AddAutoWindow view, AutoReservationService service)
        {
            _view = view;
            _service = service;

            // Todo check if save is possible
            SaveCommand = new RelayCommand(
                Save,
                () => AutoDto.Marke != string.Empty && AutoDto.Tagestarif != 0 && (AutoDto.AutoKlasse != AutoKlasse.Luxusklasse || AutoDto.Basistarif != 0)
            );
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
            set => SetValue(nameof(AutoDto.Marke), value);
        }

        public AutoKlasse AutoKlasse
        {
            get => AutoDto.AutoKlasse;
            set
            {
                SetValue(nameof(AutoDto.AutoKlasse), value);
                //notify that BasistarifHeight may have changed
                OnPropertyChanged(nameof(BasistarifHeight));
            }
        }

        public string Tagestarif
        {
            get => AutoDto.Tagestarif.ToString();
            set
            {
                if (int.TryParse(value, out var intValue))
                {
                    SetValue(nameof(AutoDto.Tagestarif), intValue);
                }
            }
        }

        public string Basistarif
        {
            get => AutoDto.Basistarif.ToString();
            set
            {
                if (int.TryParse(value, out var intValue))
                {
                    SetValue(nameof(AutoDto.Basistarif), intValue);
                }
            }
        }

        private void SetValue(string name, object value)
        {
            SetValue(AutoDto, name, value);
        }
    }
}
