using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SQLite;
using System.Configuration;
using MVCApp.Common;
using System.ComponentModel.DataAnnotations;

//http://www.cnblogs.com/insus/p/3367244.html
namespace MVCApp.Models
{
    public class Photos
    {
        public long Id { get; set; }

        [Display(Name="标题")]
        [StringLength(20,ErrorMessage="{0}在{2}位到{1}位之间",MinimumLength=1)]
        [Required(ErrorMessage="请输入{0}")]
        public string Title { get; set; }

        [Display(Name="图片路径")]
        [Required(ErrorMessage="请选择{0}")]
        public string Path { get; set; }

        public string FileName { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Altitude { get; set; }
        public string Latitude { get; set; }
        public string LongLatitude { get; set; }
        public string Aperture { get; set; }
        public string Camera { get; set; }
        public string CaptureTime { get; set; }
        public string Exposure { get; set; }
        public string Focal { get; set; }
        public string ISO { get; set; }
        public string Manufacturer { get; set; }
        public int AlbumId { get; set; }
        public DateTime PostTime { get; set; }
        public long UserId { get; set; }
        public string Location{get;set;}
    }
    public class PhotoService
    {
        static string ConnectionString = Config.ConnectionString;
        public static int GetPhotosCount()
        {
            return SQLiteHelper.GetCount("Photos");
        }
        public static List<Photos> GetPhotosByPage(int pageInt,int pageSize=10)
        {
            long pageIndex = (long)pageInt;
            string sql = "select * from Photos";
            if (pageIndex > 0)
            {
                sql = string.Format("select * from Photos limit {0},{1}", (pageIndex - 1) * pageSize, pageSize);
            }
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return GetPhotos(dt);
        }
        public static List<Photos> GetAllPhotos()
        {
            return GetPhotosByPage(0);
        }
        public static List<Photos> GetPhotos(DataTable dt)
        {
            List<Photos> list = new List<Photos>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Photos {
                    Id = Convert.ToInt64(dr["Id"].ToString()),
                    Title = dr["Title"].ToString(),
                    Path = dr["Path"].ToString(),
                    AlbumId = int.Parse(dr["AlbumId"].ToString()),
                    Altitude = dr["Altitude"].ToString(),
                    Aperture = dr["Aperture"].ToString(),
                    Camera = dr["Camera"].ToString(),
                    CaptureTime = dr["CaptureTime"].ToString(),
                    Exposure = dr["Exposure"].ToString(),
                    FileName = dr["FileName"].ToString(),
                    Focal = dr["Focal"].ToString(),
                    Height = int.Parse(dr["Height"].ToString()),
                    ISO = dr["ISO"].ToString(),
                    Latitude = dr["Latitude"].ToString(),
                    LongLatitude = dr["LongLatitude"].ToString(),
                    Manufacturer = dr["Manufacturer"].ToString(),
                    PostTime = DateTime.Parse(dr["PostTime"].ToString()),
                    UserId = long.Parse(dr["UserId"].ToString()),
                    Width = int.Parse(dr["Width"].ToString()),
                    Location = dr["Location"].ToString()
                });
            }
            return list;
        }
        public static Photos Get(int id)
        {
            string sql = "select * from Photos where Id=@Id";
            SQLiteParameter[] param = 
            {
                new SQLiteParameter("@Id",DbType.Int64,16)
            };
            param[0].Value = id;
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn)){
                    SQLiteDataAdapter da = new SQLiteDataAdapter();
                    da.SelectCommand = cmd;
                    cmd.Parameters.AddRange(param);
                    
                    da.Fill(dt);
                }
            }
            if (dt.Rows.Count > 0)
            {
                Photos p = new Photos();
                p.Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                p.Title = dt.Rows[0]["Title"].ToString();
                p.Path = dt.Rows[0]["Path"].ToString();
                p.AlbumId = int.Parse(dt.Rows[0]["AlbumId"].ToString());
                p.Altitude = dt.Rows[0]["Altitude"].ToString();
                p.Aperture = dt.Rows[0]["Aperture"].ToString();
                p.Camera = dt.Rows[0]["Camera"].ToString();
                p.CaptureTime = dt.Rows[0]["CaptureTime"].ToString();
                p.Exposure = dt.Rows[0]["Exposure"].ToString();
                p.FileName = dt.Rows[0]["FileName"].ToString();
                p.Focal = dt.Rows[0]["Focal"].ToString();
                p.Height = int.Parse(dt.Rows[0]["Height"].ToString());
                p.ISO = dt.Rows[0]["ISO"].ToString();
                p.Latitude = dt.Rows[0]["Latitude"].ToString();
                p.LongLatitude = dt.Rows[0]["LongLatitude"].ToString();
                p.Manufacturer = dt.Rows[0]["Manufacturer"].ToString();
                p.PostTime = DateTime.Parse(dt.Rows[0]["PostTime"].ToString());
                p.UserId = long.Parse(dt.Rows[0]["UserId"].ToString());
                p.Width = int.Parse(dt.Rows[0]["Width"].ToString());
                p.Location = dt.Rows[0]["Location"].ToString();
                return p;
            }
            return null;
        }
        public static Photos Add(Photos photo)
        {
            string sql = @"insert into Photos(
                                Path,
                                FileName,
                                Height,
                                Width,
                                Title,
                                Altitude,
                                Latitude,
                                LongLatitude,
                                Aperture,
                                Camera,
                                CaptureTime,
                                Exposure,
                                Focal,
                                ISO,
                                Manufacturer,
                                AlbumId,
                                PostTime,
                                UserId,
                                Location
                            ) values(
                                @Path,
                                @FileName,
                                @Height,
                                @Width,
                                @Title,
                                @Altitude,
                                @Latitude,
                                @LongLatitude,
                                @Aperture,
                                @Camera,
                                @CaptureTime,
                                @Exposure,
                                @Focal,
                                @ISO,
                                @Manufacturer,
                                @AlbumId,
                                @PostTime,
                                @UserId,
                                @Location
                            );select last_insert_rowid();";
            SQLiteParameter[] param = 
            {
                new SQLiteParameter("@Path",DbType.String,50),
                new SQLiteParameter("@FileName",DbType.String,50),
                new SQLiteParameter("@Height",DbType.Int64,16),
                new SQLiteParameter("@Width",DbType.Int64,16),
                new SQLiteParameter("@Title",DbType.String,50),
                new SQLiteParameter("@Altitude",DbType.String,50),
                new SQLiteParameter("@Latitude",DbType.String,50),
                new SQLiteParameter("@LongLatitude",DbType.String,50),
                new SQLiteParameter("@Aperture",DbType.String,50),
                new SQLiteParameter("@Camera",DbType.String,50),
                new SQLiteParameter("@CaptureTime",DbType.String,50),
                new SQLiteParameter("@Exposure",DbType.String,50),
                new SQLiteParameter("@Focal",DbType.String,50),
                new SQLiteParameter("@ISO",DbType.String,50),
                new SQLiteParameter("@Manufacturer",DbType.String,50),
                new SQLiteParameter("@AlbumId",DbType.Int64,16),
                new SQLiteParameter("@PostTime",DbType.Date,50),
                new SQLiteParameter("@UserId",DbType.Int64,16),
                new SQLiteParameter("@Location",DbType.String,100)
            };
            param[0].Value = photo.Path;
            param[1].Value = photo.FileName;
            param[2].Value = photo.Height;
            param[3].Value = photo.Width;
            param[4].Value = photo.Title;
            param[5].Value = photo.Altitude;
            param[6].Value = photo.Latitude;
            param[7].Value = photo.LongLatitude;
            param[8].Value = photo.Aperture;
            param[9].Value = photo.Camera;
            param[10].Value = photo.CaptureTime;
            param[11].Value = photo.Exposure;
            param[12].Value = photo.Focal;
            param[13].Value = photo.ISO;
            param[14].Value = photo.Manufacturer;
            param[15].Value = photo.AlbumId;
            param[16].Value = photo.PostTime;
            param[17].Value = photo.UserId;
            param[18].Value = photo.Location;
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(param);
                    int result = 0;
                    object obj = cmd.ExecuteScalar();
                    if (obj != null)
                    {
                        int.TryParse(obj.ToString(), out result);
                    }
                    if (result > 0)
                    {
                        photo.Id = result;
                        return photo;
                    }
                }
            }

            return null;
        }
        public static bool Update(Photos photo)
        {
            string sql = "update Photos set Path=@Path,Title=@Title,FileName=@FileName,Height=@Height,Width=@Width,Altitude=@Altitude, Latitude=@Latitude,LongLatitude=@LongLatitude,Aperture=@Aperture,Camera=@Camera,CaptureTime=@CaptureTime,Exposure=@Exposure,Focal=@Focal,ISO=@ISO,Manufacturer=@Manufacturer,AlbumId=@AlbumId,UserId=@UserId,Location=@Location where Id=@Id";
            SQLiteParameter[] param = 
            {
                new SQLiteParameter("@Path",DbType.String,50),
                new SQLiteParameter("@FileName",DbType.String,50),
                new SQLiteParameter("@Height",DbType.Int64,16),
                new SQLiteParameter("@Width",DbType.Int64,16),
                new SQLiteParameter("@Title",DbType.String,50),
                new SQLiteParameter("@Altitude",DbType.String,50),
                new SQLiteParameter("@Latitude",DbType.String,50),
                new SQLiteParameter("@LongLatitude",DbType.String,50),
                new SQLiteParameter("@Aperture",DbType.String,50),
                new SQLiteParameter("@Camera",DbType.String,50),
                new SQLiteParameter("@CaptureTime",DbType.String,50),
                new SQLiteParameter("@Exposure",DbType.String,50),
                new SQLiteParameter("@Focal",DbType.String,50),
                new SQLiteParameter("@ISO",DbType.String,50),
                new SQLiteParameter("@Manufacturer",DbType.String,50),
                new SQLiteParameter("@AlbumId",DbType.Int64,16),
                new SQLiteParameter("@UserId",DbType.Int64,16),
                new SQLiteParameter("@Location",DbType.String,100),
                new SQLiteParameter("@Id",DbType.Int64,16)
            };
            param[0].Value = photo.Path;
            param[1].Value = photo.FileName;
            param[2].Value = photo.Height;
            param[3].Value = photo.Width;
            param[4].Value = photo.Title;
            param[5].Value = photo.Altitude;
            param[6].Value = photo.Latitude;
            param[7].Value = photo.LongLatitude;
            param[8].Value = photo.Aperture;
            param[9].Value = photo.Camera;
            param[10].Value = photo.CaptureTime;
            param[11].Value = photo.Exposure;
            param[12].Value = photo.Focal;
            param[13].Value = photo.ISO;
            param[14].Value = photo.Manufacturer;
            param[15].Value = photo.AlbumId;
            param[16].Value = photo.UserId;
            param[17].Value = photo.Location;
            param[18].Value = photo.Id;
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(param);
                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }
        public static bool Delete(Photos photo)
        {
            string sql = "delete from Photos where Id=@Id";
            SQLiteParameter[] param = 
            {
                new SQLiteParameter("@Id",DbType.Int64,16)
            };
            param[0].Value = photo.Id;
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(param);
                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }
    }
}