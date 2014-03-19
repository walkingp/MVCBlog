using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class ExifInfo
    {
        /// <summary>
        /// 设备制造商
        /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string Camera { get; set; }
        /// <summary>
        /// 拍摄时间
        /// </summary>
        public string CaptureTime { get; set; }
        /// <summary>
        /// 曝光时间
        /// </summary>
        public string Exposure { get; set; }
        /// <summary>
        /// ISO
        /// </summary>
        public string ISO { get; set; }
        /// <summary>
        /// 图像说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 焦距
        /// </summary>
        public string Focal { get; set; }
        /// <summary>
        /// 光圈
        /// </summary>
        public string Aperture { get; set; }
        /// <summary>
        /// GPS
        /// </summary>
        public Location Location { get; set; }
    }
}