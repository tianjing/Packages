using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace TG.Package.HttpNetWork
{
   public class HttpParams
    {
        #region private member
        public HttpParams()
        {
            mHttpParams = new Dictionary<String, String>();
        }
        private Dictionary<String, String> mHttpParams;
        #endregion
        public Int32 Count { get { return mHttpParams.Count; } }
        /// <summary>
        /// if key is Exists that update value,else add key and value
        /// </summary>
        public void SetParam(String key, String value)
        {
            if (mHttpParams.ContainsKey(key))
            {
                mHttpParams[key] = HttpUtility.UrlEncode(value);
            }
            else
            {
                mHttpParams.Add(key,HttpUtility.UrlEncode( value));
            }
        }
        /// <summary>
        /// Get value by key
        /// </summary>
        /// <returns></returns>
        public String GetParam(String key)
        {
            if (mHttpParams.ContainsKey(key))
            {
                return mHttpParams[key];
            }
            return String.Empty;
        }

        #region Params TO String
        public override String ToString()
        {
            return GetString();
        }
        /// <summary>
        /// Params to String
        /// </summary>
        /// <returns></returns>
        private String GetString()
        {
            StringBuilder result = new StringBuilder();
            Int32 i = 0;
            foreach (String key in mHttpParams.Keys)
            {
                if (i > 0)
                {
                    result.Append("&");
                }
                result.AppendFormat("{0}={1}",key,mHttpParams[key]);
                i++;
            }
            return result.ToString();

        }
        #endregion


    }
}
