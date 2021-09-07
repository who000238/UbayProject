using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using UbayProject.ORM;
using AccountSource;
using DBSource;
using PostAndCommentSource;

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
            //檢查黑名單
            UserModel currentUser = UserInfoHelper.GetCurrentUser();
            if (currentUser.blackList == "Y")
                this.postArea.Visible = false;
            else
                this.postArea.Visible = true;
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
                var obj = PostHelper.getPostAndUserName(subCategoryID);
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
            UserInfoHelper.Logout();
            Response.Redirect("/MainPage.aspx");
        }


        protected void postSubmit_Click(object sender, EventArgs e)
        {
            string txtTitle = this.postTitle.Text;
            string txtInner = this.postInner.Text;
            string tempQuery2 = Request.QueryString["subCategoryID"];
            int tempCatID2 = Convert.ToInt32(tempQuery2);
            if (txtTitle.Length > 50)
            {
                Response.Write("<script>alert('標題過長')</script>");
                return;

            }
            if (txtInner.Length > 4000)
            {
                Response.Write("<script>alert('內文過長')</script>");
                return;

            }

            if (string.IsNullOrWhiteSpace(txtTitle) ||
                string.IsNullOrWhiteSpace(txtInner))
            {
                Response.Write("<script>alert('標題和內文不得為空')</script>");
                return;
            }


            UserModel currentUser = UserInfoHelper.GetCurrentUser();
            string userID = currentUser.userID;

            PostHelper.createPost(txtTitle, txtInner, userID, tempCatID2);
            this.postTitle.Text = string.Empty;
            this.postInner.Text = string.Empty;
            //Response.Write("<script>alert('貼文新増成功')</script>");
            Response.Write("<script>document.location=document.location</script>");
        }
        //public static void createPost(string title, string innerText, string userID,int subCategoryID) //待刪
        //{
        //    string connStr = DBHelper.GetConnectionString();
        //    string dbCommand =
        //        $@" INSERT INTO PostTable
        //            (
        //                 postTitle
        //                ,countOfLikes
        //                ,countOfUnlikes
        //                ,countOfViewers
        //                ,userID
        //                ,subCategoryID
        //                ,createDate
        //                ,postText
        //            )    
        //            VALUES
        //            (
        //                @postTitle
        //                ,'0'
        //                ,'0'
        //                ,'0'
        //                ,@userID
        //                ,@subCategoryID
        //                ,@createDate
        //                ,@postText
        //            )
        //          ";
        //    // connect db & execute
        //    using (SqlConnection conn = new SqlConnection(connStr))
        //    {
        //        using (SqlCommand comm = new SqlCommand(dbCommand, conn))
        //        {
        //            comm.Parameters.AddWithValue("@postTitle", title);
        //            comm.Parameters.AddWithValue("@subCategoryID", subCategoryID);
        //            comm.Parameters.AddWithValue("@postText", innerText);
        //            comm.Parameters.AddWithValue("@userID", userID);
        //            comm.Parameters.AddWithValue("@createDate", DateTime.Now);

        //            try
        //            {
        //                conn.Open();
        //                comm.ExecuteNonQuery();

        //            }
        //            catch (Exception ex)
        //            {
        //                Logger.WriteLog(ex);
        //            }
        //        }
        //    }
        //}

        //public static UserTable getUserNameByUserID(Guid userID) //EF版本
        //{
        //    try
        //    {
        //        using (ContextModel context = new ContextModel())
        //        {
        //            var query =
        //                (from item in context.UserTables
        //                 where item.userID == userID
        //                 select item);

        //            var obj = query.FirstOrDefault();
        //            return obj;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteLog(ex);
        //        return null;
        //    }
        //} // 好像不需要這個功能
        //public static Object getPostAndUserName(int subCategoryID)
        //{
        //    try
        //    {
        //        using (ContextModel context = new ContextModel())  // 用串聯的方式查詢postTable的同時也去把user
        //        {
        //            var query =
        //                (from item in context.PostTables
        //                 join UserInfo in context.UserTables
        //                     on item.userID equals UserInfo.userID
        //                 where item.subCategoryID == subCategoryID
        //                 orderby item.createDate descending
        //                 select new
        //                 {
        //                     UserInfo.userID,
        //                     UserInfo.userName,
        //                     UserInfo.account,
        //                     UserInfo.pwd,
        //                     UserInfo.userLevel,
        //                     UserInfo.sex,
        //                     UserInfo.email,
        //                     UserInfo.birthday,
        //                     UserInfo.photoURL,
        //                     UserInfo.intro,
        //                     UserInfo.favoritePosts,
        //                     UserInfo.blackList,
        //                     item.createDate,
        //                     item.postTitle,
        //                     item.postID,
        //                     item.countOfLikes,
        //                     item.countOfUnlikes,
        //                     item.countOfViewers,
        //                     item.subCategoryID,
        //                     item.postText
        //                 });
        //            var obj = query.ToList();
        //            return obj;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteLog(ex);
        //        return null;
        //    }
        //}

        //public static DataTable getPost(int subCategoryID) //這個貌似也可以刪除
        //{
        //    string connStr = DBHelper.GetConnectionString();
        //    string dbCommand =
        //        $@" SELECT 
        //                [postID]
        //                ,[postTitle]
        //                ,[countOfLikes]
        //                ,[countOfUnlikes]
        //                ,[countOfViewers]
        //                ,[userID]
        //                ,[subCategoryID]
        //                ,[createDate]
        //                ,[postText]
        //            FROM [PostTable]
        //            WHERE [subCategoryID] = @subCategoryID
        //        ";
        //    List<SqlParameter> list = new List<SqlParameter>();
        //    list.Add(new SqlParameter("@subCategoryID", subCategoryID));

        //    try
        //    {
        //        return DBHelper.ReadDataTable(connStr, dbCommand, list);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteLog(ex);
        //        return null;
        //    }
        ////}
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;

            //var data = e.Row.DataItem as PostTable;
            //Guid uid = data.userID;


            //var userInfo = getUserNameByUserID(uid);


            //var lblUserName = row.FindControl("ltlUserName") as Label;
            //lblUserName.Text = userInfo.account;
        }

        public void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string tempQuery3 = Request.QueryString["subCategoryID"];
            int subCategoryID = Convert.ToInt32(tempQuery3);
            var obj = PostHelper.getPostAndUserName(subCategoryID);
            GridView1.PageIndex = e.NewPageIndex;
            this.GridView1.DataSource = obj;
            GridView1.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int subCategoryID = Convert.ToInt32(Request.QueryString["subCategoryID"]);
            string txtSearch_input = this.SearchBar.Text;
            if (string.IsNullOrWhiteSpace(txtSearch_input) == true)
            {
                Response.Write("<script>alert('搜尋字串不得留空或者輸入空格、請檢查後重新輸入')</script>");
                Response.Write("<script>document.location=document.location</script>");

            }
            var obj = PostHelper.searchPost(txtSearch_input, subCategoryID);
            if (obj != null)
            {
                this.GridView1.Visible = false;
                this.GridView2.DataSource = obj;
                this.GridView2.DataBind();
            }
        }
    }

}