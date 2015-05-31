using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Extentions.Common;

namespace Extentions.Tests
{
    [TestClass]
    public class TestsDateTimeExtender
    {
        [TestMethod]
        public void GetDateForDayOfWeekShoulReturnProperDates()
        {
            DateTime wednestday = new DateTime(2015, 5, 27);
            DateTime monday = new DateTime(2015, 5, 25);
            DateTime tuesday = new DateTime(2015, 5, 26);
            DateTime sunday = new DateTime(2015, 5, 31);

            DateTime mondayAsResult = wednestday.GetDateForDayOfWeek(DayOfWeek.Monday);
            DateTime tuestdayAsResult = wednestday.GetDateForDayOfWeek(DayOfWeek.Tuesday);
            DateTime sundayAsResult = wednestday.GetDateForDayOfWeek(DayOfWeek.Sunday);
            
            Assert.AreEqual(monday, mondayAsResult);
            Assert.AreEqual(tuesday, tuestdayAsResult);
            Assert.AreEqual(sunday, sundayAsResult);
        }
    }
}
