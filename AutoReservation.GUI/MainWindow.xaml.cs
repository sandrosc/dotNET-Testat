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

        private void AddAuto_OnClick(object sender, RoutedEventArgs e)
        {
            var addAutoWindow = new AddAutoWindow();
            addAutoWindow.Show();
        }

        private void AddKunde_OnClick(object sender, RoutedEventArgs e)
        {
            var addKundeWindow = new AddKundeWindow();
            addKundeWindow.Show();
        }
    }
}
