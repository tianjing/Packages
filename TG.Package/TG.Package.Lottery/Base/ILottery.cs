using System;
using System.Collections.Generic;
namespace TG.Package.Lottery.Base
{
	public interface ILottery {


		Int32 Count{
			get;
		}
        Int32 TotalBet
        {
            get;
        }
		/// <summary>
		/// ÆÚºÅ
		/// </summary>
		string LotteryIssue{
			get;
		}

		WelfareLottery this[Int32 i]{
			get;
		}

        void Valid(WelfareLottery pWelfareLottery);

        void Remove(Int32 i);
        void Clear();
	}//end ILottery

}//end namespace System