using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApp.Utility;
using MVCApp.Common;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class PlaceController : BaseController
    {
        //
        // GET: /Place/

        public ActionResult Index()
        {
            return View();
        }

    }
}
