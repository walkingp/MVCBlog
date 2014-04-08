using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCApp.Common;
using MVCApp.Models;
using MVCApp.Utility;
using Newtonsoft.Json;

namespace MVCApp.Handler
{
    /// <summary>
    /// map_data 的摘要说明
    /// </summary>
    public class place_data : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            List<Place> list = PlaceService.GetAllPlaces();
            string json = JsonConvert.SerializeObject(list);
            
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