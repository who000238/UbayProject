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
using PostAndCommentSource;
using Microsoft.Security.Application;

namespace UbayProject
{
    public partial class SeePost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            //檢查登入
            if (this.Session["UserLoginInfo"] != null)
            {
                //檢查黑名單
                UserModel currentUser = UserInfoHelper.GetCurrentUser();
                if (currentUser.blackList == "Y")
                    this.commentArea.Visible = false;
                else
                    this.commentArea.Visible = true;
                //管理員身分則啟用刪除貼文按鈕
                if (currentUser.userLevel == "0")
                {
                    this.btnDeletePost.Visible = true;
                    this.ManagerArea.Visible = true;
                    this.commentIDarea.Visible = true;
                }
            }
            else
            {
                this.commentArea.Visible = false;
            }

            //讀取網址列中的貼文ID
            string postQueryString = this.Request.QueryString["postID"];

            this.hfpostID.Value = postQueryString;
            var dr = PostHelper.getPostByPostID(postQueryString);
            if (dr == null)
            {
                Response.Write("<script>alert('查無該貼文');location.href='MainPage.aspx';</script>");

                return;
            }
            //取得貼文內容
            string userID = dr["userID"].ToString();                                                                                    //取得發貼文者ID
            var postUserInfo = UserInfoHelper.getUserNameByUserID(Guid.Parse(userID));                 //已發貼文者ID去UserTable取得發文者的使用者暱稱

            int tempcountOfViewers = Convert.ToInt32(dr["countOfViewers"]);
            this.lblViewer.Text = (tempcountOfViewers + 1).ToString() + "人";
            this.lblTitle.Text = dr["postTitle"].ToString() + "</br>" + $"發文者:{postUserInfo.userName}       發文時間:{dr["createDate"]}";
            this.lblInner.Text = dr["postText"].ToString();
            int postID = Convert.ToInt32(postQueryString);

            if (this.commentInput.Text == null)
                this.commentInput.Text = string.Empty;


            CommentHelper.UpdateViewers(postID, tempcountOfViewers);
            //按讚功能
            this.Label1.Text = dr["countOfLikes"].ToString();
            this.Label2.Text = dr["countOfUnlikes"].ToString();

        }


        protected void commentSubmit_Click(object sender, EventArgs e)
        {
            string txtComment = Encoder.HtmlEncode(this.commentInput.Text);
            int postID = Convert.ToInt32(this.Request.QueryString["postID"]);
            UserModel currentUser = UserInfoHelper.GetCurrentUser();
            string userID = currentUser.userID;
            if (currentUser == null)
            {
                Response.Write("<script>alert('尚未登入')</script>");
                return;
            }

            if (txtComment.Length > 4000)
            {
                Response.Write("<script>alert('超過字數上限、請不要一次輸入過多的訊息!!')</script>");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtComment))
            {
                Response.Write("<script>alert('你還沒寫下你的留言吧?')</script>");
                return;
            }

            CommentHelper.addComment(txtComment, userID, postID);
            this.commentInput.Text = string.Empty;
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
                Response.Write("<script>document.location=document.location</script>");

            }
        }
        protected void BtnDislike_Click(object sender, EventArgs e)
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
                    item.countOfUnlikes += 1;
                }

                context.SaveChanges();
                Response.Write("<script>document.location=document.location</script>");

            }
        }

        protected void btnDeletePost_Click(object sender, EventArgs e)
        {
            string postIDtxt = Request.QueryString["postID"];
            int postID = Convert.ToInt32(postIDtxt);

            Response.Write($"<script>alert('即將刪除貼文編號:{postID}號的貼文')</script>");

            PostHelper.deletePost(postID);
            Response.Write("<script>alert('刪除貼文成功!!');location.href='MainPage.aspx'</script>");

        }

        protected void btnDeleteComment_Click(object sender, EventArgs e)
        {
            string commentIDtxt = this.InpDeleteCommentID.Text;
            int commentID = Convert.ToInt32(commentIDtxt);

            string Msg;
            Response.Write($"<script>alert('即將刪除留言編號:{commentID}號的留言')</script>");
            CommentHelper.deleteComment(commentID, out Msg);
            if (!string.IsNullOrEmpty(Msg))
            {
                Response.Write("<script>alert('查無該編號的留言!!')</script>");
                return;
            }
            else
            {
                Response.Write("<script>alert('刪除留言成功!!')</script>");
                return;
            }


        }
    }
}
