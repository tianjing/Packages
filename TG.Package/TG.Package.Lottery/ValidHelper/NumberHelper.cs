using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TG.Package.Lottery.Base;
namespace TG.Package.Lottery.ValidHelper
{
    sealed class NumberHelper
    {
      public  static bool IsRepeat(String[] pNumbers)
        {
            return pNumbers.Length != pNumbers.Distinct().ToArray().Length;
        }
       public static bool IsRepeat(String[] pSources, String[] pContrasts)
        {
            bool result = false;
            for (var i = 0; i < pContrasts.Length; i++)
            {
                if (pSources.Contains(pContrasts[i]))
                {
                    result = true;
                    break;
                }
            }
           return result;
        }
        /// <summary>
        /// Number Length equal bit
        /// </summary>
        /// <param name="pNumber">ex:"01"or"1"</param>
        /// <param name="pBit">lenght</param>
        /// <returns></returns>
        public static bool IsBitLength(String pNumber, Int32 pBit)
        {
            return pNumber.Length == pBit;
        }
        /// <summary>
        /// String is Integer
        /// </summary>
        /// <param name="str">need valid str</param>
        /// <returns></returns>
        public static bool ValidNumber(String str, Int32 max, Int32 min)
        {
            bool result = true;
            Int32 temp = 0;
            if (!Int32.TryParse(str, out temp)
                || !ValidIntegerMax(temp, max)
                || !ValidIntegerMin(temp, min))
            {
                result = false;
            }

            return result;
        }
        static bool ValidIntegerMax(Int32 value, Int32 max)
        {
            return value <= max;
        }
        static bool ValidIntegerMin(Int32 value, Int32 min)
        {
            return value >= min;
        }

    }
}
