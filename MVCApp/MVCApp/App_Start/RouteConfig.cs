using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Blog", action = "Index" }
            );
            routes.MapRoute(
                name: "Blogs",
                url: "Blog/Page/{page}",
                defaults: new { controller = "Blog", action = "Index", page = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Blog",
                url: "Blog/{id}",
                defaults: new { controller = "Blog", action = "View" , id = "" },
                constraints: new { id = @"[\d]+" }
            );
            routes.MapRoute(
                name: "About",
                url: "About",
                defaults: new { controller = "Page", action = "About" }
            );
            routes.MapRoute(
                name: "ManageGalleryPage",
                url: "Manage/Gallery/Page/{page}",
                defaults: new { controller = "Manage", action = "Gallery" }
            );
            routes.MapRoute(
                name: "BlogManage",
                url: "Manage/Blog/Page/{page}",
                defaults: new { controller = "Manage", action = "Blog" }
            );
            routes.MapRoute(
                name: "AddBlog",
                url: "Manage/AddBlog/",
                defaults: new { controller = "Manage", action = "AddBlog" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Manage", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}