using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        private AutoManager _autoManager;
        private KundeManager _kundeManager;
        private ReservationManager _reservationManager;

        public AutoReservationService()
        {
            _autoManager = new AutoManager();
            _kundeManager = new KundeManager();
            _reservationManager = new ReservationManager();
        }

        #region Auto

        public List<AutoDto> SelectAutos()
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        public AutoDto SelectAuto(int id)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        public void InsertAuto(AutoDto auto)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        public void UpdateAuto(AutoDto auto)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        public void DeleteAuto(AutoDto auto)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        public bool IsAutoAvaible(int id)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        #endregion

        #region Kunde

        public List<KundeDto> SelectKunden()
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        public KundeDto SelectKunde(int id)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        public void InsertKunde(KundeDto kunde)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        public void UpdateKunde(KundeDto kunde)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        public void DeleteKunde(KundeDto kunde)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        #endregion

        #region Reservation

        public List<ReservationDto> SelectReservationen()
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        public KundeDto SelectReservation(int id)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        public void InsertReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        public void UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        #endregion

        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");
    }
}