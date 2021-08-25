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
            string queryString = this.Request.QueryString["ID"];
            var dr = getPostByPostID(queryString);
            this.lblTitle.Text = dr["postTitle"].ToString();
            this.lblInner.Text = dr["comment"].ToString();
        }
        public DataRow getPostByPostID(string queryString)
        {
            string connStr = 資料庫相關.取得連線字串();
            string dbCommand =
                $@"select PostTable.postID
                        ,PostTable.postTitle
                        ,PostTable.countOfLikes
                        ,PostTable.countOfUnlikes
                        ,PostTable.countOfViewers
                        ,PostTable.createDate
                        ,PostTable.subCategoryID
                        ,PostTable.userID
                        ,CommentTable.comment
                        from PostTable
                        join CommentTable on CommentTable.postID=PostTable.postID
                    where PostTable.postID =  @postID
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