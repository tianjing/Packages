using System;
using TG.Package.Lottery.Base;
namespace TG.Package.Lottery.Win
{
	public class WinDetail {

		/// <summary>
		/// 期号
		/// </summary>
		public String LotteryIssue;
		public String LotteryNo;
		/// <summary>
		/// 彩票类型
		/// </summary>
		public LotteryType LotteryType;
		/// <summary>
		/// 玩法
		/// </summary>
		public PlayType PlayType;
		/// <summary>
		/// 中奖等级
		/// </summary>
		public Int32 WinGrade;
		/// <summary>
		/// 中奖金额
		/// </summary>
		public Int64 WinMoney;

		public WinDetail(){

		}

		~WinDetail(){

		}

		public virtual void Dispose(){

		}

	}//end WinDetail

}//end namespace System