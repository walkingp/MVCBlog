using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using MVCApp.Models;

namespace MVCApp.Utility
{
    public class GoogleMapHelper
    {
        public static Location GetLocation(string addr)
        {
            Location loc = new Location();
            string url = string.Concat("http://ditu.google.cn/maps/api/geocode/json?sensor=false&hl=zh_CN&address=", addr);
            string json = NetHelper.GetHtmlFromUrl(url);
            try
            {
                JsonData data = JsonMapper.ToObject(json);
                loc.Latitude = data[0][0][2][1][0].ToString();
                loc.Longtitude = data[0][0][2][1][1].ToString();//(string)data[0][0]["geometry"]["location"]["lng"];
                loc.Address = addr;
            }
            catch
            {

            }
            return loc;
        }
        public static Location GetAddress(string lat, string longLat)
        {
            Location loc = new Location();
            string url = string.Concat("http://ditu.google.cn/maps/api/geocode/json?sensor=false&hl=zh_CN&latlng=", lat, ",", longLat);
            string json = NetHelper.GetHtmlFromUrl(url);
            try
            {
                JsonData data = JsonMapper.ToObject(json);
                loc.Latitude = lat;
                loc.Longtitude = longLat;
                loc.Address = data[0][0][1].ToString();
            }
            catch
            {

            }
            return loc;
        }
    }
}
