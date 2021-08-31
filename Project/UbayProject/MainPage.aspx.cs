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
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //檢查登入
            if (this.Session["UserLoginInfo"] != null)
            {
                this.linkLogout.Visible = true;
                this.a_Login.Visible = false;
                this.UserInfoLink.Visible = true;
            }
            else
            {
                this.linkLogout.Visible = false;
                this.a_Login.Visible = true;
                this.UserInfoLink.Visible = false;

            }
            using (ContextModel context = new ContextModel())
            {
                var query =
                     (from item in context.MainCategoryTables
                      select item);
                foreach (var item in query)
                {
                    HyperLink link = new HyperLink();
                    this.BoardLink.Controls.Add(link);
                    link.Text = item.mainCategoryName + "</br>";
                    link.NavigateUrl = $"SubPage/{item.mainCategoryName}.aspx?mainCategoryID={item.mainCategoryID}";
                }
            }

            //取得熱門貼文
            try
            {
                var dt = GetHotPost();
                this.GridView1.DataSource = dt;
                this.GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return;
            }

            //讀取session
            //資料庫連線
            //資料系結
        }

        protected void linkLogout_Click(object sender, EventArgs e)
        {
            使用者相關功能.登出();
            Response.Redirect("MainPage.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //取得使用者搜尋值
            string txtSearch_input = this.SearchBar.Text;
            //檢查輸入值
            if (string.IsNullOrWhiteSpace(txtSearch_input) == true)
            {
                Response.Write("<script>alert('搜尋字串不得留空或者輸入空格、請檢查後重新輸入')</script>");
            }
            var obj = 搜尋貼文EF(txtSearch_input);
            if (obj != null)
            {
                this.GridView1.Visible = false;
                this.GridView2.DataSource = obj;
                this.GridView2.DataBind();
            }
        }

        public static Object 搜尋貼文EF(string Input_txt) //與subsubmaster 取得貼文及UserNameEF版相同
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.PostTables
                         join UserInfo in context.UserTables
                          on item.userID equals UserInfo.userID
                          where item.postTitle.Contains(Input_txt)
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

        //public static DataTable 搜尋貼文(string Input_txt) //待刪
        //{
        //    string connStr = 資料庫相關.取得連線字串();
        //    string dbCommand =
        //        $@"SELECT * FROM PostTable 
        //            JOIN UserTable ON PostTable.userID = UserTable.userID
        //            WHERE postTitle Like '%{Input_txt}%' ";
        //    List<SqlParameter> list = new List<SqlParameter>();
        //    list.Add(new SqlParameter("@Input_txt", Input_txt));
        //    try
        //    {
        //        return 資料庫相關.查詢資料清單(connStr, dbCommand, list);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteLog(ex);
        //        return null;
        //    }
        //}

        public static DataTable GetHotPost()
        {
            string connStr = 資料庫相關.取得連線字串();
            string dbCommand =
                $@" 
                SELECT TOP (5) [postID]
                      ,[postTitle]
                      ,[countOfLikes]
                      ,[countOfUnlikes]
                      ,[countOfViewers]
                      ,[PostTable].[userID]
                      ,[subCategoryID]
                      ,[PostTable].[createDate]
                      ,[postText]
                      ,[userName]
                  FROM [UBayProject].[dbo].[PostTable]
                   INNER JOIN UserTable ON PostTable.userID = UserTable.userID
                  ORDER BY countOfViewers DESC
                ";
            List<SqlParameter> list = new List<SqlParameter>();

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

    
    }
}