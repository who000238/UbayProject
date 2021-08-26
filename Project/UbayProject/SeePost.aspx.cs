﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UbayProject.ORM;
using 登入功能的類別庫;
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
            if (dr == null)
            {
                Response.Write("<script>alert('該貼文不存在')</script>");
                Response.Redirect("MainPage.aspx");
            }
            this.lblTitle.Text = dr["postTitle"].ToString();
            this.lblInner.Text = dr["postText"].ToString();
            int postID = Convert.ToInt32(this.Request.QueryString["postID"]);
            //var Comment = getComment(postID);
            //this.lblComment.Text = Comment["comment"].ToString();
            if (this.commentInput.Text == null)
                this.commentInput.Text = string.Empty;

            var Comment = GetCommentByEF(postID);
            foreach (var item in Comment)
            {
                var userInfo = getUserNameByUserID(item.userID);

                Label labelUp = new Label();
                Label labelDown = new Label();
                this.commentPostArea.Controls.Add(labelUp);
                this.commentPostArea.Controls.Add(labelDown);
                labelUp.Text = $"使用者ID:{userInfo.userName}    留言時間:{item.createDate}  </br>";
                labelDown.Text = $"留言:{item.comment}  </br> ------------------------------------------ </br>";

                //label.Text = $"留言:{item.comment},  使用者ID:{item.userID}, 留言時間:{item.createDate} </br>";
                //label.Text = item.comment +"&nbsp;" + item.userID + "&nbsp;" + item.createDate + "</br>";
                //this.lblComment.Text += item.comment + "</br>";
            }
        }


        protected void commentSubmit_Click(object sender, EventArgs e)
        {
            string txtComment = this.commentInput.Text;
            int postID = Convert.ToInt32(this.Request.QueryString["postID"]);
            UserModel currentUser = 使用者相關功能.取得目前登入者的資訊();
            if (currentUser == null)
            {
                Response.Write("<script>alert('尚未登入')</script>");
                return;
            }
            string userID = currentUser.userID;
            if (!string.IsNullOrWhiteSpace(txtComment))
                addComment(txtComment, userID, postID);
            else
                Response.Write("<script>alert('你還沒寫下你的留言吧?')</script>");
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
        }


        public static bool addComment(string txtComment, string userID, int postID)
        {
            string connStr = 資料庫相關.取得連線字串();
            string dbCommand =
                $@"  INSERT INTO CommentTable
                (
                          postID
                          ,comment
                          ,userID
                          ,createDate
                )
                          VALUES 
                (
                              @postID
                              ,@comment
                              ,@userID
                              ,@createDate
                )
                ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@postID", postID));
            list.Add(new SqlParameter("@comment", txtComment));
            list.Add(new SqlParameter("@userID", userID));
            list.Add(new SqlParameter("@createDate", DateTime.Now));
            try
            {
                int effectRows = 資料庫相關.ModifyData(connStr, dbCommand, list);

                if (effectRows == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }

        public static DataRow getComment(int postID)
        {
            string connStr = 資料庫相關.取得連線字串();
            string dbCommand =
              $@" SELECT 
                      comment,
                      userID,
                      createDate
                    FROM CommentTable
                    WHERE postID = @postID 
                ";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@postID", postID));
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

        public static List<CommentTable> GetCommentByEF(int postID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.CommentTables
                         where item.postID == postID
                         select item);
                    var list = query.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
    }
}
