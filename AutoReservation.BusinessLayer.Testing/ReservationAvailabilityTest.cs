using System;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class ReservationAvailabilityTest
    {
        private ReservationManager _target;
        private ReservationManager Target => _target ?? (_target = new ReservationManager());

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void ReservationEndsJustBeforeAnother()
        {
            var reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 2,
                Von = new DateTime(2020, 01, 8),
                Bis = new DateTime(2020, 01, 10)
            };
            Target.Add(reservation);
        }

        [TestMethod]
        public void ReservationStartsJustAfterAnother()
        {
            var reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 2,
                Von = new DateTime(2020, 01, 20),
                Bis = new DateTime(2020, 01, 22)
            };
            Target.Add(reservation);
        }

        [TestMethod]
        [ExpectedException(typeof(UnavailableAutoException))]
        public void OverlapAtStart()
        {
            var reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 2,
                Von = new DateTime(2020, 01, 19),
                Bis = new DateTime(2020, 01, 21)
            };
            Target.Add(reservation);
        }

        [TestMethod]
        [ExpectedException(typeof(UnavailableAutoException))]
        public void OverlapAtEnd()
        {
            var reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 2,
                Von = new DateTime(2020, 01, 9),
                Bis = new DateTime(2020, 01, 11)
            };
            Target.Add(reservation);
        }

        [TestMethod]
        [ExpectedException(typeof(UnavailableAutoException))]
        public void FullOverlapOutside()
        {
            var reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 2,
                Von = new DateTime(2020, 01, 9),
                Bis = new DateTime(2020, 01, 21)
            };
            Target.Add(reservation);
        }

        [TestMethod]
        [ExpectedException(typeof(UnavailableAutoException))]
        public void FullOverlapInside()
        {
            var reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 2,
                Von = new DateTime(2020, 01, 14),
                Bis = new DateTime(2020, 01, 16)
            };
            Target.Add(reservation);
        }
    }
}
