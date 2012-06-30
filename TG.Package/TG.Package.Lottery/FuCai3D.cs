using System;
using System.Collections.Generic;
using TG.Package.Lottery.Base;
namespace TG.Package.Lottery
{
	public sealed class FuCai3D : ILottery,IDisposable {

        public FuCai3D(String pLotteryIssue)
        {
            mWelfareLotterys = new List<WelfareLottery>();
            mLotteryIssue = pLotteryIssue;
		}

        #region member
        private Int32 mTotalBet = 0;
        public int TotalBet
        {
            get { return mTotalBet; }
        }
        public Int32 Count{
			get{
				return this.mWelfareLotterys.Count;
			}
		}

        private string mLotteryIssue=String.Empty;
		/// <summary>
		/// ÆÚºÅ
		/// </summary>
		public string LotteryIssue{
			get{
                return mLotteryIssue;
			}
		}

		public WelfareLottery this[Int32 i]{
			get{
				return this.mWelfareLotterys[i];
			}
		}

        private List<WelfareLottery> mWelfareLotterys;
    
        #endregion

        #region function
        public void Add(String pOne, String pTen, String pHundred)
        {
            WelfareLottery lottery = new WelfareLottery();
            lottery.PlayType = PlayType.Normal;
            lottery.Lottery.Add(NumericType.Ones, pOne);
            lottery.Lottery.Add(NumericType.Tens, pTen);
            lottery.Lottery.Add(NumericType.Hundreds, pHundred);

            Valid(lottery);

            lottery.BetCount = CalculateBet(lottery);
            mTotalBet += lottery.BetCount;
            mWelfareLotterys.Add(lottery);
        }
        public void Add(String pDan, String pTuo, PlayType pPlayType)
        {
            WelfareLottery lottery = new WelfareLottery();
            lottery.PlayType = pPlayType;
            lottery.Lottery.Add(NumericType.Dan, pDan);
            lottery.Lottery.Add(NumericType.Tuo, pTuo);


            Valid(lottery);

            lottery.BetCount = CalculateBet(lottery);
            mTotalBet += lottery.BetCount;
            mWelfareLotterys.Add(lottery);
        
        }
        public void Add(String pNumbers, PlayType pPlayType)
        {
            WelfareLottery lottery = new WelfareLottery();
            lottery.PlayType = pPlayType;
            switch(pPlayType)
            {
                case PlayType.Zu3: lottery.Lottery.Add(NumericType.Zu3, pNumbers); break;
                case PlayType.Zu6: lottery.Lottery.Add(NumericType.Zu6, pNumbers); break;
                default: throw new FormatException("error:PlayType must be zu3 or zu6");
            }

            Valid(lottery);

            lottery.BetCount = CalculateBet(lottery);
            mTotalBet += lottery.BetCount;
            mWelfareLotterys.Add(lottery);
        }

        public void Valid(WelfareLottery pWelfareLottery)
        {
            ValidHelper.ValidLotteryNumber.ValidNumbers(pWelfareLottery,LotteryType.FuCai3D);
		}
        #region CalculateBet
        private Int32 CalculateBet(WelfareLottery pWelfareLottery)
        {
            switch (pWelfareLottery.PlayType)
            {
                case PlayType.Normal: return NormalCalculateBet(pWelfareLottery);
                case PlayType.Zu3: return Zu3CalculateBet(pWelfareLottery);
                case PlayType.Zu6: return Zu6CalculateBet(pWelfareLottery); ;
                case PlayType.Normal | PlayType.DanTuo: return NormalDanCalculateBet(pWelfareLottery);
                case PlayType.Zu3 | PlayType.DanTuo: return Zu3DanCalculateBet(pWelfareLottery);
                case PlayType.Zu6 | PlayType.DanTuo: return Zu6DanCalculateBet(pWelfareLottery);
                default: throw new FormatException("error:PlayType is error");
            }
        }
        private static Int32 NormalCalculateBet(WelfareLottery pWelfareLottery)
        {
            Int32 onecount=(pWelfareLottery.Lottery[NumericType.Ones].Length/2)+1;
            Int32 tencount = (pWelfareLottery.Lottery[NumericType.Tens].Length/2)+1;
            Int32 hundredcount = (pWelfareLottery.Lottery[NumericType.Hundreds].Length/2)+1;
            return onecount * tencount * hundredcount;
        }
        private static Int32 Zu3CalculateBet(WelfareLottery pWelfareLottery)
        {
            Int32 zu3count=(pWelfareLottery.Lottery[NumericType.Zu3].Length/2)+1;
           return  Calculate.Discrete.P(zu3count, 2);
        }
        private static Int32 Zu6CalculateBet(WelfareLottery pWelfareLottery)
        {
            Int32 zu6count=(pWelfareLottery.Lottery[NumericType.Zu6].Length/2)+1;
            return Calculate.Discrete.C(zu6count, 3);
        }
        private static Int32 NormalDanCalculateBet(WelfareLottery pWelfareLottery)
        {
            Int32 dancount = (pWelfareLottery.Lottery[NumericType.Dan].Length / 2) + 1;
            Int32 tuocount = (pWelfareLottery.Lottery[NumericType.Tuo].Length / 2) + 1;
            return 3 * dancount * tuocount;
        }
        private static Int32 Zu3DanCalculateBet(WelfareLottery pWelfareLottery)
        {
            Int32 tuocount = (pWelfareLottery.Lottery[NumericType.Tuo].Length / 2) + 1;
            return 2 * tuocount;
        }
        private static Int32 Zu6DanCalculateBet(WelfareLottery pWelfareLottery)
        {
            Int32 dancount = (pWelfareLottery.Lottery[NumericType.Dan].Length / 2) + 1;
            Int32 tuocount = (pWelfareLottery.Lottery[NumericType.Tuo].Length / 2) + 1;
            return Calculate.Discrete.C(tuocount, 3 - dancount);
        }
        #endregion
        public void Remove(int i)
        {
            mTotalBet -= mWelfareLotterys[i].BetCount;
            this.mWelfareLotterys.RemoveAt(i);
        }

        public void Clear()
        {
            mTotalBet = 0;
            this.mWelfareLotterys.Clear();
        }
        #endregion

        public void Dispose()
        {
            mWelfareLotterys = null;
            mLotteryIssue = null;
        }
    }//end FuCai3D

}//end namespace System