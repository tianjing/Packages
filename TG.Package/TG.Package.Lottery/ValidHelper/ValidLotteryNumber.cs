using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TG.Package.Lottery.Base;

namespace TG.Package.Lottery.ValidHelper
{
    abstract class ValidLotteryNumber
    {
        public static void ValidNumbers(WelfareLottery pWelfareLottery, LotteryType pLotteryType)
        {
            IValidNumber valid = null;
            switch (pLotteryType)
            {
                case LotteryType.TwoColor: valid = new TwoColor(); break;
                case LotteryType.FuCai3D: valid = new FuCai3D(); break;
                default: break;
            }
            if (null != valid)
            {
                valid.Valid(pWelfareLottery);
            }
        }
    }
}
