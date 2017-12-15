using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AutoReservation.BusinessLayer.Exceptions;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class ReservationDateRangeTest
    {
        private ReservationManager _target;
        private ReservationManager Target => _target ?? (_target = new ReservationManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void DateRangeVeryLarge()
        {
            var reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 2,
                Von = new DateTime(2020, 02, 1),
                Bis = new DateTime(2020, 12, 1)
            };
            Target.Add(reservation);
        }

        [TestMethod]
        public void DateRangeVerySmall()
        {
            var reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 2,
                Von = new DateTime(2020, 02, 1),
                Bis = new DateTime(2020, 02, 2)
            };
            Target.Add(reservation);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDateRangeException))]
        public void VeryVerySmallDateRange()
        {
            //one second long reservation
            var reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 2,
                Von = new DateTime(2020, 02, 1, 0, 0, 0),
                Bis = new DateTime(2020, 02, 1, 0, 0, 1)
            };
            Target.Add(reservation);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDateRangeException))]
        public void VerySmallDateRange()
        {
            //12 hour long reservation
            var reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 2,
                Von = new DateTime(2020, 02, 1, 0, 0, 0),
                Bis = new DateTime(2020, 02, 1, 12, 0, 0)
            };
            Target.Add(reservation);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDateRangeException))]
        public void AlmostReasonableDateRange()
        {
            //23 hours 59 minutes and 59 seconds long reservation
            var reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 2,
                Von = new DateTime(2020, 02, 1, 12, 0, 0),
                Bis = new DateTime(2020, 02, 2, 11, 59, 59)
            };
            Target.Add(reservation);
        }
    }
}
