using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;

namespace AutoReservation.BusinessLayer
{
    public class ReservationManager
        : ManagerBase<Reservation>
    {
        public override Reservation Get(int id)
        {
            using (var context = new AutoReservationContext())
            {
                return context.Reservationen
                    .Include(r => r.Auto)
                    .Include(r => r.Kunde)
                    .FirstOrDefault(r => r.ReservationsNr == id);
            }
        }

        public override List<Reservation> GetList()
        {
            using (var context = new AutoReservationContext())
            {
                return context.Reservationen
                    .Include(r => r.Auto)
                    .Include(r => r.Kunde)
                    .ToList();
            }
        }

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
                try
                {
                    context.Entry(reservation).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateOptimisticConcurrencyException(context, reservation);
                }
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

        public bool AutoIsAvailable(int id, DateTime von, DateTime bis)
        {
            using (var context = new AutoReservationContext())
            {
                return AutoIsAvailable(context, new Reservation
                {
                    AutoId = id,
                    Von = von,
                    Bis = bis
                });
            }
        }
    }
}