using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    [Table("Kunde")]
    public class Kunde
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20), Required]
        public string Nachname { get; set; }

        [MaxLength(20), Required]
        public string Vorname { get; set; }

        [Required]
        public DateTime Geburtsdatum { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual ICollection<Reservation> Reservationen { get; set; }
    }
}
