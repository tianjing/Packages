using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TG.Package.Lottery.Base;
namespace TG.Package.Lottery.ValidHelper
{
    interface IValidNumber
    {
        /// <summary>
        /// ValidNumbers
        /// </summary>
        /// <param name="numbers">ex:"01,02"or "1,2"</param>
        /// <returns></returns>
        void Valid(WelfareLottery pWelfareLottery);
    }
}
