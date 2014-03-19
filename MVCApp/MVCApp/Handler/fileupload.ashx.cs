using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MVCApp.Handler
{
    /// <summary>
    /// Summary description for fileupload1
    /// </summary>
    public class fileupload1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            if (context.Request.Files != null)
            {
                foreach (string file in context.Request.Files.AllKeys)
                {
                    HttpPostedFile f = context.Request.Files[file];
                    if (f != null && f.FileName != "")
                    {
                        string folder = context.Server.MapPath("~/upload");
                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }
                        string filePath = Path.Combine(folder, Path.GetFileName(f.FileName));
                        f.SaveAs(filePath);
                        sb.Append("/upload/" + Path.GetFileName(f.FileName));
                        sb.Append(";");
                    }
                }
            }
            
            context.Response.Write(sb.ToString().Trim(';'));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}