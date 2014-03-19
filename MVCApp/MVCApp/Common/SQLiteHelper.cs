using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace MVCApp.Common
{
    public class SQLiteHelper
    {
        public static int GetCount(string table)
        {
            int result = 0;
            string sql = "select count(*) from " + table;
            using (SQLiteConnection conn = new SQLiteConnection(Config.ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    object obj = cmd.ExecuteScalar();
                    int.TryParse(obj.ToString(), out result);
                }
            }

            return result;
        }
    }
}