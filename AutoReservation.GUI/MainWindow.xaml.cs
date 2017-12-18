using System.Windows;
using AutoReservation.Service.Wcf;

namespace AutoReservation.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AutoReservationService _autoReservationService;

        private AutoReservationService AutoReservationService =>
            _autoReservationService ?? (_autoReservationService = new AutoReservationService());

        public MainWindow()
        {
            InitializeComponent();
            UpdateLists();
        }

        private void UpdateLists()
        {
            DataGridAutos.ItemsSource = AutoReservationService.GetAutos();
        }
    }
}
