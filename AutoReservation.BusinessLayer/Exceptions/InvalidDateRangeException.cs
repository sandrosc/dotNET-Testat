using System;
using AutoReservation.Dal.Entities;

namespace AutoReservation.BusinessLayer.Exceptions
{
    public class InvalidDateRangeException : Exception
    {
        public InvalidDateRangeException(string message) : base(message)
        {
        }

        public InvalidDateRangeException(string message, Reservation reservation) : base(message)
        {
            Reservation = reservation;
        }

        public Reservation Reservation { get; }
    }
}
