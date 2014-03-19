using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApp.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        public bool IsLogin
        {
            get
            {
                return this.HttpContext.User.Identity.IsAuthenticated;
            }
        }
        public int CurrentUserId
        {
            get
            {
                if (!IsLogin)
                {
                    return 0;
                }
                HttpCookie cookie = Request.Cookies[".ASPXAUTH"];

                string[] dataArr= ((System.Web.Security.FormsIdentity)this.HttpContext.User.Identity).Ticket.UserData.Split('|');
                if (dataArr != null && dataArr.Length==2)
                {
                    return int.Parse(dataArr[0]);
                }
                return 0;
            }
        }
        public string CurrentUserName
        {
            get
            {
                if (!IsLogin)
                {
                    return "";
                }
                HttpCookie cookie=Request.Cookies[".ASPXAUTH"];

                return ((System.Web.Security.FormsIdentity)this.HttpContext.User.Identity).Ticket.UserData.Split('|')[1];
            }
        }
    }
}
