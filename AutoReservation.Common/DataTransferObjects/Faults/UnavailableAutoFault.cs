using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    [DataContract]
    public class UnavailableAutoFault
    {
        public UnavailableAutoFault()
        {
            Message = "Auto ist nicht verfügbar.";
        }

        [DataMember]
        public string Message { get; set; }
    }
}