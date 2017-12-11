using System;
using AutoReservation.Dal.Entities;

namespace AutoReservation.BusinessLayer.Exceptions
{
    public class UnavailableAutoException : Exception
    {
        public UnavailableAutoException(string message) : base(message)
        { }

        public UnavailableAutoException(string message, Auto auto) : base(message)
        {
            Auto = auto;
        }

        public Auto Auto { get; }
    }
}
