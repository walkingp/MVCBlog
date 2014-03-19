using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Web;

namespace MVCApp.Utility
{
    /// <summary>
    /// 网络操作类
    /// </summary>
    public class NetHelper
    {
        /// <summary>
        /// 获得html内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHtmlFromUrl(string url)
        {
            string strResult = "";
            try {
                Uri uri = new Uri(url);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "GET";
                request.AllowAutoRedirect = true;
                request.UserAgent = "Googlebot/2.1 (+http://www.google.com/bot.html)";
                request.Referer = string.Concat("http://", uri.Host);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.UTF8;
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                strResult = streamReader.ReadToEnd();

                return strResult;
            } catch (Exception ex) {
                return ex.Message;
            }
        }
    }
}
