using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class AutoUpdateTests
    {
        private AutoManager _target;
        private AutoManager Target => _target ?? (_target = new AutoManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void UpdateAutoTest()
        {
            var vwGolf = Target.Get(2);
            vwGolf.Tagestarif = 100;
            Target.Update(vwGolf);
            
            Assert.AreEqual(100, Target.Get(2).Tagestarif);
        }
    }
}
