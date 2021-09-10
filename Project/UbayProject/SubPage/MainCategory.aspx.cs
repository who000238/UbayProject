using DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UbayProject.ORM;

namespace UbayProject.SubPage
{
    public partial class MainCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string MainCateIDtxt = Request.QueryString["mainCategoryID"];
            int MainCateID = Convert.ToInt32(MainCateIDtxt);
            var dr = getMainCategoryNameByMainCategoryID(MainCateID);
            string MainCategoryName = dr["mainCategoryName"].ToString();
            this.lblMainCategoryName.Text = MainCategoryName;
        }

        public static DataRow getMainCategoryNameByMainCategoryID(int MainCateID)
        {
            string connStr = DBHelper.GetConnectionString(); 
            string dbCommand =
                    @" SELECT 
                        [mainCategoryName]
                        FROM [MainCategoryTable]
                        WHERE [mainCategoryID] = @MainCateID
                     ";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@MainCateID", MainCateID));
            try
            {

                return DBHelper.ReadDataRow(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

    }
}