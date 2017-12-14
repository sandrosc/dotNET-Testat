using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class KundeUpdateTest
    {
        private KundeManager _target;
        private KundeManager Target => _target ?? (_target = new KundeManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            //Update Anna Nass to Anna Wet
            var anna = Target.Get(1);
            anna.Nachname = "Wet";
            Target.Update(anna);

            Assert.AreEqual(Target.Get(1).Nachname, "Wet");
        }
    }
}
