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
            if (PageIndex == 1)
            {
                sb.Append(string.Format(@"<li class=""disabled""><a href=""{0}{1}"">&laquo;</a></li>", Url, 1));
            }
            else
            {
                sb.Append(string.Format(@"<li><a href=""{0}{1}"">&laquo;</a></li>", Url, PageIndex - 1));
            }
            for (int i = 1; i <= PageCount; i++)
            {
                if (i == PageIndex)
                {
                    sb.Append(string.Format(@"<li class=""active""><a href=""{0}{1}"" title="""">{1}</a></li>", Url, i));
                }
                else
                {
                    sb.Append(string.Format(@"<li><a href=""{0}{1}"" title="""">{1}</a></li>", Url, i));
                }
            }
            if (PageIndex == PageCount)
            {
                sb.Append(string.Format(@"<li class=""disabled""><a href=""{0}{1}"">&raquo;</a></li>", Url, PageCount));
            }
            else
            {
                sb.Append(string.Format(@"<li><a href=""{0}{1}"">&raquo;</a></li>", Url, PageIndex + 1));
            }

            return sb.ToString();
        }

    }
}