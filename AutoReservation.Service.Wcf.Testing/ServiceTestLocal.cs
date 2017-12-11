using AutoReservation.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public class ServiceTestLocal : ServiceTestBase
    {
        private IAutoReservationService target;
        protected override IAutoReservationService Target => target ?? (target = new AutoReservationService());
    }
}