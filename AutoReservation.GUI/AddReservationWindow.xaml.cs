using AutoReservation.GUI.ViewModels;
using AutoReservation.Service.Wcf;

namespace AutoReservation.GUI
{
    public partial class AddReservationWindow
    {
        public AddReservationWindow(AutoReservationService service)
        {
            InitializeComponent();
            DataContext = new ReservationViewModel(this, service);
        }
    }
}
