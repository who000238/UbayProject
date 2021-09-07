using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UbayProject.ORM;
using AccountSource;
using DBSource;

namespace UbayProject.TryFIleFolder
{
    public partial class TrySeePostWithASHX : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //檢查登入
            if (this.Session["UserLoginInfo"] != null)
            {
                this.commentArea.Visible = true;
            }
            else
            {
                this.commentArea.Visible = false;
            }
            
            //讀取網址列中的貼文ID
            string postQueryString = this.Request.QueryString["postID"];
            this.hfpostID.Value = postQueryString;
            var dr = getPostByPostID(postQueryString);                                           //取得貼文內容
            string userID = dr["userID"].ToString();                                                        //取得發貼文者ID
            var postUserInfo = getUserNameByUserID(Guid.Parse(userID));                 //已發貼文者ID去UserTable取得發文者的使用者暱稱
            if (dr == null)
            {
                Response.Write("<script>alert('該貼文不存在')</script>"); // 這邊會無法顯示 會直接跳頁
                Response.Redirect("MainPage.aspx");
            }
            int tempcountOfViewers = Convert.ToInt32(dr["countOfViewers"]);
            this.lblViewer.Text = (tempcountOfViewers + 1).ToString() + "人";
            this.lblTitle.Text = dr["postTitle"].ToString() + "</br>" + $"發文者:{postUserInfo.userName}       發文時間:{dr["createDate"]}";
            this.lblInner.Text = dr["postText"].ToString();
            int postID = Convert.ToInt32(postQueryString);

            if (this.commentInput.Text == null)
                this.commentInput.Text = string.Empty;

            UpdateViewers(postID, tempcountOfViewers);
            //按讚功能
            this.Label1.Text = dr["countOfLikes"].ToString();
   
        }


        protected void commentSubmit_Click(object sender, EventArgs e)
        {
            string txtComment = this.commentInput.Text;
            int postID = Convert.ToInt32(this.Request.QueryString["postID"]);
            UserModel currentUser = UserInfoHelper.GetCurrentUser();
            if (currentUser == null)
            {
                Response.Write("<script>alert('尚未登入')</script>");
                return;
            }
            string userID = currentUser.userID;
            if (!string.IsNullOrWhiteSpace(txtComment))
            {
                addComment(txtComment, userID, postID);
                this.commentInput.Text = "";
            }
            else
                Response.Write("<script>alert('你還沒寫下你的留言吧?')</script>");
        }


        public DataRow getPostByPostID(string queryString)
        {
            string connStr = DBHelper.GetConnectionString();
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
                return DBHelper.ReadDataRow(connStr, dbCommand, list);
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
            string connStr = DBHelper.GetConnectionString();
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
                int effectRows = DBHelper.ModifyData(connStr, dbCommand, list);

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
            string connStr = DBHelper.GetConnectionString();
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
                return DBHelper.ReadDataRow(connStr, dbCommand, list);
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
        /// <summary>
        /// 在page_unload的時候把VIewer+1
        /// </summary>
        /// <param name="postID"></param>
        /// <param name="tempcountOfViewers"></param>
        /// <returns></returns>
        public static bool UpdateViewers(int postID, int tempcountOfViewers)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@"
                     UPDATE PostTable
                    SET
                               countOfViewers           =   @countOfViewers 
                    WHERE
                        postID = @postID
                     ";
            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@countOfViewers", tempcountOfViewers + 1));
            paramList.Add(new SqlParameter("@postID", postID));

            try
            {
                int effectRows = DBHelper.ModifyData(connStr, dbCommand, paramList);

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

        protected void BtnLike_Click(object sender, EventArgs e)
        {
            string tempQuery = Request.QueryString["postID"];
            int tempPostID = Convert.ToInt32(tempQuery);
            using (ContextModel context = new ContextModel())
            {
                var query =
                      (from item in context.PostTables
                       where item.postID == tempPostID
                       select item);
                foreach (var item in query)
                {
                    item.countOfLikes += 1;
                }

                context.SaveChanges();
            }
        }
    }
}