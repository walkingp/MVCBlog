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
    public class Blog
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Banner { get; set; }
        public string Content { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public DateTime PostTime { get; set; }
        public long Hits { get; set; }
        public long Comment { get; set; }
        public bool IsDraft { get; set; }
    }
    public class BlogService
    {
        static string ConnectionString = Config.ConnectionString;
        public static int GetBlogsCount()
        {
            return SQLiteHelper.GetCount("Blog");
        }
        public static List<Blog> GetBlogsByPage(int pageInt)
        {
            long pageIndex = (long)pageInt;
            string sql = "select * from Blog";
            if (pageIndex > 0)
            {
                sql = string.Format("select * from Blog limit {0},{1}", (pageIndex - 1) * Config.PageSize, Config.PageSize);
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

            return GetBlogs(dt);
        }
        public static List<Blog> GetAllPhotos()
        {
            return GetBlogsByPage(0);
        }
        public static List<Blog> GetLastest(int? id=0)
        {
            string sql = string.Format("select * from Blog{0} order by Id desc limit {1}", id > 0 ? " where id<>" + id : "", 5);
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }
            return GetBlogs(dt);
        }
        public static List<Blog> GetBlogs(DataTable dt)
        {
            List<Blog> list = new List<Blog>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Blog
                {
                    Id = Convert.ToInt64(dr["Id"].ToString()),
                    Title = dr["Title"].ToString(),
                    Banner = dr["Banner"].ToString(),
                    Content = dr["Content"].ToString(),
                    UserId = long.Parse(dr["UserId"].ToString()),
                    UserName = dr["UserName"].ToString(),
                    Hits = long.Parse(dr["Hits"].ToString()),
                    Comment = long.Parse(dr["Comment"].ToString()),
                    PostTime = DateTime.Parse(dr["PostTime"].ToString()),
                    IsDraft = bool.Parse(dr["IsDraft"].ToString())
                });
            }
            return list;
        }
        public static Blog Get(int id)
        {
            string sql = "select * from Blog where Id=@Id";
            SQLiteParameter[] param = 
            {
                new SQLiteParameter("@Id",DbType.Int64,16)
            };
            param[0].Value = id;

            return Get(sql, param);
        }
        public static Blog GetPrevious(int id)
        {
            string sql = "select * from Blog where Id<@Id limit 1";
            SQLiteParameter[] param = 
            {
                new SQLiteParameter("@Id",DbType.Int64,16)
            };
            param[0].Value = id;

            return Get(sql, param);
        }
        public static Blog GetNext(int id)
        {
            string sql = "select * from Blog where Id>@Id limit 1";
            SQLiteParameter[] param = 
            {
                new SQLiteParameter("@Id",DbType.Int64,16)
            };
            param[0].Value = id;

            return Get(sql, param);
        }
        private static Blog Get(string sql, SQLiteParameter[] param)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    SQLiteDataAdapter da = new SQLiteDataAdapter();
                    da.SelectCommand = cmd;
                    cmd.Parameters.AddRange(param);

                    da.Fill(dt);
                }
            }
            if (dt.Rows.Count > 0)
            {
                Blog p = new Blog();
                p.Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                p.Title = dt.Rows[0]["Title"].ToString();
                p.Banner = dt.Rows[0]["Banner"].ToString();
                p.UserId = long.Parse(dt.Rows[0]["UserId"].ToString());
                p.Content = dt.Rows[0]["Content"].ToString();
                p.UserName = dt.Rows[0]["UserName"].ToString();
                p.Hits = long.Parse(dt.Rows[0]["Hits"].ToString());
                p.Comment = long.Parse(dt.Rows[0]["Comment"].ToString());
                p.PostTime = DateTime.Parse(dt.Rows[0]["PostTime"].ToString());
                p.IsDraft = bool.Parse(dt.Rows[0]["IsDraft"].ToString());
                return p;
            }
            return null;
        }
        public static Blog Add(Blog blog)
        {
            string sql = @"insert into Blog(
                                Title,
                                Banner,
                                Content,
                                UserId,
                                UserName
                            ) values(
                                @Title,
                                @Banner,
                                @Content,
                                @UserId,
                                @UserName
                            );select last_insert_rowid();";
            SQLiteParameter[] param = 
            {
                new SQLiteParameter("@Title",DbType.String,120),
                new SQLiteParameter("@Banner",DbType.String,200),
                new SQLiteParameter("@Content",DbType.String),
                new SQLiteParameter("@UserId",DbType.Int64,16),
                new SQLiteParameter("@UserName",DbType.String,50)
            };
            param[0].Value = blog.Title;
            param[1].Value = blog.Banner;
            param[2].Value = blog.Content;
            param[3].Value = blog.UserId;
            param[4].Value = blog.UserName;
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
                        blog.Id = result;
                        return blog;
                    }
                }
            }

            return null;
        }
        public static bool Update(Blog blog)
        {
            string sql = "update Blog set Title=@Title,Banner=@Banner,Content=@Content,UserId=@UserId,UserName=@UserName,IsDraft=@IsDraft where Id=@Id";
            SQLiteParameter[] param = 
            {
                new SQLiteParameter("@Title",DbType.String,120),
                new SQLiteParameter("@Banner",DbType.String,200),
                new SQLiteParameter("@Content",DbType.String),
                new SQLiteParameter("@UserId",DbType.Int64,16),
                new SQLiteParameter("@UserName",DbType.String,50),
                new SQLiteParameter("@IsDraft",DbType.Boolean),
                new SQLiteParameter("@Id",DbType.Int64,16)
            };
            param[0].Value = blog.Title;
            param[1].Value = blog.Banner;
            param[2].Value = FormatText(blog.Content);
            param[3].Value = blog.UserId;
            param[4].Value = blog.UserName;
            param[5].Value = blog.IsDraft;
            param[6].Value = blog.Id;
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
        public static bool Delete(Blog blog)
        {
            string sql = "delete from Blog where Id=@Id";
            SQLiteParameter[] param = 
            {
                new SQLiteParameter("@Id",DbType.Int64,16)
            };
            param[0].Value = blog.Id;
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
        private static string FormatText(string p)
        {
            Regex regex = new Regex("(?<line>.*)[^\r\n]", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
            MatchCollection groups = regex.Matches(p);
            p = regex.Replace(p, "<p>$1</p>");
            return p;
        }
    }
}