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

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string inp_acc = this.txtAccount.Text;
            string inp_pwd = this.txtPassowrd.Text;
            string msg;
            if (!使用者相關功能.嘗試登入(inp_acc,inp_pwd,out msg))
            {
                Response.Write($"<script>alert('{msg}')</script>");
                return;
            }
            Response.Redirect("MainPage.aspx");
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            //按下取消回到主選單頁面
            Response.Redirect("MainPage.aspx");
        }
    }
}