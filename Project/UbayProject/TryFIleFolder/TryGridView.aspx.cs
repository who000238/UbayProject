using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UbayProject.ORM;
using 處理資料庫相關的類別庫;

namespace UbayProject
{
    public partial class TryGri : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                int subCategoryID = 1;
                var obj = 取得貼文及UserNameEF版(subCategoryID);
                this.GridView1.DataSource = obj;
                this.GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('{ex}')</script>");
            }

        }

        public static UserTable getUserNameByUserID(Guid userID) //EF版本
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UserTables
                         where item.userID == userID
                         select item);

                    var obj = query.FirstOrDefault();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        } // 好像不需要這個功能
        public static Object 取得貼文及UserNameEF版(int subCategoryID)
        {
            try
            {
                using (ContextModel context = new ContextModel())  // 用串聯的方式查詢postTable的同時也去把user
                {
                    var query =
                        //(from item in context.PostTables
                        // where item.subCategoryID == subCategoryID
                        // select item);
                        (from item in context.PostTables
                         join UserInfo in context.UserTables
                             on item.userID equals UserInfo.userID
                         select new
                         {
                             UserInfo.userID,
                             UserInfo.userName,
                             UserInfo.account,
                             UserInfo.pwd,
                             UserInfo.createDate,
                             UserInfo.userLevel,
                             UserInfo.sex,
                             UserInfo.email,
                             UserInfo.birthday,
                             UserInfo.photoURL,
                             UserInfo.intro,
                             UserInfo.favoritePosts,
                             UserInfo.blackList,
                             item.postTitle,
                             item.postID,
                             item.countOfLikes,
                             item.countOfUnlikes,
                             item.countOfViewers,
                             item.subCategoryID,
                             item.postText
                         });
                    var obj = query.ToList();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        public static DataTable 取得貼文(int subCategoryID)
        {
            string connStr = 資料庫相關.取得連線字串();
            string dbCommand =
                $@" SELECT 
                        [postID]
                        ,[postTitle]
                        ,[countOfLikes]
                        ,[countOfUnlikes]
                        ,[countOfViewers]
                        ,[userID]
                        ,[subCategoryID]
                        ,[createDate]
                        ,[postText]
                    FROM [PostTable]
                    WHERE [subCategoryID] = @subCategoryID
                ";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@subCategoryID", subCategoryID));

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

            //var data = e.Row.DataItem as PostTable;
            //Guid uid = data.userID;


            //var userInfo = getUserNameByUserID(uid);


            //var lblUserName = row.FindControl("ltlUserName") as Label;
            //lblUserName.Text = userInfo.account;
        }

    }
}