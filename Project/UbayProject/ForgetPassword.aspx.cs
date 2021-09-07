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
                Response.Write("<script type='text/javascript'> alert('您已登入，無法使用忘記密碼功能');location.href = 'MainPage.aspx';</script>");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //輸入檢查
            if (this.txtAccount.Text == null || this.txtMail.Text == null)
            {
                Response.Write("<script type='text/javascript'> alert('輸入不正確，將轉至首頁');location.href = 'MainPage.aspx';</script>");
            }


            //查詢輸入帳號、信箱是否同時屬於同一個使用者
            string inputAccount = this.txtAccount.Text;
            string inputEmail = this.txtMail.Text;
            bool isUserExist = false;
            string newPassword = "";
            using (ORM.ContextModel content = new ORM.ContextModel())
            {
                var temp =
                    (from user in content.UserTables
                     where user.account.ToString() == inputAccount
                     select user).FirstOrDefault();

                if ( temp != null &&  temp.email == inputEmail)
                {
                    //將使用者密碼改為隨機8位的密碼
                    newPassword = Membership.GeneratePassword(8, 0);
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
                //產生Email信件內容
                string emailContent = $"你的密碼是:{newPassword}";
                //寄出信
                sendMail(inputEmail,emailContent, "忘記密碼");
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
                DBSource.Logger.WriteLog(ex);
                Response.Write("<script type='text/javascript'> alert('寄出失敗');location.href = 'MainPage.aspx';</script>");

            }
        }
    }
}