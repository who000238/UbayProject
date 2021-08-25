using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using 處理資料庫相關的類別庫;

namespace UbayProject
{
    public partial class TryGri : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                int categoryID = 1;
                var dt = 取得貼文(categoryID);
                if (dt.Rows.Count > 0)  // check is empty data
                {
                    this.GridView1.DataSource = dt;
                    this.GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('{ex}')</script>");
            }

        }


        public static DataTable 取得貼文(int categoryID)
        {
            string connStr = 資料庫相關.取得連線字串();
            string dbCommand =
                $@" SELECT 
                        [postTitle],
                        [createDate],
                        [postID]
                    FROM [PostTable]
                    WHERE [subCategoryID] = @subCategoryID
                ";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@subCategoryID", categoryID));

            try
            {
                return 資料庫相關.查詢資料清單(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;

        }
    }
}