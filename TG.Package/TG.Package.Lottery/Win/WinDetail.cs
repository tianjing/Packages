using System;
using TG.Package.Lottery.Base;
namespace TG.Package.Lottery.Win
{
	public class WinDetail {

		/// <summary>
		/// �ں�
		/// </summary>
		public String LotteryIssue;
		public String LotteryNo;
		/// <summary>
		/// ��Ʊ����
		/// </summary>
		public LotteryType LotteryType;
		/// <summary>
		/// �淨
		/// </summary>
		public PlayType PlayType;
		/// <summary>
		/// �н��ȼ�
		/// </summary>
		public Int32 WinGrade;
		/// <summary>
		/// �н����
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