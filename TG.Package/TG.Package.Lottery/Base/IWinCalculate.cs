
using System;
using System.Collections.Generic;
using TG.Package.Lottery.Win;
namespace TG.Package.Lottery.Base
{
	public interface IWinCalculate {

		/// 
		/// <param name="pLottery"></param>
		List<WinDetail> GetWinDetails(ILottery pLottery);
	}//end IWinCalculate

}//end namespace System