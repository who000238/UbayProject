using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace UbayProject
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //確認使用者未登入
            if (this.Session["UserLoginInfo"] != null)
            {
                this.Response.Redirect("Main.aspx");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //查詢輸入帳號、信箱是否同時屬於同一個使用者
            string inputAccount = this.txtAccount.Text;
            string inputEmail = this.txtMail.Text;
            bool isUserExist = false;
            string newPassword;
            using (ORM.ContextModel content = new ORM.ContextModel())
            {
                var temp =
                    (from user in content.UserTables
                     where user.userID.ToString() == inputAccount
                     select user).FirstOrDefault();

                if ( temp != null &&  temp.email == inputEmail)
                {
                    //將使用者密碼改為隨機8位的密碼
                    newPassword = Membership.GeneratePassword(8, 3);
                    temp.pwd = newPassword;
                    isUserExist = true;
                    content.SaveChanges();
                }
                else 
                {
                    Response.Write("<script type='text/javascript'> alert('輸入錯誤');location.href = 'MainPage.aspx';</script>");
                }
            }

            //寄出修改後的密碼給使用者
            if (isUserExist)
            {
                //寄出信
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //回到首頁
            this.Response.Redirect("Main.aspx");
        }
    }
}