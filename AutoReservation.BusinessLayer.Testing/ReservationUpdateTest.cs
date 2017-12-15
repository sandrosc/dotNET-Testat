using System;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class ReservationUpdateTest
    {
        private ReservationManager _target;
        private ReservationManager Target => _target ?? (_target = new ReservationManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            var reservation = Target.Get(1);
            reservation.Auto = new AutoManager().Get(2);
            reservation.AutoId = 2;
            reservation.Von = new DateTime(2020, 02, 10);
            reservation.Bis = new DateTime(2020, 02, 12);

            Target.Update(reservation);

            Assert.AreEqual(2, Target.Get(1).AutoId);
        }
    }
}
