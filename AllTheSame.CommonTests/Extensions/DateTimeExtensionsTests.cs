using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllTheSame.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
namespace AllTheSame.Common.Extensions.Tests
{
    [TestClass()]
    public class DateTimeExtensionsTests
    {
        [TestMethod()]
        public void GetDateTimeStringTest()
        {
            string expected = "1/1/2001 4:25:00 PM";
            DateTime dt = DateTime.Parse("01/01/2001 4:25 PM");

            string actual = dt.GetDateTimeString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsBetweenTest()
        {
            DateTime dt1 = DateTime.Parse("01/01/2001").Date;
            DateTime dt2 = DateTime.Parse("04/01/2001").Date;
            DateTime dt3 = DateTime.Parse("08/01/2001").Date;

            Assert.IsTrue(dt2.IsBetween(dt1, dt3) == true);
            Assert.IsTrue(dt3.IsBetween(dt1, dt2) == false);
        }

        [TestMethod()]
        public void ToFriendlyDateStringTest()
        {
            DateTime dt1 = DateTime.Parse("01/01/2001");
            var exp1 = "January 01, 2001 @ 12:00 am";
            var fd1 = dt1.ToFriendlyDateString();

            Assert.IsNotNull(fd1);
            Assert.AreEqual(exp1, fd1);

            DateTime dt2 = DateTime.Parse("02/29/2000").Date;
            var exp2 = "February 29, 2000 @ 12:00 am";
            var fd2 = dt2.ToFriendlyDateString();

            Assert.IsNotNull(fd2);
            Assert.AreEqual(exp2, fd2);
        }

        [TestMethod()]
        public void GetTimeSpanTillNowTest()
        {
            DateTime dt1 = DateTime.Parse("01/01/2001");
            var span = dt1.GetTimeSpanTillNow();
            var dtNow = DateTime.Now.Subtract(dt1).Duration();

            Assert.IsNotNull(span);
            Assert.IsTrue(span.Duration() != null);
            Assert.IsTrue(dtNow == span);
        }

        [TestMethod()]
        public void GetTimeSpanTillNowTest1()
        {
            DateTime dt1 = DateTime.Parse("01/01/2001");
            var span = dt1.GetTimeSpanTillNow();
            var dtNow = DateTime.Now.Subtract(dt1).Duration();

            Assert.IsNotNull(span);

            Thread.Sleep(1);
            //re-grab now, again check if is now greater than span
            var dtNow2 = DateTime.Now.Subtract(dt1).Duration();
            Assert.IsTrue(dtNow2 > span);
        }
    }
}
