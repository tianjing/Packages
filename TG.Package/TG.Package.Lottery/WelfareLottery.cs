///////////////////////////////////////////////////////////
//  WelfareLottery.cs
//  Implementation of the Class WelfareLottery
//  Generated by Enterprise Architect
//  Created on:      27-����-2012 16:49:55
//  Original author: Administrator
///////////////////////////////////////////////////////////




using System;
using System.Collections.Generic;
using TG.Package.Lottery.Base;
using System.Collections;
namespace TG.Package.Lottery
{
	public class WelfareLottery {

		public WelfareLottery(){
            Lottery = new Dictionary<NumericType, string>();
		}
		/// <summary>
		/// ��Ʊ
		/// </summary>
		public Dictionary<NumericType,String> Lottery{
			//read property
			get;
			//write property
			set;
		}


		/// <summary>
		/// ע��
		/// </summary>
        public Int32 BetCount
        {
            get;
            set;
        }

        public PlayType PlayType
        {
            get;
            set;
        }

	}//end WelfareLottery

}//end namespace System