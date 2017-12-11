using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal.Entities;

namespace AutoReservation.BusinessLayer
{
	public class ReservationManager
			: ManagerBase<Reservation>
	{
		public new void Add(Reservation reservation)
		{
			if (!reservation.DateRangeIsValid())
			{
				throw new InvalidDateRangeException(
						"Datumsbereich erfüllt die Vorgabe, nach der eine Reservation mindestens einen Tag dauert, nicht.",
						reservation);
			}
			if (!reservation.AutoIsAvailable())
			{
				throw new UnavailableAutoException(
						"Das Auto ist zu diesem Zeitpunkt leider nicht verfügbar.", reservation.Auto);
			}
			base.Add(reservation);
		}
	}
}