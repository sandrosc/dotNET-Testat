<<<<<<< Updated upstream
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    [Table("Reservation")]
    public class Reservation
    {
        [Key, Column("Id")]
        public int ReservationsNr { get; set; }

        [Required]
        public int AutoId { get; set; }

        [Required]
        public int KundeId { get; set; }

        [Required]
        public DateTime Von { get; set; }

        [Required]
        public DateTime Bis { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("AutoId")]
        public virtual Auto Auto { get; set; }

        [ForeignKey("KundeId")]
        public virtual Kunde Kunde { get; set; }
    }
}
=======
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AutoReservation.Dal.Entities
{
    [Table("Reservation")]
    public class Reservation
    {
        [Key, Column("Id")]
        public int ReservationsNr { get; set; }

        [Required]
        public int AutoId { get; set; }

        [Required]
        public int KundeId { get; set; }

        [Required]
        public DateTime Von { get; set; }

        [Required]
        public DateTime Bis { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("AutoId")]
        public virtual Auto Auto { get; set; }

        [ForeignKey("KundeId")]
        public virtual Kunde Kunde { get; set; }
        
        public bool DateRangeIsValid()
        {
            return (Von < Bis && (Bis - Von).TotalDays >= 1);
        }

        public bool AutoIsAvailable()
        {
            return Auto.Reservationen.Any(r => r != this && r.Von < Bis && r.Bis > Von);
        }
    }
}
>>>>>>> Stashed changes
