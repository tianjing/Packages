using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TG.Package.Lottery.Base;

namespace TG.Package.Lottery.ValidHelper
{
    public class TwoColor : IValidNumber
    {
        private const Int32 mBit = 2;
        private const Int32 mRedMax = 33;
        private const Int32 mRedMin = 1;
        private const Int32 mBlueMax = 16;
        private const Int32 mBlueMin = 1;
        private const Char SplitStr = ',';

        public void Valid(WelfareLottery pWelfareLottery)
        {
            PlayType playtype = pWelfareLottery.PlayType;
            String red = pWelfareLottery.Lottery[NumericType.Red];
            String blue = pWelfareLottery.Lottery[NumericType.Blue];
            String dan = String.Empty;

            ValidNumbers(red, NumericType.Red);
            ValidNumbers(blue, NumericType.Blue);

            if (playtype == PlayType.DanTuo)
            {
                dan = pWelfareLottery.Lottery[NumericType.Dan];
                ValidNumbers(dan, NumericType.Dan);
                if (NumberHelper.IsRepeat(red.Split(SplitStr), dan.Split(SplitStr)))
                {
                    throw new FormatException("error:dan number is repeat");
                }
            }

        }

        public void ValidNumbers(String pNumbers, NumericType pNumericType)
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
                if (!ValidNumber(res[i], pNumericType))
                {
                    throw new FormatException("error:number is error");
                }
            }
        }
        public bool ValidNumber(string pNumber, NumericType pNumericType)
        {
            switch (pNumericType)
            {
                case NumericType.Dan:
                case NumericType.Red: return ValidHelper.NumberHelper.ValidNumber(pNumber, mRedMax, mRedMin);
                case NumericType.Blue: return ValidHelper.NumberHelper.ValidNumber(pNumber, mBlueMax, mBlueMin);
                default: return false;
            }
        }


    }
}
