using System.Data.Entity;
using System.Linq;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;

namespace AutoReservation.BusinessLayer
{
    public class ReservationManager
        : ManagerBase<Reservation>
    {
        public override void Add(Reservation reservation)
        {
            using (var context = new AutoReservationContext())
            {
                if (!ModificationIsValid(context, reservation)) return;
                context.Entry(reservation).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public override void Update(Reservation reservation)
        {
            using (var context = new AutoReservationContext())
            {
                if (!ModificationIsValid(context, reservation)) return;
                context.Entry(reservation).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private static bool ModificationIsValid(AutoReservationContext context, Reservation reservation)
        {
            if (!reservation.DateRangeIsValid)
            {
                throw new InvalidDateRangeException(
                    "Datumsbereich erfüllt die Vorgabe, nach der eine Reservation mindestens einen Tag dauert, nicht.",
                    reservation);
            }
            if (!AutoIsAvailable(context, reservation))
            {
                throw new UnavailableAutoException(
                    "Das Auto ist zu diesem Zeitpunkt leider nicht verfügbar.", reservation.Auto);
            }

            return true;
        }

        private static bool AutoIsAvailable(AutoReservationContext context, Reservation reservation)
        {
            return context.Reservationen
                .Where(r => r.AutoId == reservation.AutoId && r.ReservationsNr != reservation.ReservationsNr)
                .All(r => r.Bis <= reservation.Von || r.Von >= reservation.Bis);
        }
    }
}