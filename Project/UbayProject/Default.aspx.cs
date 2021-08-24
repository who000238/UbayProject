
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using 處理資料庫相關的類別庫;

namespace UbayProject
{
    public partial class Default : System.Web.UI.Page
    {

        public static DataRow GetMainCategory()
        {
            string connStr = 資料庫相關.取得連線字串();
            string dbCommand =
            $@"SELECT TOP(1)  mainCategoryName
               FROM MainCategoryTable
               ORDER BY mainCategoryID DESC
                ";

            List<SqlParameter> list = new List<SqlParameter>();

            return 資料庫相關.查詢單筆資料(connStr, dbCommand, list);
        }



        protected void Page_Load(object sender, EventArgs e)
        {
         this.Label1.Text = GetMainCategory()["mainCategoryName"].ToString();
        }
    }
}