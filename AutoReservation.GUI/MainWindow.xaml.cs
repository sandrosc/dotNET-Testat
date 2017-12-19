using System.Threading.Tasks;
using System.Windows;
using AutoReservation.GUI.ViewModels;
using AutoReservation.Service.Wcf;

namespace AutoReservation.GUI
{
    public partial class MainWindow
    {
        private static readonly AutoReservationService AutoReservationService = new AutoReservationService();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(AutoReservationService);
        }

        private void AddAuto_OnClick(object sender, RoutedEventArgs e)
        {
            var addAutoWindow = new AddAutoWindow();
            addAutoWindow.ShowDialog();
        }

        private void AddKunde_OnClick(object sender, RoutedEventArgs e)
        {
            var addKundeWindow = new AddKundeWindow(AutoReservationService);
            addKundeWindow.ShowDialog();
        }
    }
}
