using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using UbayProject.ORM;
using 登入功能的類別庫;
using 處理資料庫相關的類別庫;

namespace UbayProject
{
    public partial class SubSubMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //檢查登入
            if (this.Session["UserLoginInfo"] != null)
            {
                this.linkLogout.Visible = true;
                this.a_Login.Visible = false;
                this.postArea.Visible = true;
            }
            else
            {
                this.linkLogout.Visible = false;
                this.a_Login.Visible = true;
                this.postArea.Visible = false;
            }
            //取得subCategoryID並轉成INT
            string tempQuery2 = Request.QueryString["mainCategoryID"];
            int tempCatID2 = Convert.ToInt32(tempQuery2);
            using (ContextModel context = new ContextModel())
            {
                //產生子版連結
                var query =
                      (from item in context.SubCategoryTables
                       where item.mainCategoryID == tempCatID2
                       select item);
                foreach (var item in query)
                {
                    HyperLink link = new HyperLink();
                    this.BoardLink.Controls.Add(link);
                    link.Text = item.subCategoryName + "</br>";
                    link.NavigateUrl = $"/SubPage/{item.subCategoryName}.aspx?mainCategoryID={item.mainCategoryID}&subCategoryID={item.subCategoryID}";
                }
            }

            try
            {
                string tempQuery3 = Request.QueryString["subCategoryID"];

                int subCategoryID = Convert.ToInt32(tempQuery3);
                var obj = 取得貼文及UserNameEF版(subCategoryID);
                this.GridView1.DataSource = obj;
                this.GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('{ex}')</script>");
            }

        }
        protected void linkLogout_Click(object sender, EventArgs e)
        {
            使用者相關功能.登出();
            Response.Redirect("/MainPage.aspx");
        }


        protected void postSubmit_Click(object sender, EventArgs e)
        {
            string txtTitle = this.postTitle.Text;
            string txtInner = this.postInner.Text;
            string tempQuery2 = Request.QueryString["subCategoryID"];
            int tempCatID2 = Convert.ToInt32(tempQuery2);

            if (string.IsNullOrWhiteSpace(txtTitle) ||
                string.IsNullOrWhiteSpace(txtInner))
            {
                Response.Write("<script>alert('標題和內文不得為空')</script>");
                return;
            }
            UserModel currentUser = 使用者相關功能.取得目前登入者的資訊();
            string userID = currentUser.userID;

            createPost(txtTitle, txtInner, userID, tempCatID2);
            this.postTitle.Text= string.Empty;
            this.postInner.Text = string.Empty;
            //Response.Write("<script>alert('貼文新増成功')</script>");
            Response.Write("<script>document.location=document.location</script>");
        }
        public static void createPost(string title, string innerText, string userID,int subCategoryID)
        {
            string connStr = 資料庫相關.取得連線字串();
            string dbCommand =
                $@" INSERT INTO PostTable
                    (
                         postTitle
                        ,countOfLikes
                        ,countOfUnlikes
                        ,countOfViewers
                        ,userID
                        ,subCategoryID
                        ,createDate
                        ,postText
                    )    
                    VALUES
                    (
                        @postTitle
                        ,'0'
                        ,'0'
                        ,'0'
                        ,@userID
                        ,@subCategoryID
                        ,@createDate
                        ,@postText
                    )
                  ";
            // connect db & execute
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddWithValue("@postTitle", title);
                    comm.Parameters.AddWithValue("@subCategoryID", subCategoryID);
                    comm.Parameters.AddWithValue("@postText", innerText);
                    comm.Parameters.AddWithValue("@userID", userID);
                    comm.Parameters.AddWithValue("@createDate", DateTime.Now);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                    }
                }
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
                             where item.subCategoryID == subCategoryID
                             orderby item.createDate descending
                         select new
                         {
                             UserInfo.userID,
                             UserInfo.userName,
                             UserInfo.account,
                             UserInfo.pwd,
                             UserInfo.userLevel,
                             UserInfo.sex,
                             UserInfo.email,
                             UserInfo.birthday,
                             UserInfo.photoURL,
                             UserInfo.intro,
                             UserInfo.favoritePosts,
                             UserInfo.blackList,
                             item.createDate,
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