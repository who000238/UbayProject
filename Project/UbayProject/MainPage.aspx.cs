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
using Microsoft.Security.Application;
using System.Web;

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
                    link.NavigateUrl = $"SubPage/MainCategory.aspx?mainCategoryID={item.mainCategoryID}";
                }
            }

            //取得熱門貼文
            try
            {
                var list = PostHelper.GetHotPostByEF();
               
                this.Repeater1.DataSource = list;
                this.Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return;
            }

        }

        protected void linkLogout_Click(object sender, EventArgs e)
        {
            UserInfoHelper.Logout();
            Response.Redirect("MainPage.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //取得使用者搜尋值
            string txtSearch_input = HttpUtility.HtmlEncode(this.SearchBar.Text);

            //檢查輸入值
            if (string.IsNullOrWhiteSpace(txtSearch_input) == true)
            {
                Response.Write("<script>alert('搜尋字串不得留空或者輸入空格、請檢查後重新輸入')</script>");
                Response.Write("<script>document.location=document.location</script>");
            }
       
            Response.Redirect($"SearchPage.aspx?Search={txtSearch_input}");
        }


    }
}