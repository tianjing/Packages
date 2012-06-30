using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TG.Package.Lottery.Base;

namespace TG.Package.Lottery.ValidHelper
{
    public class FuCai3D : IValidNumber
    {
        private const Int32 mBit = 1;
        private const Int32 mMax = 9;
        private const Int32 mMin = 0;
        private const Int32 mZ3MinSize = 2;
        private const Int32 mMinSize = 3;
        private const Char SplitStr = ',';

        public void Valid(WelfareLottery pWelfareLottery)
        {
            switch (pWelfareLottery.PlayType)
            {
                case (PlayType.Zu6 | PlayType.DanTuo):
                case (PlayType.Normal | PlayType.DanTuo): ValidNormalDan(pWelfareLottery); break;

                case (PlayType.Zu3 | PlayType.DanTuo): ValidZu3Dan(pWelfareLottery); break;

                case PlayType.Normal: ValidNormal(pWelfareLottery); break;
                case PlayType.Zu3: ValidZu3(pWelfareLottery); break;
                case PlayType.Zu6: ValidZu6(pWelfareLottery); break;

                default: throw new FormatException("error:playtype is wrong");
            }
        }
        private static void ValidNumbers(String pNumbers)
        {
            String[] res = pNumbers.Split(SplitStr);
            if (NumberHelper.IsRepeat(res))
            {
                throw new FormatException("error:number is repeat");
            }
            for (var i = 0; i < res.Length; i++)
            {
                if (!ValidHelper.NumberHelper.IsBitLength(res[i], mBit))
                {
                    throw new FormatException("error:number bit is error");
                }
                if (!ValidNumber(res[i]))
                {
                    throw new FormatException("error:number is error");
                }
            }
        }
        private static bool ValidNumber(String pNumber)
        {
            return ValidHelper.NumberHelper.ValidNumber(pNumber, mMax, mMin);
        }

        private static void ValidNormal(WelfareLottery pWelfareLottery)
        {
            String one = pWelfareLottery.Lottery[NumericType.Ones];
            String ten = pWelfareLottery.Lottery[NumericType.Tens];
            String hundred = pWelfareLottery.Lottery[NumericType.Hundreds];
            ValidNumbers(one);
            ValidNumbers(ten);
            ValidNumbers(hundred);
        }
        private static void ValidZu3(WelfareLottery pWelfareLottery)
        {
            String zu3 = pWelfareLottery.Lottery[NumericType.Zu3];
            if ((zu3.Length / 2 + 1) < 2)
            { throw new FormatException("error:z3 length large than 1"); }
            ValidNumbers(zu3);
        }
        private static void ValidZu6(WelfareLottery pWelfareLottery)
        {
            String zu6 = pWelfareLottery.Lottery[NumericType.Zu6];
            if ((zu6.Length / 2 + 1) < 3)
            { throw new FormatException("error:z3 length less than 3"); }
            ValidNumbers(zu6);
        }
        private static void ValidZu3Dan(WelfareLottery pWelfareLottery)
        {
            String dan = pWelfareLottery.Lottery[NumericType.Dan];
            String tuo = pWelfareLottery.Lottery[NumericType.Tuo];
            Int32 tuocount = tuo.Length / 2 + 1;
            if (dan.Length != 1)
            { throw new FormatException("error:dan length must be 1"); }
            if (tuocount < 2)
            { throw new FormatException("error:dan length less than 2"); }
            if (NumberHelper.IsRepeat(dan.Split(SplitStr), tuo.Split(SplitStr)))
            {throw new FormatException("error:tuo number is repeat with dan"); }
            ValidNumbers(dan);
            ValidNumbers(tuo);
        }
        private static void ValidNormalDan(WelfareLottery pWelfareLottery)
        {
            String dan = pWelfareLottery.Lottery[NumericType.Dan];
            String tuo = pWelfareLottery.Lottery[NumericType.Tuo];
            Int32 dancount=dan.Length/2+1;
            Int32 tuocount = tuo.Length / 2 + 1;
            if (dancount != 1 && dancount != 2)
            { throw new FormatException("error:dan numbers must be 1-2"); }
            if (tuocount < 2)
            { throw new FormatException("error:dan length less than 2"); }
            if ((dancount + tuocount)<3)
            { throw new FormatException("error:numbers is not enough"); }
            if (NumberHelper.IsRepeat(dan.Split(SplitStr), tuo.Split(SplitStr)))
            { throw new FormatException("error:tuo number is repeat with dan"); }
            ValidNumbers(dan);
            ValidNumbers(tuo);
        }
    }
}
