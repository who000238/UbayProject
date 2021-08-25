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
    public partial class SeePost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //讀取網址列中的貼文ID
            string queryString = this.Request.QueryString["postID"];
            var dr = getPostByPostID(queryString);
            if(dr == null)
            {
                Response.Write("<script>alert('該貼文不存在')</script>");
                Response.Redirect("MainPage.aspx");
            }
            this.lblTitle.Text = dr["postTitle"].ToString();
            this.lblInner.Text = dr["postText"].ToString();
        }
        public DataRow getPostByPostID(string queryString)
        {
            string connStr = 資料庫相關.取得連線字串();
            string dbCommand =
                $@"SELECT PostTable.postID
                        ,postTitle
                        ,countOfLikes
                        ,countOfUnlikes
                        ,countOfViewers
                        ,createDate
                        ,subCategoryID
                        ,userID
                        ,postText
                        FROM PostTable
                    WHERE postID =  @postID
                ";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@postID", queryString));
            try
            {
                return 資料庫相關.查詢單筆資料(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
    }
}