using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TG.Package.Lottery;
namespace LotteryTest
{
    [TestFixture]
    public class AddTwoColor
    {
        #region true
        [Test]
        public static void AddTrueTwoColor()
        {
            String red = "01,02,03,04,05,06";
            String blue = "01,02";
            TwoColor tc = new TwoColor("321321231");
            tc.Add(red, blue);

            Assert.AreEqual(tc.TotalBet, 2);
        }
        [Test]
        public static void AddDanTrueTwoColor()
        {
            String red = "01,02,03,04,05,06";
            String blue = "01,02";
            String dan = "07,09";
            TwoColor tc = new TwoColor("321321231");
            tc.Add(red, blue, dan);

            Assert.AreEqual(tc.TotalBet, 30);
        }
        #endregion

        #region false
        [Test]
        public static void AddTwoColorWithErrorSplit()
        {
            String red = "01,02,03,04,05a06";
            String blue = "01,02";
            TwoColor tc = new TwoColor("321321231");
            try
            {
                tc.Add(red, blue);
            }
            catch (FormatException)
            {
                Assert.True(true);
            }
        }
        [Test]
        public static void AddTwoColorWithErrorNumber()
        {
            String red = "01,02,03,04,05,0a";
            String blue = "01,02";
            TwoColor tc = new TwoColor("321321231");
            try
            {
                tc.Add(red, blue);
            }
            catch (FormatException)
            {
                Assert.True(true);
            }
        }
        [Test]
        public static void AddTwoColorWithErrorMaxNumber()
        {
            String red = "01,02,03,04,05,34";
            String blue = "01,02";
            TwoColor tc = new TwoColor("321321231");
            try
            {
                tc.Add(red, blue);
            }
            catch (FormatException)
            {
                Assert.True(true);
            }
        }
        [Test]
        public static void AddTwoColorWithErrorMinNumber()
        {
            String red = "00,02,03,04,05,06";
            String blue = "01,02";
            TwoColor tc = new TwoColor("321321231");
            try
            {
                tc.Add(red, blue);
            }
            catch (FormatException)
            {
                Assert.True(true);
            }
        }
        #endregion
    }
}
