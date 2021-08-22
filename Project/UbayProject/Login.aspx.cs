using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using 登入功能的類別庫;

namespace UbayProject
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //檢查是否登入
            if(this.Session["UserLoginInfo"] != null)
            {
                Response.Redirect("MainPage.aspx"); //若session存在、導至MainPage畫面
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string inp_Account = this.txtAccount.Text;
            string inp_PWD = this.txtPassowrd.Text;
            string msg;
            if(!登入功能.嘗試登入(inp_Account,inp_PWD,out msg))
            {
                Response.Write($"<script>alert('{msg}')</script>");
                return;
            }
                Response.Redirect("MainPage.aspx");
        }
    }
}