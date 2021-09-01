using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSource
{
    public class DBHelper
    {
        /// <summary>取得連線字串</summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }

        //public static void 增加一筆資料()
        //{

        //}

        //public static void 刪除一筆資料()
        //{

        //}
       
        //public static void 修改一筆資料()
        //{

        //}
        /// <summary>讀取單筆資料</summary>
        /// <param name="connStr"></param>
        /// <param name="dbCommand"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataRow ReadDataRow(string connStr, string dbCommand, List<SqlParameter> list)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddRange(list.ToArray());

                    conn.Open();
                    var reader = comm.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    if (dt.Rows.Count == 0)
                        return null;

                    return dt.Rows[0];
                }
            }
        }
        /// <summary>讀取資料清單</summary>
        /// <param name="connStr"></param>
        /// <param name="dbCommand"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ReadDataTable(string connStr, string dbCommand, List<SqlParameter> list)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddRange(list.ToArray());


                    conn.Open();
                    var reader = comm.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    return dt;
                }
            }
        }

        public static int ModifyData(string connStr, string dbCommand, List<SqlParameter> paramList)
        {
            // connect db & execute
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddRange(paramList.ToArray());
                    conn.Open();
                    int effectRowsCount = comm.ExecuteNonQuery();
                    return effectRowsCount;
                }
            }
        }

       


       
    }
}
