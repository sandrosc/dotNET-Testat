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
        List<AutoDto> SelectAutos();

        [OperationContract]
        AutoDto SelectAuto(int id);

        [OperationContract]
        void InsertAuto(AutoDto auto);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault<AutoDto>))]
        void UpdateAuto(AutoDto auto);

        [OperationContract]
        void DeleteAuto(AutoDto auto);

        [OperationContract]
        bool IsAutoAvaible(int id);

        #endregion

        #region Kunde

        [OperationContract]
        List<KundeDto> SelectKunden();

        [OperationContract]
        KundeDto SelectKunde(int id);

        [OperationContract]
        void InsertKunde(KundeDto kunde);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault<KundeDto>))]
        void UpdateKunde(KundeDto kunde);

        [OperationContract]
        void DeleteKunde(KundeDto kunde);

        #endregion

        #region Reservation

        [OperationContract]
        List<ReservationDto> SelectReservationen();

        [OperationContract]
        KundeDto SelectReservation(int id);

        [OperationContract]
        [FaultContract(typeof(InvalidDateRangeFault))]
        [FaultContract(typeof(UnavailableAutoFault))]
        void InsertReservation(ReservationDto reservation);

        [FaultContract(typeof(OptimisticConcurrencyFault<ReservationDto>))]
        [FaultContract(typeof(InvalidDateRangeFault))]
        [FaultContract(typeof(UnavailableAutoFault))]
        [OperationContract]
        void UpdateReservation(ReservationDto reservation);

        [OperationContract]
        void DeleteReservation(ReservationDto reservation);

        #endregion
    }
}