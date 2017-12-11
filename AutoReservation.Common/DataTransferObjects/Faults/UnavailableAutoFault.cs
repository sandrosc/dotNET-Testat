using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    [DataContract]
    public class UnavailableAutoFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}