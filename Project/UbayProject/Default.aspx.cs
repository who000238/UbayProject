
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace UbayProject
{
    public partial class Default : System.Web.UI.Page
    {
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
        public static DataRow GetMainCategory()
        {
            string connStr = GetConnectionString();
            string dbCommand =
            $@"SELECT TOP(1)  mainCategoryName
               FROM MainCategoryTable
               ORDER BY mainCategoryID DESC
                ";

            List<SqlParameter> list = new List<SqlParameter>();

            return ReadDataRow(connStr, dbCommand, list);
        }

        public static string GetConnectionString()
        {
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
         this.Label1.Text = GetMainCategory()["mainCategoryName"].ToString();
        }
    }
}