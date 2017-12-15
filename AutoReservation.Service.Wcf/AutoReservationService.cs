using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using AutoReservation.BusinessLayer;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal.Entities;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        private readonly AutoManager _autoManager;
        private readonly KundeManager _kundeManager;
        private readonly ReservationManager _reservationManager;

        public AutoReservationService()
        {
            _autoManager = new AutoManager();
            _kundeManager = new KundeManager();
            _reservationManager = new ReservationManager();
        }

        #region Auto

        public List<AutoDto> GetAutos()
        {
            WriteActualMethod();
            return _autoManager.GetList().ConvertToDtos();
        }

        public AutoDto GetAuto(int id)
        {
            WriteActualMethod();
            return _autoManager.Get(id).ConvertToDto();
        }

        public void AddAuto(AutoDto auto)
        {
            WriteActualMethod();
            _autoManager.Add(auto.ConvertToEntity());
        }

        public void UpdateAuto(AutoDto auto)
        {
            WriteActualMethod();
            try
            {
                _autoManager.Update(auto.ConvertToEntity());
            }
            catch (OptimisticConcurrencyException<Auto>)
            {
                throw new FaultException<OptimisticConcurrencyFault<Auto>>(new OptimisticConcurrencyFault<Auto>()) { };
            }
        }

        public void RemoveAuto(AutoDto auto)
        {
            WriteActualMethod();
            try
            {
                _autoManager.Remove(auto.ConvertToEntity());
            }
            catch (OptimisticConcurrencyException<Auto>)
            {
                throw new FaultException<OptimisticConcurrencyFault<Auto>>(new OptimisticConcurrencyFault<Auto>()) { };
            }
        }

        public bool IsAutoAvailable(int id, DateTime von, DateTime bis)
        {
            WriteActualMethod();
            return _reservationManager.AutoIsAvailable(id, von, bis);
        }

        #endregion

        #region Kunde

        public List<KundeDto> GetKunden()
        {
            WriteActualMethod();
            return _kundeManager.GetList().ConvertToDtos();
        }

        public KundeDto GetKunde(int id)
        {
            WriteActualMethod();
            return _kundeManager.Get(id).ConvertToDto();
        }

        public void AddKunde(KundeDto kunde)
        {
            WriteActualMethod();
            _kundeManager.Add(kunde.ConvertToEntity());
        }

        public void UpdateKunde(KundeDto kunde)
        {
            WriteActualMethod();
            try
            {
                _kundeManager.Update(kunde.ConvertToEntity());
            }
            catch (OptimisticConcurrencyException<Kunde>)
            {
                throw new FaultException<OptimisticConcurrencyFault<Kunde>>(new OptimisticConcurrencyFault<Kunde>()) { };
            }
        }

        public void RemoveKunde(KundeDto kunde)
        {
            WriteActualMethod();
            try
            {
                _kundeManager.Remove(kunde.ConvertToEntity());
            }
            catch (OptimisticConcurrencyException<Kunde>)
            {
                throw new FaultException<OptimisticConcurrencyFault<Kunde>>(new OptimisticConcurrencyFault<Kunde>()) { };
            }
        }

        #endregion

        #region Reservation

        public List<ReservationDto> GetReservationen()
        {
            WriteActualMethod();
            return _reservationManager.GetList().ConvertToDtos();
        }

        public ReservationDto GetReservation(int id)
        {
            WriteActualMethod();
            return _reservationManager.Get(id).ConvertToDto();
        }

        public void AddReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            try
            {
                _reservationManager.Add(reservation.ConvertToEntity());
            }
            catch (OptimisticConcurrencyException<Reservation>)
            {
                throw new FaultException<OptimisticConcurrencyFault<Reservation>>(new OptimisticConcurrencyFault<Reservation>()) { };
            }
            catch (InvalidDateRangeException)
            {
                throw new FaultException<InvalidDateRangeFault>(new InvalidDateRangeFault()) { };
            }
            catch (UnavailableAutoException)
            {
                throw new FaultException<UnavailableAutoFault>(new UnavailableAutoFault()) { };
            }
        }

        public void UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            try
            {
                _reservationManager.Update(reservation.ConvertToEntity());
            }
            catch (OptimisticConcurrencyException<Reservation>)
            {
                throw new FaultException<OptimisticConcurrencyFault<Reservation>>(new OptimisticConcurrencyFault<Reservation>()) { };
            }
            catch (InvalidDateRangeException)
            {
                throw new FaultException<InvalidDateRangeFault>(new InvalidDateRangeFault()) { };
            }
            catch (UnavailableAutoException)
            {
                throw new FaultException<UnavailableAutoFault>(new UnavailableAutoFault()) { };
            }
        }

        public void RemoveReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            try
            {
                _reservationManager.Remove(reservation.ConvertToEntity());
            }
            catch (OptimisticConcurrencyException<Reservation>)
            {
                throw new FaultException<OptimisticConcurrencyFault<Reservation>>(new OptimisticConcurrencyFault<Reservation>()) { };
            }
        }

        #endregion

        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");
    }
}