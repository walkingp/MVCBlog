using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MVCApp.Common
{
    public class Pager
    {
        private int pageSize;
        public int PageSize
        {
            get
            {
                return Config.PageSize;
            }
            set
            {
                pageSize = value;
            }
        }
        private int pageIndex;
        public int PageIndex
        {
            get
            {
                return pageIndex;
            }
            set
            {
                pageIndex = value;
            }
        }
        public int TotalCount
        {
            get;
            set;
        }
        public string Url
        {
            get;
            set;
        }
        private int pageCount;
        public int PageCount
        {
            get
            {
                return pageCount;
            }
            set
            {
                pageCount = value;
            }
        }
        public string ShowPageHtml()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= PageCount; i++)
            {
                if (i == PageIndex)
                {
                    sb.Append(string.Format(@"<a href=""{0}{1}"" class=""current"" title="""">{1}</a>", Url, i));
                }
                else
                {
                    sb.Append(string.Format(@"<a href=""{0}{1}"" title="""">{1}</a>", Url, i));
                }
            }

            return sb.ToString();
        }

    }
}