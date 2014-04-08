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
    public class BlogController : BaseController
    {
        //
        // GET: /Blog/
        public ActionResult Index( int? pageIndex)
        {
            int page = 0;
            if (pageIndex.HasValue)
            {
                page = pageIndex.Value;
            }
            List<Blog> list = BlogService.GetBlogsByPage(page);
            ViewData["Blogs"] = list;
            List<Blog> listNewest = BlogService.GetLastest();
            ViewData["LastestBlogs"] = listNewest;

            Pager pager = new Pager { PageCount = BlogService.GetBlogsCount() / Config.PageSize + 1, Url = "/Blog/Page/", PageIndex = page };
            string html = pager.ShowPageHtml();
            ViewData["page"] = html;
            return View();
        }

        public ActionResult View(int id)
        {
            Blog blog = BlogService.Get(id);
            List<Blog> listNewest = BlogService.GetLastest(id);
            ViewData["LastestBlogs"] = listNewest;
            ViewData["blog"] = blog;
            ViewData["title"] = blog.Title;

            blog = BlogService.GetPrevious(id);
            ViewData["PreviousBlog"] = blog;
            blog = BlogService.GetNext(id);
            ViewData["NextBlog"] = blog;

            return View();
        }
        [AcceptVerbs("POST"), ActionName("Save")]
        public ActionResult Save()
        {
            Blog blog = new Blog();
            blog.Title = Request["Title"];
            blog.Content = Request["Content"];
            blog.UserId = CurrentUserId;
            blog.UserName = CurrentUserName;
            BlogService.Add(blog);
            return RedirectToAction("Blog", "Manage");
        }
        [AcceptVerbs("POST"), ActionName("Update")]
        [ValidateInput(false)]
        public ActionResult Update()
        {
            int id = 0;
            int.TryParse(Request["Id"].ToString(), out id);

            Blog blog = BlogService.Get(id);
            if (blog == null)
            {
                return View();
            }
            blog.Title = Request["Title"];
            blog.Content = Request["Content"];
            blog.UserId = CurrentUserId;
            blog.UserName = CurrentUserName;
            BlogService.Update(blog);
            return RedirectToAction("Blog", "Manage");
        }
        public ActionResult Delete(int Id)
        {
            Blog blog = BlogService.Get(Id);
            if (blog == null)
            {
                return View();
            }
            BlogService.Delete(blog);
            return RedirectToAction("Blog", "Manage");
        }
    }
}
