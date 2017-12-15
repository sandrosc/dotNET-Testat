using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class AutoKlasseTest
    {
        private AutoManager _target;
        private AutoManager Target => _target ?? (_target = new AutoManager());

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void CarTypesGetRecognized()
        {
            var autos = Target.GetList();
            Assert.IsTrue(autos[0] is StandardAuto);
            Assert.IsTrue(autos[1] is MittelklasseAuto);
            Assert.IsTrue(autos[2] is LuxusklasseAuto);
        }
    }
}
