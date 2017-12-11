using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    [DataContract]
    public class InvalidDateRangeFault
    {
        public InvalidDateRangeFault()
        {
            Message = "Datumsbreich ist ungültig.";
        }

        [DataMember]
        public string Message { get; set; }
    }
}