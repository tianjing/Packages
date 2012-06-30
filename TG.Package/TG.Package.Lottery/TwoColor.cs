using System;
using System.Collections.Generic;
using TG.Package.Lottery.Base;
using System.Collections;
namespace TG.Package.Lottery
{
    public class TwoColor : ILottery,IDisposable
    {

        public TwoColor(String pLotteryIssue)
        {
            mLotteryIssue = pLotteryIssue;
            mWelfareLotterys = new List<WelfareLottery>();
        }
        #region member
        private List<WelfareLottery> mWelfareLotterys;
        public WelfareLottery this[Int32 i]
        {
            get
            {
                return this.mWelfareLotterys[i];
            }
        }
        private Int32 mTotalBet = 0;
        public Int32 TotalBet
        {
            get
            {
                return mTotalBet;
            }
        }
        public Int32 Count
        {
            get
            {
                return this.mWelfareLotterys.Count;
            }
        }
        private String mLotteryIssue = String.Empty;
        /// <summary>
        /// ÆÚºÅ
        /// </summary>
        public string LotteryIssue
        {
            get
            {
                return mLotteryIssue;
            }
        }
        #endregion

        #region function
        #region ADD
        /// <summary>
        /// Add Lottery Numbers
        /// </summary>
        /// <param name="pRed">red number ex:"01,02"</param>
        /// <param name="pBlue">blue number ex:"01,02"</param>
        /// <param name="pDan">dan number ex:"01,02"</param>
        public void Add(String pRed, String pBlue, String pDan)
        {
            WelfareLottery lottery = new WelfareLottery();

            lottery.Lottery.Add(NumericType.Red, pRed);
            lottery.Lottery.Add(NumericType.Blue, pBlue);
            lottery.Lottery.Add(NumericType.Dan, pDan);
            lottery.PlayType = PlayType.DanTuo;

            Valid(lottery);

            lottery.BetCount = CalculateBet(pRed, pBlue, pDan);
            mTotalBet += lottery.BetCount;
            mWelfareLotterys.Add(lottery);
        }
        /// <summary>
        /// Add Lottery Numbers
        /// </summary>
        /// <param name="pRed">red number ex:"01,02"</param>
        /// <param name="pBlue">blue number ex:"01,02"</param>
        public void Add(String pRed, String pBlue)
        {
            WelfareLottery lottery = new WelfareLottery();

                lottery.Lottery.Add(NumericType.Red, pRed);
                lottery.Lottery.Add(NumericType.Blue, pBlue);
                lottery.PlayType = PlayType.Normal;

                Valid(lottery);

                lottery.BetCount = CalculateBet(pRed, pBlue, String.Empty);
                mTotalBet += lottery.BetCount;
                mWelfareLotterys.Add(lottery);
        }
        /// <summary>
        /// Add Number Collections
        /// </summary>
        public void AddRange(IList<String> pRed, IList<String> pBlue)
        {
            if (pRed.Count == pBlue.Count)
            {
                for (var i = 0; i < pRed.Count; i++)
                {
                    if (!String.IsNullOrEmpty(pRed[i]) && !String.IsNullOrEmpty(pBlue[i]))
                    {
                        Add(pRed[i], pBlue[i]);
                    }
                    else
                    {
                        throw new FormatException("red or blue  is Empty");
                    }
                }
            }
            else { throw new FormatException("red and blue Length is not equal"); }


        }
        #endregion
        /// <summary>
        /// Calculate one Lottery`s bet
        /// </summary>
        /// <param name="pRed">Red number</param>
        /// <param name="pBlue">blue number</param>
        /// <param name="pDan">dan number</param>
        /// <returns></returns>
        private static Int32 CalculateBet(String pRed, String pBlue, String pDan)
        {
            Int32 redcount = (pRed.Length / 3) + 1;
            Int32 bluecount = (pBlue.Length / 3) + 1;
            Int32 dancount = 0;
            if (!String.IsNullOrEmpty(pDan))
            {
                dancount = (pDan.Length / 3) + 1;
            }
            return Calculate.Discrete.C(redcount, 6 - dancount) * bluecount;
        }


        /// <summary>
        /// Valid number 
        /// </summary>
        public void Valid(WelfareLottery pWelfareLottery)
        {
            ValidHelper.ValidLotteryNumber.ValidNumbers(pWelfareLottery, LotteryType.TwoColor);
        }
        /// <summary>
        /// Remove WelfareLotterys with i
        /// </summary>
        /// <param name="i">index</param>
        public void Remove(int i)
        {
            mTotalBet -= mWelfareLotterys[i].BetCount;
            this.mWelfareLotterys.RemoveAt(i);
        }
        /// <summary>
        /// Clear All WelfareLotterys
        /// </summary>
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
    }//end TwoColor

}//end namespace System