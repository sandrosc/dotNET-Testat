using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Dal.Entities;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        #region Read all entities

        [TestMethod]
        public void GetAutosTest()
        {
            var autos = Target.GetAutos();
            Assert.AreEqual(3, autos.Count);
        }

        [TestMethod]
        public void GetKundenTest()
        {
            var kunden = Target.GetKunden();
            Assert.AreEqual(4, kunden.Count);
        }

        [TestMethod]
        public void GetReservationenTest()
        {
            var reservationen = Target.GetReservationen();
            Assert.AreEqual(3, reservationen.Count);
        }

        #endregion

        #region Get by existing ID

        [TestMethod]
        public void GetAutoByIdTest()
        {
            var auto = Target.GetAuto(1);
            Assert.AreEqual("Fiat Punto", auto.Marke);
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            var auto = Target.GetKunde(1);
            Assert.AreEqual("Nass", auto.Nachname);
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            var auto = Target.GetReservation(1);
            Assert.AreEqual(new DateTime(2020, 1, 20), auto.Bis);
        }

        #endregion

        #region Get by not existing ID

        [TestMethod]
        public void GetAutoByIdWithIllegalIdTest()
        {
            var auto = Target.GetAuto(42);
            Assert.IsNull(auto);
        }

        [TestMethod]
        public void GetKundeByIdWithIllegalIdTest()
        {
            var kunde = Target.GetKunde(42);
            Assert.IsNull(kunde);
        }

        [TestMethod]
        public void GetReservationByNrWithIllegalIdTest()
        {
            var reservation = Target.GetReservation(42);
            Assert.IsNull(reservation);
        }

        #endregion

        #region Insert

        [TestMethod]
        public void InsertAutoTest()
        {
            var auto = new AutoDto
            {
                Marke = "Tesla",
                AutoKlasse = AutoKlasse.Luxusklasse,
                Basistarif = 999
            };
            Target.AddAuto(auto);
            var res = Target.GetAuto(4);
            Assert.AreEqual(res.Marke, auto.Marke);
            Assert.AreEqual(res.AutoKlasse, auto.AutoKlasse);
            Assert.AreEqual(res.Basistarif, auto.Basistarif);
        }

        [TestMethod]
        public void InsertKundeTest()
        {
            var kunde = new KundeDto
            {
                Geburtsdatum = new DateTime(2017, 09, 27),
                Nachname = "Vernom",
                Vorname = "Hussler"
            };
            Target.AddKunde(kunde);
            var res = Target.GetKunde(5);
            Assert.AreEqual(res.Geburtsdatum, kunde.Geburtsdatum);
            Assert.AreEqual(res.Nachname, kunde.Nachname);
            Assert.AreEqual(res.Vorname, kunde.Vorname);
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            var reservation = new ReservationDto
            {
                Bis = new DateTime(2017, 09, 27),
                Von = new DateTime(2017, 08, 27),
                Auto = Target.GetAuto(1),
                Kunde = Target.GetKunde(1)
            };
            Target.AddReservation(reservation);
            var res = Target.GetReservation(4);
            Assert.AreEqual(res.Bis, reservation.Bis);
            Assert.AreEqual(res.Von, reservation.Von);
        }

        #endregion

        #region Delete  

        [TestMethod]
        public void DeleteAutoTest()
        {
            var auto = Target.GetAuto(1);
            Target.RemoveAuto(auto);
            Assert.IsNull(Target.GetAuto(1));
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            Target.RemoveKunde(Target.GetKunde(1));
            Assert.IsNull(Target.GetKunde(1));
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            Target.RemoveReservation(Target.GetReservation(1));
            Assert.IsNull(Target.GetReservation(1));
        }

        #endregion

        #region Update

        [TestMethod]
        public void UpdateAutoTest()
        {
            var auto = Target.GetAuto(1);
            auto.Marke = "Tesla";
            Target.UpdateAuto(auto);
            Assert.AreEqual("Tesla", Target.GetAuto(1).Marke);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            var kunde = Target.GetKunde(1);
            kunde.Nachname = "Manisor";
            Target.UpdateKunde(kunde);
            Assert.AreEqual("Manisor", Target.GetKunde(1).Nachname);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            var reservation = Target.GetReservation(1);
            reservation.Bis = new DateTime(2050, 3, 3);
            Target.UpdateReservation(reservation);
            Assert.AreEqual(reservation.Bis, Target.GetReservation(1).Bis);
        }

        #endregion

        #region Update with optimistic concurrency violation

        [TestMethod]
        [ExpectedException(typeof(FaultException<OptimisticConcurrencyFault<Auto>>))]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            var original = Target.GetAuto(1);
            original.Marke = "R_S Star";
            var modified = Target.GetAuto(1);
            modified.Marke = "Z-T Star";

            Target.UpdateAuto(modified);
            Target.UpdateAuto(original);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<OptimisticConcurrencyFault<Kunde>>))]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            var original = Target.GetKunde(1);
            original.Nachname = "Mozart";
            var modified = Target.GetKunde(1);
            modified.Nachname = "Fred";

            Target.UpdateKunde(modified);
            Target.UpdateKunde(original);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<OptimisticConcurrencyFault<Reservation>>))]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            var original = Target.GetReservation(1);
            original.Bis = new DateTime(2018, 10, 10);
            var modified = Target.GetReservation(1);
            modified.Bis = new DateTime(2018, 11, 11);

            Target.UpdateReservation(modified);
            Target.UpdateReservation(original);
        }

        #endregion

        #region Insert / update invalid time range

        [TestMethod]
        [ExpectedException(typeof(FaultException<InvalidDateRangeFault>))]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            var reservation = new ReservationDto
            {
                Bis = new DateTime(2017, 09, 27),
                Von = new DateTime(2017, 10, 27),
                Auto = Target.GetAuto(1),
                Kunde = Target.GetKunde(1)
            };
            Target.AddReservation(reservation);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<UnavailableAutoFault>))]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            var reservation = new ReservationDto
            {
                Bis = new DateTime(2017, 09, 27),
                Von = new DateTime(2017, 08, 27),
                Auto = Target.GetAuto(1),
                Kunde = Target.GetKunde(1)
            };
            Target.AddReservation(reservation);

            var reservation2 = new ReservationDto
            {
                Bis = new DateTime(2017, 10, 27),
                Von = new DateTime(2017, 08, 27),
                Auto = Target.GetAuto(1),
                Kunde = Target.GetKunde(1)
            };
            Target.AddReservation(reservation2);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<InvalidDateRangeFault>))]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            var reservation = Target.GetReservation(1);
            reservation.Bis = new DateTime(2017, 09, 27);
            reservation.Von = new DateTime(2017, 10, 27);
            Target.UpdateReservation(reservation);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<UnavailableAutoFault>))]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            var reservation = Target.GetReservation(1);
            reservation.Auto = Target.GetAuto(2);
            Target.UpdateReservation(reservation);
        }

        #endregion

        #region Check Availability

        [TestMethod]
        public void CheckAvailabilityIsTrueTest()
        {
            // Assert.IsTrue(Target.IsAutoAvaible(1, new DateTime(2021, 01, 01), new DateTime(2021, 01, 10)));
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void CheckAvailabilityIsFalseTest()
        {
            // Assert.IsFalse(Target.IsAutoAvaible(1, new DateTime(2020, 01, 10), new DateTime(2020, 01, 20)));
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion
    }
}