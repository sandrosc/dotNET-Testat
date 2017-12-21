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
    }
}
