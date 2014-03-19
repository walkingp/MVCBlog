﻿using MVCApp.Common;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApp.Utility;

namespace MVCApp.Controllers
{
    public class ManageController : BaseController
    {
        public ActionResult Index(string message, int? page)
        {
            int pageIndex = 1;
            if (page.HasValue)
            {
                pageIndex =(int) page;
            }
            List<Photos> list = new List<Photos>();
            list = PhotoService.GetPhotosByPage(pageIndex);
            ViewData["Photos"] = list;

            Pager pager = new Pager { PageCount = PhotoService.GetPhotosCount()/Config.PageSize +1, Url = "/Home/Page/", PageIndex = pageIndex };
            string html = pager.ShowPageHtml();
            ViewData["html"] = html;
            ViewData["Message"] = string.IsNullOrEmpty(message) ? "" : message;
            ViewData["CurrentUserName"] = base.CurrentUserName;

            return View();
        }
        [ActionName("Create"),AcceptVerbs("POST")]
        public ActionResult SavePeople()
        {
            Photos p = new Photos();
            p.Title = Request["pName"];
            string hPhoto = Request["hPhoto"];
            string folder = HttpContext.Server.MapPath("~/upload");
            if (string.IsNullOrEmpty(hPhoto))
            {
                HttpPostedFileBase file = Request.Files["photo"];
                if (file != null)
                {
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    string filePath = Path.Combine(folder, Path.GetFileName(file.FileName));
                    file.SaveAs(filePath);

                    p.Path = "/upload/" + Path.GetFileName(file.FileName);
                }
            }
            else
            {
                p.Path = hPhoto;
            }
            string fullPath = Server.MapPath(p.Path);
            ExifInfo exitInfo = ImageHelper.GetExifInfo(fullPath);
            p.Altitude = exitInfo.Location.Altitude;
            p.Aperture = exitInfo.Aperture;
            p.Camera = exitInfo.Camera;
            p.CaptureTime = exitInfo.CaptureTime;
            p.Exposure = exitInfo.Exposure;
            p.FileName = Path.GetFileName(p.Path);
            p.Focal = exitInfo.Focal;
            //p.Height=
            p.ISO = exitInfo.ISO;
            p.Latitude = exitInfo.Location.Latitude;
            p.LongLatitude = exitInfo.Location.Longtitude;
            p.Manufacturer = exitInfo.Manufacturer;
            //p.Width=
            p.UserId = CurrentUserId;
            p.PostTime = DateTime.Now;

            string smallThumbPath = Server.MapPath(Path.Combine("/upload", "s", Path.GetFileName(fullPath)));
            string mediumThumbPath = Server.MapPath(Path.Combine("/upload", "m", Path.GetFileName(fullPath)));
            ImageHelper.MakeThumNail(fullPath, smallThumbPath, 106, 80, "HW");
            ImageHelper.MakeThumNail(fullPath, mediumThumbPath, 600, 600, "HW");

            Photos result= PhotoService.Add(p);
            if (Request["isAjax"] != null)
            {
                AjaxResult ajaxResult = new AjaxResult();
                ajaxResult.Result = result == null ? EnumResult.Fail : EnumResult.Succ;
                ajaxResult.Obj = result;

                string json= JSONHelper.Serialize<AjaxResult>(ajaxResult);
                Response.Write(json);
                return null;
            }

            ViewData["Photos"] = PhotoService.GetPhotosByPage(1);
            ViewData["Message"] = "";

            return RedirectToAction("Index");
        }
        [ActionName("Delete"),AcceptVerbs("GET")]
        public ActionResult RemovePeople(int id)
        {
            ViewData["Message"] = "";
            Photos p = PhotoService.Get(id);
            if (p != null)
            {
                bool isSucc = PhotoService.Delete(p);
                if (isSucc)
                {
                    ViewData["Message"] = "Deleted sucessfully!";
                }
            }
            else
            {
                ViewData["Message"] = "Parameter errors";
            }
            return RedirectToAction("Index");
        }
    }
}