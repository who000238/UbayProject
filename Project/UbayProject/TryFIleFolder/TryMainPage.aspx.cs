using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UbayProject.ORM;
using AccountSource;

namespace UbayProject
{
    public partial class TryMainPage : System.Web.UI.Page
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
                    link.NavigateUrl = $"SubPage/{item.mainCategoryName}.aspx?mainCategoryID={item.mainCategoryID}";
                }
            }


            //讀取session
            //資料庫連線
            //資料系結
        }

        protected void linkLogout_Click(object sender, EventArgs e)
        {
            UserInfoHelper.Logout();
            Response.Redirect("MainPage.aspx");
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {

        }
    }
}