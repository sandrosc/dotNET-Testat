using AutoReservation.GUI.ViewModels;
using AutoReservation.Service.Wcf;

namespace AutoReservation.GUI
{
    public partial class AddAutoWindow
    {
        public AddAutoWindow(AutoReservationService service)
        {
            InitializeComponent();
            DataContext = new AutoViewModel(this, service);
        }
    }
}
