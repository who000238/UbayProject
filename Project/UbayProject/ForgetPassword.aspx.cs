using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net.Mail;

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
            this.Response.Redirect("MainPage.aspx");
        }
        protected void sendMail(string receiveEmailAddress, string emailHtmlContent, string title)
        {
            try
            {
                MailMessage mail = new MailMessage();
                //前面是發信email後面是顯示的名稱
                mail.From = new MailAddress("ubayproject2021@gmail.com", title);

                //收信者email
                mail.To.Add(receiveEmailAddress);

                //設定優先權
                mail.Priority = MailPriority.Normal;

                //標題
                mail.Subject = "AutoEmail";

                //內容
                mail.Body = emailHtmlContent;

                //內容使用html
                mail.IsBodyHtml = true;

                //設定gmail的smtp (這是google的)
                SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);

                //您在gmail的帳號密碼
                MySmtp.Credentials = new System.Net.NetworkCredential("ubayproject2021", "73057305");

                //開啟ssl
                MySmtp.EnableSsl = true;

                //發送郵件
                MySmtp.Send(mail);

                //放掉宣告出來的MySmtp
                MySmtp = null;

                //放掉宣告出來的mail
                mail.Dispose();

                //提示成功
                Response.Write("<script type='text/javascript'> alert('已寄出');location.href = 'MainPage.aspx';</script>");


            }
            catch (Exception ex)
            {
                // 寫進LOG

                Response.Write("<script type='text/javascript'> alert('寄出失敗');location.href = 'MainPage.aspx';</script>");

            }
        }
    }
}