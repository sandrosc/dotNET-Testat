using System.Windows;
using System.Windows.Controls;
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

        private void DeleteKundeCommand(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (e.Command == DataGrid.DeleteCommand)

            {
                if (MessageBox.Show("Are you sure you want to delete?", "Please confirm.", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)

                {
                    // TODO: delete
                    MessageBox.Show(sender.ToString());
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
    }
}