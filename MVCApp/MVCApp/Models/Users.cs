using MVCApp.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCApp.Models
{
    public class Users
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public EnumRole Role { get; set; }
    }
    public class UserService
    {
        static string ConnectionString = Config.ConnectionString;
        public static Users Login(string name, string pwd)
        {
            string where = "Name=@Name and Pwd=@Pwd";
            SQLiteParameter[] param ={
                                         new SQLiteParameter("@Name",DbType.String,50),
                                         new SQLiteParameter("@Pwd",DbType.String,50)
                                    };
            param[0].Value = name;
            param[1].Value = pwd;

            return Get(where, param);
        }
        public static Users Get(int userId)
        {
            string where = "Id=@Id";
            SQLiteParameter[] param ={
                                         new SQLiteParameter("@Id",DbType.Int64,16)
                                    };
            param[0].Value = userId;

            return Get(where, param);
        }
        public static Users Get(string name)
        {
            string where = "Name=@Name";
            SQLiteParameter[] param ={
                                         new SQLiteParameter("@Name",DbType.String,50)
                                    };
            param[0].Value = name;

            return Get(where, param);
        }
        public static Users Get(string where, SQLiteParameter[] param)
        {
            string sql = "select * from Users where " + where;
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
                Users p = new Users();
                p.Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                p.Name = dt.Rows[0]["Name"].ToString();
                p.Pwd = dt.Rows[0]["pwd"].ToString();
                p.Role=(EnumRole)(Convert.ToInt64(dt.Rows[0]["Role"]));

                return p;
            }
            return null;
        }
        public static Users Add(Users user)
        {
            string sql = "insert into Users(name,pwd,role) values(@name,@pwd,@role);select last_insert_rowid();";
            SQLiteParameter[] param = 
            {
                new SQLiteParameter("@name",DbType.String,50),
                new SQLiteParameter("@pwd",DbType.String,50),
                new SQLiteParameter("@role",DbType.Int64,16)
            };
            param[0].Value = user.Name;
            param[1].Value = user.Pwd;
            param[2].Value =(int) user.Role;
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
                        return new Users { Id = result, Name = user.Name,Pwd=user.Pwd,Role=user.Role };
                    }
                }
            }

            return null;
        }
        public static bool Update(Users user)
        {
            string sql = "update Users set name=@name,pwd=@pwd,role=@role where Id=@Id";
            SQLiteParameter[] param = 
            {
                new SQLiteParameter("@name",DbType.String,50),
                new SQLiteParameter("@pwd",DbType.String,50),
                new SQLiteParameter("@role",DbType.Int64,16),
                new SQLiteParameter("@Id",DbType.Int64,16)
            };
            param[0].Value = user.Name;
            param[1].Value = user.Pwd;
            param[2].Value =(int) user.Role;
            param[3].Value = user.Id;
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
        public static bool Delete(Users user)
        {
            string sql = "delete from users where Id=@Id";
            SQLiteParameter[] param = 
            {
                new SQLiteParameter("@Id",DbType.Int64,16)
            };
            param[0].Value = user.Id;
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
        public static bool IsLogin()
        {
            return HttpContext.Current.Request.IsAuthenticated;
        }
        public int CurrentUserId
        {
            get
            {
                if (!IsLogin())
                {
                    return 0;
                }
                HttpCookie cookie = HttpContext.Current.Request.Cookies[".ASPXAUTH"];

                //string[] dataArr = ((System.Web.Security.FormsIdentity)HttpContext.User.Identity).Ticket.UserData.Split('|');
                //if (dataArr != null && dataArr.Length == 2)
                //{
                //    return int.Parse(dataArr[0]);
                //}
                return 0;
            }
        }
    }
    public enum EnumRole
    {
        User=0,
        Admin=1,
        PowerUser=2
    }
}