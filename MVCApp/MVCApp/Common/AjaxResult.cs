using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Common
{
    public class AjaxResult
    {
        public EnumResult Result { get; set; }
        public object Obj { get; set; }
    }
    public enum EnumResult
    {
        Fail=-1,
        NoResult=0,
        Succ=1
    }
}