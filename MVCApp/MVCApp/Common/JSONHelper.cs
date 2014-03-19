using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MVCApp.Common
{
    public class JSONHelper
    {
        public static string Serialize<T>(T obj)
        {
            JavaScriptSerializer serialize = new JavaScriptSerializer();

            return serialize.Serialize(obj);
        }
        public static T Serialize<T>(string json)
        {
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            return serialize.Deserialize<T>(json);
        }
    }
}