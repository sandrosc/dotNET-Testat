using AutoReservation.GUI.ViewModels;
using AutoReservation.Service.Wcf;

namespace AutoReservation.GUI
{
    public partial class AddReservationWindow
    {
        public AddReservationWindow(AutoReservationService service)
        {
            InitializeComponent();
            DataContext = new KundeViewModel(this, service);
        }

        //private void AddKunde_Click(object sender, RoutedEventArgs e)
        //{
        //    if (Nachname.Text == "" || Vorname.Text == "" || !Geburtstagsdatum.SelectedDate.HasValue)
        //    {
        //        MessageBox.Show("Nicht alle Daten ausgefüllt!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    else
        //    {
        //        var kunde = new KundeDto()
        //        {
        //            Geburtsdatum = Geburtstagsdatum.SelectedDate ?? DateTime.Now,
        //            Nachname = Nachname.Text,
        //            Vorname = Vorname.Text
        //        };
        //    }
        //}
    }
}
