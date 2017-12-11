using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    [DataContract]
    public class InvalidDateRangeFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}