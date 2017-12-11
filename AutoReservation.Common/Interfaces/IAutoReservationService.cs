using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using System.ServiceModel;

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
        void InsertReservation(ReservationDto reservation);

        [OperationContract]
        void UpdateReservation(ReservationDto reservation);

        [OperationContract]
        void DeleteReservation(ReservationDto reservation);
        #endregion
    }
}
