using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using MVCApp.Models;

namespace MVCApp.Utility
{
    public class ImageHelper
    {
        public static string GetThumbImagePath(string path)
        {
            return path.Replace(Path.GetFileName(path), "s/" + Path.GetFileName(path));
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="orginalImagePat"> 原图片地址 </param>
        /// <param name="thumNailPath"> 缩略图地址 </param>
        /// <param name="width"> 缩略图宽度 </param>
        /// <param name="height"> 缩略图高度 </param>
        /// <param name="model"> 生成缩略的模式 </param>
        public static void MakeThumNail(string originalImagePath, string thumNailPath, int width, int height, string model)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int thumWidth = width;      //缩略图的宽度
            int thumHeight = height;    //缩略图的高度

            int x = 0;
            int y = 0;

            int originalWidth = originalImage.Width;    //原始图片的宽度
            int originalHeight = originalImage.Height;  //原始图片的高度

            switch (model)
            {
                case "HW":      //指定高宽缩放,可能变形
                    break;
                case "W":       //指定宽度,高度按照比例缩放
                    thumHeight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H":       //指定高度,宽度按照等比例缩放
                    thumWidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut":
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)thumWidth / (double)thumHeight)
                    {
                        originalHeight = originalImage.Height;
                        originalWidth = originalImage.Height * thumWidth / thumHeight;
                        y = 0;
                        x = (originalImage.Width - originalWidth) / 2;
                    }
                    else
                    {
                        originalWidth = originalImage.Width;
                        originalHeight = originalWidth * height / thumWidth;
                        x = 0;
                        y = (originalImage.Height - originalHeight) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(thumWidth, thumHeight);

            //新建一个画板
            System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量查值法
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量，低速度呈现平滑程度
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            graphic.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            graphic.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, thumWidth, thumHeight), new System.Drawing.Rectangle(x, y, originalWidth, originalHeight), System.Drawing.GraphicsUnit.Pixel);

            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(thumNailPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(thumNailPath));
                }
                bitmap.Save(thumNailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                graphic.Dispose();
            }

        }

        /// <summary>
        /// 在图片上添加文字水印
        /// </summary>
        /// <param name="path"> 要添加水印的图片路径 </param>
        /// <param name="syPath"> 生成的水印图片存放的位置 </param>
        public static void AddWaterWord(string text, string path, string syPath)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(path);

            //新建一个画板
            System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(image);
            graphic.DrawImage(image, 0, 0, image.Width, image.Height);

            //设置字体
            System.Drawing.Font f = new System.Drawing.Font("Verdana", 60);

            //设置字体颜色
            System.Drawing.Brush b = new System.Drawing.SolidBrush(System.Drawing.Color.Green);

            graphic.DrawString(text, f, b, 35, 35);
            graphic.Dispose();

            //保存文字水印图片
            image.Save(syPath);
            image.Dispose();

        }

        /// <summary>
        /// 在图片上添加图片水印
        /// </summary>
        /// <param name="path"> 原服务器上的图片路径 </param>
        /// <param name="syPicPath"> 水印图片的路径 </param>
        /// <param name="waterPicPath"> 生成的水印图片存放路径 </param>
        public static void AddWaterPic(string path, string syPicPath, string waterPicPath)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(path);
            System.Drawing.Image waterImage = System.Drawing.Image.FromFile(syPicPath);
            System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(image);
            graphic.DrawImage(waterImage, new System.Drawing.Rectangle(image.Width - waterImage.Width, image.Height - waterImage.Height, waterImage.Width, waterImage.Height), 0, 0, waterImage.Width, waterImage.Height, System.Drawing.GraphicsUnit.Pixel);
            graphic.Dispose();

            image.Save(waterPicPath);
            image.Dispose();
        }

        public static ExifInfo GetExifInfo(string filePath)
        {
            ExifInfo exif = new ExifInfo();
            exif.Location = new Location();
            Image img = Image.FromFile(filePath);

            exif.Location.Latitude = GetLatitude(img).ToString();
            exif.Location.Longtitude = GetLongitude(img).ToString();
            exif.Location.Altitude = GetAltitude(img).ToString();
            PropertyItem[] pt = img.PropertyItems;
            for (int i = 0; i < pt.Length; i++)
            {
                PropertyItem p = pt[i];
                switch (pt[i].Id)
                {
                    // 设备制造商 20. 
                    case 0x010F:
                        exif.Manufacturer = System.Text.ASCIIEncoding.ASCII.GetString(pt[i].Value);
                        break;
                    case 0x0110: // 设备型号 25. 
                        exif.Camera = GetValueOfType2(p.Value);
                        break;
                    case 0x0132: // 拍照时间 30.
                        exif.CaptureTime = GetValueOfType2(p.Value);
                        break;
                    case 0x829A: // .曝光时间 
                        exif.Exposure = GetValueOfType5(p.Value);
                        break;
                    case 0x8827: // ISO 40.  
                        exif.ISO = GetValueOfType3(p.Value);
                        break;
                    case 0x010E: // 图像说明info.description
                        exif.Description = GetValueOfType2(p.Value);
                        break;
                    case 0x920a: //相片的焦距
                        exif.Focal = GetValueOfType5A(p.Value) + " mm";
                        break;
                    case 0x829D: //相片的光圈值
                        exif.Aperture = GetValueOfType5A(p.Value);
                        break;
                    default:
                        break;
                }
            }

            return exif;
        }
        private static string GetValueOfType2(byte[] b)// 对type=2 的value值进行读取
        {
            return System.Text.Encoding.ASCII.GetString(b);
        }
        private static string GetValueOfType3(byte[] b) //对type=3 的value值进行读取
        {
            if (b.Length != 2) return "unknow";
            return Convert.ToUInt16(b[1] << 8 | b[0]).ToString();
        }
        private static string GetValueOfType5(byte[] b) //对type=5 的value值进行读取
        {
            if (b.Length != 8) return "unknow";
            UInt32 fm, fz;
            fm = 0;
            fz = 0;
            fz = Convert.ToUInt32(b[7] << 24 | b[6] << 16 | b[5] << 8 | b[4]);
            fm = Convert.ToUInt32(b[3] << 24 | b[2] << 16 | b[1] << 8 | b[0]);
            return fm.ToString() + "/" + fz.ToString() + " sec";
        }
        private static string GetValueOfType5A(byte[] b)//获取光圈的值
        {
            if (b.Length != 8) return "unknow";
            UInt32 fm, fz;
            fm = 0;
            fz = 0;
            fz = Convert.ToUInt32(b[7] << 24 | b[6] << 16 | b[5] << 8 | b[4]);
            fm = Convert.ToUInt32(b[3] << 24 | b[2] << 16 | b[1] << 8 | b[0]);
            double temp = (double)fm / fz;
            return (temp).ToString();
        }
        public static float? GetLatitude(Image targetImg)
        {
            try
            {
                //Property Item 0x0001 - PropertyTagGpsLatitudeRef
                PropertyItem propItemRef = targetImg.GetPropertyItem(1);
                //Property Item 0x0002 - PropertyTagGpsLatitude
                PropertyItem propItemLat = targetImg.GetPropertyItem(2);
                return ExifGpsToFloat(propItemRef, propItemLat);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
        public static float? GetLongitude(Image targetImg)
        {
            try
            {
                ///Property Item 0x0003 - PropertyTagGpsLongitudeRef
                PropertyItem propItemRef = targetImg.GetPropertyItem(3);
                //Property Item 0x0004 - PropertyTagGpsLongitude
                PropertyItem propItemLong = targetImg.GetPropertyItem(4);
                return ExifGpsToFloat(propItemRef, propItemLong);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
        public static int? GetAltitude(Image targetImg)
        {
            try
            {
                PropertyItem propItemLong = targetImg.GetPropertyItem(6);
                return BitConverter.ToInt32(propItemLong.Value, 0);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
        private static float ExifGpsToFloat(PropertyItem propItemRef, PropertyItem propItem)
        {
            uint degreesNumerator = BitConverter.ToUInt32(propItem.Value, 0);
            uint degreesDenominator = BitConverter.ToUInt32(propItem.Value, 4);
            float degrees = degreesNumerator / (float)degreesDenominator;

            uint minutesNumerator = BitConverter.ToUInt32(propItem.Value, 8);
            uint minutesDenominator = BitConverter.ToUInt32(propItem.Value, 12);
            float minutes = minutesNumerator / (float)minutesDenominator;

            uint secondsNumerator = BitConverter.ToUInt32(propItem.Value, 16);
            uint secondsDenominator = BitConverter.ToUInt32(propItem.Value, 20);
            float seconds = secondsNumerator / (float)secondsDenominator;

            float coorditate = degrees + (minutes / 60f) + (seconds / 3600f);
            string gpsRef = System.Text.Encoding.ASCII.GetString(new byte[1] { propItemRef.Value[0] }); //N, S, E, or W
            if (gpsRef == "S" || gpsRef == "W")
                coorditate = 0 - coorditate;
            return coorditate;
        }
    }
}
