using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects.Faults;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        #region Auto

        [OperationContract]
        List<AutoDto> GetAutos();

        [OperationContract]
        AutoDto GetAuto(int id);

        [OperationContract]
        void AddAuto(AutoDto auto);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault<AutoDto>))]
        void UpdateAuto(AutoDto auto);

        [OperationContract]
        void RemoveAuto(AutoDto auto);

        [OperationContract]
        bool IsAutoAvaible(int id);

        #endregion

        #region Kunde

        [OperationContract]
        List<KundeDto> GetKunden();

        [OperationContract]
        KundeDto GetKunde(int id);

        [OperationContract]
        void AddKunde(KundeDto kunde);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault<KundeDto>))]
        void UpdateKunde(KundeDto kunde);

        [OperationContract]
        void RemoveKunde(KundeDto kunde);

        #endregion

        #region Reservation

        [OperationContract]
        List<ReservationDto> GetReservationen();

        [OperationContract]
        ReservationDto GetReservation(int id);

        [OperationContract]
        [FaultContract(typeof(InvalidDateRangeFault))]
        [FaultContract(typeof(UnavailableAutoFault))]
        void AddReservation(ReservationDto reservation);

        [FaultContract(typeof(OptimisticConcurrencyFault<ReservationDto>))]
        [FaultContract(typeof(InvalidDateRangeFault))]
        [FaultContract(typeof(UnavailableAutoFault))]
        [OperationContract]
        void UpdateReservation(ReservationDto reservation);

        [OperationContract]
        void RemoveReservation(ReservationDto reservation);

        #endregion
    }
}