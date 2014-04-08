using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SQLite;
using System.Configuration;
using MVCApp.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MVCApp.Models
{
    public class Place
    {
        public long Id { get; set; }
        public string Location { get; set; }
        public string Lati { get; set; }
        public string LongLati { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public long Star { get; set; }
    }
    public class PlaceService
    {
        static string ConnectionString = Config.ConnectionString;
        public static int GetPlacesCount()
        {
            return SQLiteHelper.GetCount("Place");
        }
        public static List<Place> GetAllPlaces()
        {
            string sql = "select * from Place order by Id desc";
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }
            return GetPlaces(dt);
        }
        public static List<Place> GetPlaces(DataTable dt)
        {
            List<Place> list = new List<Place>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Place
                {
                    Id = Convert.ToInt64(dr["Id"].ToString()),
                    Location = dr["Location"].ToString(),
                    Lati = dr["Lati"].ToString(),
                    LongLati = dr["LongLati"].ToString(),
                    Date = DateTime.Parse(dr["Date"].ToString()),
                    Notes = dr["Notes"].ToString(),
                    Star= Convert.ToInt64(dr["Star"].ToString())
                });
            }
            return list;
        }
        public static Place Get(int Id)
        {
            string where = "Id=@Id";
            SQLiteParameter[] param ={
                                         new SQLiteParameter("@Id",DbType.Int64,16)
                                    };
            param[0].Value = Id;

            return Get(where, param);
        }
        public static Place Get(string where, SQLiteParameter[] param)
        {
            string sql = "select * from Place where " + where;
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn))
                {
                    da.SelectCommand.Parameters.AddRange(param);

                    da.Fill(dt);
                }
            }
            if (dt.Rows.Count > 0)
            {
                Place p = new Place();
                p.Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                p.Location = dt.Rows[0]["Location"].ToString();
                p.Lati = dt.Rows[0]["Lati"].ToString();
                p.LongLati = dt.Rows[0]["LongLati"].ToString();
                p.Notes = dt.Rows[0]["Notes"].ToString();
                p.Date = Convert.ToDateTime(dt.Rows[0]["Date"]);
                p.Star = Convert.ToInt32(dt.Rows[0]["Star"].ToString());

                return p;
            }
            return null;
        }
        public static Place Add(Place place)
        {
            string sql = "insert into Place(Location,Lati,LongLati,Date,Notes,Star) values(@Location,@Lati,@LongLati,@Date,@Notes,@Star);select last_insert_rowid();";
            SQLiteParameter[] param = 
            {
                new SQLiteParameter("@Location",DbType.String,50),
                new SQLiteParameter("@Lati",DbType.String,10),
                new SQLiteParameter("@LongLati",DbType.String,10),
                new SQLiteParameter("@Date",DbType.Date),
                new SQLiteParameter("@Notes",DbType.String,500),
                new SQLiteParameter("@Star",DbType.Int64)
            };
            param[0].Value = place.Location;
            param[1].Value = place.Lati;
            param[2].Value = place.LongLati;
            param[3].Value = place.Date;
            param[4].Value = place.Notes;
            param[5].Value = place.Star;
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
                        return new Place { Id = result, Location = place.Location, Lati = place.Lati, LongLati = place.LongLati, Date = place.Date, Notes = place.Notes,Star=place.Star };
                    }
                }
            }

            return null;
        }
        public static bool Update(Place place)
        {
            string sql = "update Place set Location=@Location,Lati=@Lati,LongLati=@LongLati,Date=@Date,Notes=@Notes,Star=@Star where Id=@Id";
            SQLiteParameter[] param = 
            {
                new SQLiteParameter("@Location",DbType.String,50),
                new SQLiteParameter("@Lati",DbType.String,10),
                new SQLiteParameter("@LongLati",DbType.String,10),
                new SQLiteParameter("@Date",DbType.Date),
                new SQLiteParameter("@Notes",DbType.String,500),
                new SQLiteParameter("@Id",DbType.Int64,16),
                new SQLiteParameter("@Star",DbType.Int64)
            };
            param[0].Value = place.Location;
            param[1].Value = place.Lati;
            param[2].Value = place.LongLati;
            param[3].Value = place.Date;
            param[4].Value = place.Notes;
            param[5].Value = place.Id;
            param[6].Value = place.Star;
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
        public static bool Delete(Place place)
        {
            string sql = "delete from Place where Id=@Id";
            SQLiteParameter[] param = 
            {
                new SQLiteParameter("@Id",DbType.Int64,16)
            };
            param[0].Value = place.Id;
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