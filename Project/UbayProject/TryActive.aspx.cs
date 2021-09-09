using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UbayProject.ORM;

namespace UbayProject
{
    public partial class TryActive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //頁面讀取
            //檢查是否有登入狀態
            if (this.Session["UserLoginInfo"] != null)
            {
                Response.Redirect("MainPage.aspx");
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string iptOTP = this.TextBox1.Text;
            using (ContextModel context = new ContextModel())
            {
                var query =
                      (from item in context.UserTables
                       select item.OTP);
                foreach (var item in query)
                {
                    string dbOTP = item;

                    if (iptOTP == dbOTP)
                    {
                        var query2 =
                                  (from item2 in context.UserTables
                                   where item2.OTP == iptOTP
                                   select item2);
                        foreach (var item2 in query2)
                        {
                            item2.blackList = "N";
                        }
                    }
                }

                context.SaveChanges();
            }
            Response.Redirect("MainPage.aspx");
        }
    }
}