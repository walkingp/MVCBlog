using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApp.Common;
using MVCApp.Models;
using MVCApp.Utility;

namespace MVCApp.Controllers
{
    public class GalleryController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {            
            List<Photos> list = new List<Photos>();
            list = PhotoService.GetPhotosByPage(0);
            ViewData["Photos"] = list;

            List<string> listJson = new List<string>();
            foreach (Photos p in list)
            {
                listJson.Add(JSONHelper.Serialize<Photos>(p));
            }
            ViewData["Data"] = listJson;

            return View();
        }

        public ActionResult Map()
        {
            return View();
        }
    }
}
