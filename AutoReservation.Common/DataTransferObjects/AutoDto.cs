using System;
using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class AutoDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Marke { get; set; }

        [DataMember]
        public int Tagestarif { get; set; }

        [DataMember]
        public int Basistarif { get; set; }

        [DataMember]
        public AutoKlasse AutoKlasse { get; set; }

        [DataMember]
        public Byte[] RowVersion { get; set; }

        public override string ToString()
            => $"{Id}; {Marke}; {Tagestarif}; {Basistarif}; {AutoKlasse}; {RowVersion}";
    }
}
