using AutoReservation.TestEnvironment;
using AutoReservation.Gui.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Input;

namespace AutoReservation.Gui.Testing
{
    [TestClass]
    public class ViewModelTest
    {
        private ReservationViewModel viewModel;

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
            viewModel = new ReservationViewModel();
        }

        [TestMethod]
        public void AutosLoadTest()
        {
            ICommand loadCommand = viewModel.LoadCommand;
            loadCommand.Execute(null);
            Assert.AreEqual(viewModel.Autos.Count, 3);
        }

        [TestMethod]
        public void KundenLoadTest()
        {
            ICommand loadCommand = viewModel.LoadCommand;
            loadCommand.Execute(null);
            Assert.AreEqual(viewModel.Kunden.Count, 4);
        }

        [TestMethod]
        public void ReservationenLoadTest()
        {
            ICommand loadCommand = viewModel.LoadCommand;
            loadCommand.Execute(null);
            Assert.AreEqual(viewModel.Reservations.Count, 3);
        }

        [TestMethod]
        public void ViewCanLoadTest()
        {
            ICommand loadCommand = viewModel.LoadCommand;
            Assert.IsTrue(loadCommand.CanExecute(0));
        }
    }
}