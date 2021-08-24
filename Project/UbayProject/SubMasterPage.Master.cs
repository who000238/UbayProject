using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using 登入功能的類別庫;
using 處理資料庫相關的類別庫;

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
            }
            else
            {
                this.linkLogout.Visible = false;
                this.a_Login.Visible = true;
            }

        
        }

        protected void linkLogout_Click(object sender, EventArgs e)
        {
            使用者相關功能.登出();
            Response.Redirect("/MainPage.aspx");
        }

  
    }
}