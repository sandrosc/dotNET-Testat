using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
            
            //reservation.Auto = new StandardAuto(){Autoklasse = };

            Assert.AreEqual("a", "a");
        }
    }
}
