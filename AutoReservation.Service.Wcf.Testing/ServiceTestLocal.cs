using AutoReservation.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public class ServiceTestLocal : ServiceTestBase
    {
        private IAutoReservationService _target;
        protected override IAutoReservationService Target => _target ?? (_target = new AutoReservationService());
    }
}