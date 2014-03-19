using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApp.Controllers
{
    public class PhotosController : BaseController
    {
        //
        // GET: /Edit/
        [AcceptVerbs("POST"), ActionName("Save")]
        public ActionResult Save()
        {
            if (ModelState.IsValid)
            {

            }
            int id = 0;
            int.TryParse(Request["Id"].ToString(), out id);
            Photos p = PhotoService.Get(id);
            if (p == null)
            {
                return View("Error");
            }
            p.Title = Request["pName"];
            HttpPostedFileBase file = Request.Files["photo"];
            if (file != null)
            {
                string folder = HttpContext.Server.MapPath("~/upload");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string filePath = Path.Combine(folder, Path.GetFileName(file.FileName));
                file.SaveAs(filePath);
                p.Path = "/upload/" + Path.GetFileName(file.FileName);
            }


            ViewData["Edit"] = p;
            ViewData["Message"] = "Updated succesully";
            bool isSucc = PhotoService.Update(p);

            return RedirectToAction("Index", "Manage", new { Message = ViewData["Message"] });
            //return View("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Photos p = PhotoService.Get(id.Value);
                if (p == null)
                {
                    return View("Error");
                }
                ViewData["Edit"] = p;

                return View(p);
            }
            return null;
        }

    }
}

