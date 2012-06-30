using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TG.Package.Lottery.Base;
namespace TG.Package.Lottery.Win
{
    public class TwoColor : IWinCalculate
    {

        public TwoColor()
        {

        }

        ~TwoColor()
        {

        }

        public virtual void Dispose()
        {

        }

        /// 
        /// <param name="pLottery"></param>
        public List<WinDetail> GetWinDetails(ILottery pLottery)
        {

            return null;
        }

    }//end TwoColor
}
