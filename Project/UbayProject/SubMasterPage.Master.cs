﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using UbayProject.ORM;
using AccountSource;
using DBSource;

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
                //this.postArea.Visible = false;ㄏ
            }

            //取得mainCategoryID並轉成INT
            string tempQuery = Request.QueryString["mainCategoryID"];
            int tempCatID = Convert.ToInt32(tempQuery);
            using (ContextModel context = new ContextModel())
            {
                //產生母版連結
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

                //產生子版連結
                var query2 =
                      (from item in context.SubCategoryTables
                       where item.mainCategoryID == tempCatID
                       select item);
                foreach (var item in query2)
                {
                    HyperLink link = new HyperLink();
                    this.ContentPlaceHolder1.Controls.Add(link);
                    link.ImageUrl = $"Pics/{item.subCategoryName}.jpg";
                    link.Text = item.subCategoryName + "</br>";
                    link.NavigateUrl = $"/SubPage/{item.subCategoryName}.aspx?mainCategoryID={item.mainCategoryID}&subCategoryID={item.subCategoryID}";
                }

            }

           

        }
        protected void linkLogout_Click(object sender, EventArgs e)
        {
            UserInfoHelper.Logout();
            Response.Redirect("/MainPage.aspx");
        }


        //protected void postSubmit_Click(object sender, EventArgs e)
        //{
        //    string txtTitle = this.postTitle.Text;
        //    string txtInner = this.postInner.Text;

        //    if (string.IsNullOrWhiteSpace(txtTitle) ||
        //        string.IsNullOrWhiteSpace(txtInner))
        //    {
        //        Response.Write("<script>alert('標題和內文不得為空')</script>");
        //        return;
        //    }
        //    UserModel currentUser = 使用者相關功能.取得目前登入者的資訊();
        //    string userID = currentUser.userID;

        //    createPost(txtTitle, txtInner, userID);
        //    Response.Write("<script>alert('貼文新増成功')</script>");

        //}
        //public static void createPost(string title, string innerText, string userID)
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
        //            comm.Parameters.AddWithValue("@subCategoryID", 1);
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

        //public static DataTable 取得貼文(int categoryID) //可刪
        //{
        //    string connStr = DBHelper.GetConnectionString();
        //    string dbCommand =
        //        $@" SELECT 
        //                [postTitle],
        //                [createDate],
        //                [postID]
        //            FROM [PostTable]
        //            WHERE [subCategoryID] = @subCategoryID
        //        ";
        //    List<SqlParameter> list = new List<SqlParameter>();
        //    list.Add(new SqlParameter("@subCategoryID", categoryID));
        //    try
        //    {
        //        return DBHelper.ReadDataTable(connStr, dbCommand, list);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteLog(ex);
        //        return null;
        //    }
        //}
        private int GetCurrentPage()
        {
            string pageText = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(pageText))
            {
                return 1;
            }
            int intPage;
            if (!int.TryParse(pageText,out intPage))
            {
                return 1;
            }

            if(intPage <= 0)
            {
                return 1;
            }

            return intPage;
        }

        private DataTable GetPagedDataTable(DataTable dt)
        {
            DataTable dtPaged = (dt.Rows.Count==0)?dt.Clone() :
            dt.Copy();

            foreach(DataRow dr in dt.Rows)
            {
                var drNew = dtPaged.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    drNew[dc.ColumnName] = dr[dc];
                }

                dtPaged.Rows.Add(drNew);
            }
            return dtPaged;
        }
    }

}
