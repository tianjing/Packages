
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace TG.Package.HttpNetWork
{
    public class HttpRequest:IDisposable
    {

        public HttpRequest()
        {
            this.CookieContainer = new CookieContainer();
            Heads = new Dictionary<String, String>();
        }
        #region member
        private String mUserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
        /// <summary>
        /// default value Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)
        /// </summary>
        public String UserAgent
        {
            get { return mUserAgent; }
            set { mUserAgent = value; }
        }
        private String mAccept = "*/*";
        /// <summary>
        /// default value */*
        /// </summary>
        public String Accept
        {
            get { return mAccept; }
            set { mAccept = value; }
        }

        private String mContentType = "application/x-www-form-urlencoded";
        /// <summary>
        /// default value application/x-www-form-urlencoded
        /// </summary>
        public String ContentType
        {
            get { return mContentType; }
            set { mContentType = value; }
        }
        private Boolean mKeepAlive = true;
        /// <summary>
        /// default value true
        /// </summary>
        public Boolean KeepAlive
        {
            get { return mKeepAlive; }
            set { mKeepAlive = value; }
        }
        /// <summary>
        /// cookiecollent
        /// </summary>
        public CookieContainer CookieContainer { get; set; }
        public Dictionary<String, String> Heads;
        private void AddHeads(HttpWebRequest tx)
        {
            foreach (String key in Heads.Keys)
            {
                tx.Headers.Add(key, Heads[key]);
            }

        }


        public String Referer { get; set; }
        #endregion

        #region public medthod
        public HttpResponse Get(String url)
        {
            return DoGetForHttpResponse(url);
        }
        public HttpResponse Post(String uri, HttpParams sHttpParams)
        {
            return DoPostForHttpResponse(uri, sHttpParams.ToString());
        }
        public HttpResponse Post(String uri, String pPostData)
        {
            return DoPostForHttpResponse(uri, pPostData);
        }
        #endregion


        #region private medthod
        private HttpWebRequest CreateRequest(String url)
        {

            HttpWebRequest tx = (HttpWebRequest)WebRequest.Create(url);
            tx.UserAgent = mUserAgent;
            tx.Accept = mAccept;
            tx.ContentType = mContentType;
            tx.KeepAlive = mKeepAlive;
            tx.CookieContainer = CookieContainer;
            AddHeads(tx);
            if (!String.Equals(String.Empty, Referer))
            {
                tx.Referer = Referer;
            }
            return tx;
        }


        /// <summary>
        /// 向服务器post请求
        /// </summary>
        /// <param name="uri">网址</param>
        /// <param name="postdata">post数据</param>
        /// <returns></returns>
        private HttpResponse DoPostForHttpResponse(String url, String sHttpParams)
        {
            String postdata = sHttpParams;
            Stream poststream;
            byte[] postd;

            postd = Encoding.UTF8.GetBytes(postdata);
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
            HttpWebRequest tx = CreateRequest(url);
            try
            {
                tx.Method = "post";
                tx.ServicePoint.Expect100Continue = false;
                tx.Proxy = null;
                tx.ContentLength = postdata.Length;
                poststream = tx.GetRequestStream();
                poststream.Write(postd, 0, postdata.Length);
                poststream.Close();

                Referer = tx.GetResponse().ResponseUri.AbsolutePath;

                return new HttpResponse(tx.GetResponse());

            }
            catch (Exception) { return null; }
        }
        /// <summary>
        /// 向服务器提出GET请求
        /// </summary>
        /// <param name="uri">网址</param>
        /// <returns></returns>
        private HttpResponse DoGetForHttpResponse(String url)
        {
            byte[] getalldata = new byte[999999];
            byte[] receivedate = new byte[1024];
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
            HttpWebRequest tx = CreateRequest(url);
            try
            {
                Referer = tx.GetResponse().ResponseUri.AbsolutePath;
                return new HttpResponse(tx.GetResponse());
            }
            catch (Exception)
            {
                return null;
            }
        }
        //对https支持
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {   // 总是接受  
            return true;
        }
        #endregion

        public void Dispose()
        {
            CookieContainer = null;
            Heads = null;
            mAccept = null;
            mContentType = null;
            mUserAgent= null;
        }
    }

}
