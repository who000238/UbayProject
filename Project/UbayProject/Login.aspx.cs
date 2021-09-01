using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountSource;

namespace UbayProject
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //檢查是否有登入狀態
            //檢查是否有登入狀態
            //if (this.Session["UserLoginInfo"] != null)
            if (HttpContext.Current.Session["UserLoginInfo"] != null)
            {
                Response.Redirect("MainPage.aspx");
            }
            else
            {
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string inp_acc = this.txtAccount.Text;
            string inp_pwd = this.txtPassowrd.Text;
            string msg;
            if (!UserInfoHelper.TryLogin(inp_acc, inp_pwd, out msg))
            {
                Response.Write($"<script>alert('{msg}')</script>");
                return;
            }

            Response.Redirect("MainPage.aspx");
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            //按下取消回到主選單頁面
            Response.Write($"<script>alert('success')</script>");
            Response.Redirect("MainPage.aspx");
        }
    }
}