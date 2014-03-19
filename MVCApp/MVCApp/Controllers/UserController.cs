using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCApp.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return Redirect("/Home");
            }
            ViewData["message"] = "";
            return View();
        }
        public ActionResult Login(Users model)
        {
            ViewData["message"] = "";
            bool isLogin = UserService.Login(model.Name, model.Pwd)!=null;
            if (!isLogin)
            {
                ViewData["message"] = "Login failed";
                return RedirectToAction("/Index");
            }
            string userData = string.Format("{0}|{1}", model.Id, model.Name);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, model.Name, DateTime.Now, DateTime.Now.AddDays(30), true, userData);
            string ticString = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticString);
            cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
            if (Request.QueryString["ReturnUrl"] != null)
            {
                return Redirect(HttpUtility.UrlDecode(Request.QueryString["ReturnUrl"]));
            }
            return Redirect("/Manage");
        }
        public ActionResult Profile(Users model)
        {
            ViewData["message"] = "";
            string userName = base.CurrentUserName;
            model = UserService.Get(userName);
            return View(model);
        }
        public ActionResult Logout(Users model)
        {
            FormsAuthentication.SignOut();
            if (Request.QueryString["ReturnUrl"] != null && !Request.QueryString["ReturnUrl"].Contains("Login"))
            {
                return Redirect(HttpUtility.UrlDecode(Request.QueryString["ReturnUrl"]));
            }
            return Redirect("/Home");
        }
        public ActionResult Update(Users model)
        {
            bool isSucc = UserService.Update(model);
            ViewData["message"] = isSucc ? "Updated succeded" : "Updated failed";
            return View("Profile", model);
        }
    }
}
