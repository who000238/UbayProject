using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UbayProject.ORM.DBModels;
using 登入功能的類別庫;
using 處理資料庫相關的類別庫;

namespace UbayProject
{
    public partial class SubPageMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //檢查登入
            if (this.Session["UserLoginInfo"] != null)
            {
                this.linkLogout.Visible = true;
                this.a_Login.Visible = false;
                //this.postArea.Visible = true;
            }
            else
            {
                this.linkLogout.Visible = false;
                this.a_Login.Visible = true;
                //this.postArea.Visible = false;
            }

            
            using (ContextModel context = new ContextModel())
            {
                var query =
                    (from mainCategoryID in context.MainCategoryTables
                     select mainCategoryID.mainCategoryName);
                foreach (var mainName in query)
                {
                    HyperLink link = new HyperLink();
                    this.BoardLink.Controls.Add(link);
                    link.Text = mainName + "</br>";
                    link.NavigateUrl = $"SubPage/{mainName}.aspx";
                }

                var query4 =
                    (from main in context.MainCategoryTables
                     join sub in context.SubCategoryTables
                     on main.mainCategoryID
                     equals sub.mainCategoryID
                     select sub.subCategoryName);
                foreach (var subName in query4)
                {
                    HyperLink link2 = new HyperLink();
                    this.ContentPlaceHolder1.Controls.Add(link2);
                    link2.Text = subName + "</br>";
                    link2.NavigateUrl = $"SubPage/{subName}.aspx";
                }


                //var query4 =
                //    (from main in context.MainCategoryTables
                //     join sub in context.SubCategoryTables
                //     on main.mainCategoryID
                //     equals sub.mainCategoryID
                //     select sub.subCategoryName);


                //foreach (var subName in query4)
                //{
                //    HyperLink link2 = new HyperLink();
                //    this.ContentPlaceHolder1.Controls.Add(link2);
                //    link2.Text = subName + "</br>";
                //    link2.NavigateUrl = $"SubPage/{subName}.aspx";
                //}
            }

            //using (ContextModel context = new ContextModel())
            //{
            //    var query =
            //        (from subCategoryID in context.SubCategoryTables
            //         select subCategoryID.subCategoryName);

            //    foreach (var subCategoryID in query)
            //    {
            //        HyperLink link = new HyperLink();
            //        this.ContentPlaceHolder1.Controls.Add(link);
            //        link.Text = subCategoryID + "</br>";
            //        link.NavigateUrl = $"SubPage/{subCategoryID.ToString()}.aspx";
            //    }
            //}
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

            if (string.IsNullOrWhiteSpace(txtTitle) ||
                string.IsNullOrWhiteSpace(txtInner))
            {
                Response.Write("<script>alert('標題和內文不得為空')</script>");
                return;
            }
            UserModel currentUser = 使用者相關功能.取得目前登入者的資訊();
            string userID = currentUser.userID;

            createPost(txtTitle, txtInner, userID);
            Response.Write("<script>alert('貼文新増成功')</script>");

        }
        public static void createPost(string title, string inner, string userID)
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
                    )
                        INSERT INTO CommentTable
                    (
		                    [comment]
                          ,[userID]
                          ,[postID]
                          ,[createDate]
                    )
                     VALUES   
                    ( 
                            @inner
                            ,@userID
                            ,'1'
                            ,@createDate
                    ) ";




            // connect db & execute
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddWithValue("@postTitle", title);
                    comm.Parameters.AddWithValue("@subCategoryID", 1);
                    comm.Parameters.AddWithValue("@inner", inner);
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
    }

}
