using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MVCApp.Common
{
    public class Config
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["sqliteConnectionString"];
            }
        }
        public static int PageSize = 10;
    }
}