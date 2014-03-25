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
            p.Title = Request["Title"];
            string folder = HttpContext.Server.MapPath("~/album/photos/");
            HttpPostedFileBase file = Request.Files["photo"];
            if (file.ContentLength > 0)
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string filePath = Path.Combine(folder, Path.GetFileName(file.FileName));
                file.SaveAs(filePath);
                p.Path = "/album/photos/" + Path.GetFileName(file.FileName);
            }
            p.Altitude = Request["Altitude"];
            p.Aperture = Request["Aperture"];
            p.Camera = Request["Camera"];
            p.CaptureTime = Request["CaptureTime"];
            p.Exposure = Request["Exposure"];
            p.Focal = Request["Focal"];
            p.ISO = Request["ISO"];
            p.Latitude = Request["Latitude"];
            p.LongLatitude = Request["LongLatitude"];
            p.Manufacturer = Request["Manufacturer"];
            p.Location = Request["Location"];            

            ViewData["Edit"] = p;
            ViewData["Message"] = "Updated succesully";
            bool isSucc = PhotoService.Update(p);

            return RedirectToAction("Gallery", "Manage", new { Message = ViewData["Message"] });
            //return View("Index");
        }
        public ActionResult Add()
        {
            return View();
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

