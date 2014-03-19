using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCApp.Common;
using MVCApp.Models;
using MVCApp.Utility;

namespace MVCApp.Handler
{
    /// <summary>
    /// map_data 的摘要说明
    /// </summary>
    public class map_data : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            List<Photos> list = PhotoService.GetPhotosByPage(0);
            string json =JSONHelper.Serialize(list);

            
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}