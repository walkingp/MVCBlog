using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApp.Controllers
{
    public class ErrorController : BaseController
    {
        //
        // GET: /Error/

        public string Index()
        {
            return "Error! <a href=\"/Home/Index\">Back to home</a>";
        }

    }
}
