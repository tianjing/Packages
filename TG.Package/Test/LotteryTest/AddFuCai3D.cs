using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TG.Package.Lottery;
using TG.Package.Lottery.Base;
namespace LotteryTest
{
    [TestFixture]
    public class AddFuCai3D
    {
        #region true
        [Test]
        public static void TrueAdd()
        {
            String one = "1,2,3,4,5,6";
            String ten = "1,2";
            String hundred = "0,2";
            FuCai3D fc = new FuCai3D("321321231");
            fc.Add(one, ten, hundred);

            Assert.AreEqual(fc.TotalBet, 24);
        }
        [Test]
        public static void TrueAddDan()
        {
            String dan = "1,2";
            String tuo = "7,8,9,0";
            FuCai3D fc = new FuCai3D("321321231");
            fc.Add(dan, tuo,PlayType.Normal|PlayType.DanTuo);

            Assert.AreEqual(fc.TotalBet, 24);
        }
        [Test]
        public static void TrueAddZu3()
        {
            String zu3 = "1,2,3";
            FuCai3D fc = new FuCai3D("321321231");
            fc.Add(zu3, PlayType.Zu3);

            Assert.AreEqual(fc.TotalBet, 6);
        }
        [Test]
        public static void TrueAddZu3Dan()
        {
            String dan = "0";
            String tuo = "6,7,8,9";
            FuCai3D fc = new FuCai3D("321321231");
            fc.Add(dan, tuo, PlayType.Zu3 | PlayType.DanTuo);

            Assert.AreEqual(fc.TotalBet, 8);
        }
        [Test]
        public static void TrueAddZu6()
        {
            String zu6 = "1,2,3,4,5,6";
            FuCai3D fc = new FuCai3D("321321231");
            fc.Add(zu6, PlayType.Zu6);

            Assert.AreEqual(fc.TotalBet, 20);
        }
        [Test]
        public static void TrueAddZu6Dan()
        {
            String dan = "1,2";
            String tuo = "6,7,8,9";
            FuCai3D fc = new FuCai3D("321321231");
            fc.Add(dan, tuo, PlayType.Zu6 | PlayType.DanTuo);

            Assert.AreEqual(fc.TotalBet, 4);
        }
        #endregion

        #region false
        [Test]
        public static void AddWithErrorSplit()
        {
            bool result = false;
            String one = "1;2,3,4,5,6";
            String ten = "1,2";
            String hundred = "0,2";
            FuCai3D fc = new FuCai3D("321321231");
            try
            {
                fc.Add(one, ten, hundred);
            }
            catch (FormatException)
            {
                result=true;
            }
            Assert.True(result);
        }

        [Test]
        public static void AddWithErrorNumber()
        {
            bool result = false;
            String one = "11,2,3,4,5,6";
            String ten = "1,2";
            String hundred = "0,2";
            FuCai3D fc = new FuCai3D("321321231");
            try
            {
                fc.Add(one, ten, hundred);
            }
            catch (FormatException)
            {
                result=true;
            }
            Assert.True(result);
        }
        [Test]
        public static void AddWithErrorRepeatNumber()
        {
            bool result = false;
            String one = "1,1,2,3,4,5,6";
            String ten = "1,2";
            String hundred = "0,2";
            FuCai3D fc = new FuCai3D("321321231");
            try
            {
                fc.Add(one, ten, hundred);
            }
            catch (FormatException)
            {
                result =true;
            }
            Assert.True(result);
        }

        [Test]
        public static void AddZu3WithErrorRepeat()
        {
            bool result = false;
            String zu3 = "1,1,2,3,4,5,6";
            FuCai3D fc = new FuCai3D("321321231");
            try
            {
                fc.Add(zu3,PlayType.Zu3);
            }
            catch (FormatException)
            {
                result = true;
            }
            Assert.True(result);
        }
        [Test]
        public static void AddZu6WithErrorRepeat()
        {
            bool result = false;
            String zu6 = "1,1,2,3,4,5,6";
            FuCai3D fc = new FuCai3D("321321231");
            try
            {
                fc.Add(zu6, PlayType.Zu6);
            }
            catch (FormatException)
            {
                result = true;
            }
            Assert.True(result);
        }

        [Test]
        public static void AddZu3WithErrorNumber()
        {
            bool result = false;
            String zu3 = "a,2,3,4,5,6";
            FuCai3D fc = new FuCai3D("321321231");
            try
            {
                fc.Add(zu3, PlayType.Zu3);
            }
            catch (FormatException)
            {
                result = true;
            }
            Assert.True(result);
        }
        [Test]
        public static void AddZu6WithErrorNumber()
        {
            bool result = false;
            String zu6 = "a,2,3,4,5,6";
            FuCai3D fc = new FuCai3D("321321231");
            try
            {
                fc.Add(zu6, PlayType.Zu6);
            }
            catch (FormatException)
            {
                result = true;
            }
            Assert.True(result);
        }

        [Test]
        public static void AddZu3DanWithErrorRepeat()
        {
            bool result = false;
            String dan = "1";
            String tuo = "1,2";
            FuCai3D fc = new FuCai3D("321321231");
            try
            {
                fc.Add(dan, tuo, PlayType.Zu3 | PlayType.DanTuo);
            }
            catch (FormatException)
            {
                result = true;
            }
            Assert.True(result);
        }
        [Test]
        public static void AddZu6DanWithErrorRepeat()
        {
            bool result = false;
            String dan = "1";
            String tuo = "1,2,3";
            FuCai3D fc = new FuCai3D("321321231");
            try
            {
                fc.Add(dan, tuo, PlayType.Zu6 | PlayType.DanTuo);
            }
            catch (FormatException)
            {
                result = true;
            }
            Assert.True(result);
        }

        #endregion 
    }
}
