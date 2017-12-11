using System;
using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    [DataContract]
    public class OptimisticConcurrencyFault<T>
    {
        public OptimisticConcurrencyFault()
        {
            Message = $"{typeof(T).Name} wurde in der Zwischenzeit schon bearbeitet";
        }

        [DataMember]
        public string Message { get; set; }
    }
}