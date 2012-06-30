using System;
namespace TG.Package.Lottery.Calculate
{
	public class WinNumberHelper {

		public WinNumberHelper(){

		}

		~WinNumberHelper(){

		}

		public virtual void Dispose(){

		}

		/// 
		/// <param name="pWinNo"></param>
		/// <param name="pBuyNo"></param>
		public Int32 NumberEqual(String[] pWinNo, String[] pBuyNo){

			return 0;
		}

		/// 
		/// <param name="pWinNo"></param>
		/// <param name="pBuyNo"></param>
		public bool SeatAndNumberEqual(String[] pWinNo, String[] pBuyNo){

			return false;
		}

	}//end WinNumberHelper

}//end namespace System