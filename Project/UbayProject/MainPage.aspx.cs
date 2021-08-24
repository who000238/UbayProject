using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using 登入功能的類別庫;

namespace UbayProject
{
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //檢查登入
            if (this.Session["UserLoginInfo"] != null)
            {
                this.btnLogout.Visible = true;
                this.a_Login.Visible = false;
            }
            else
            {
                this.btnLogout.Visible = false;
                this.a_Login.Visible = true;
            }
            //讀取session
            //資料庫連線
            //資料系結
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            使用者相關功能.登出();
            Response.Redirect("MainPage.aspx");
        }

       
    }
}