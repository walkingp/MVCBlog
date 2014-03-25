using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MVCApp.Views.Shared
{
    public partial class Manage : System.Web.UI.MasterPage
    {
        public int NavBarIndex { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected string GetSelected(int index)
        {
            return NavBarIndex.Equals(index) ? @" class=""active""" : "";
        }
    }
}