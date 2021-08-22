using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UbayProject.Auth;

namespace UbayProject
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //檢查是否登入
            if (this.Session["UserLoginInfo"] != null)
            {
                Response.Redirect("/SystemAdmin/UserInfo.aspx");
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

            if (!AuthManager.TryLogin(inp_acc,inp_pwd,out msg))
            {
            Response.Write($"<script>alert('{msg}')</script>");
                return;
            }
            Response.Redirect("MainPage.aspx");
        }
    }
}