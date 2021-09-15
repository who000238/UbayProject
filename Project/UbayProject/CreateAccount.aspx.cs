using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountSource;
using DBSource;
using Microsoft.Security.Application;
using UbayProject.ORM;

namespace UbayProject
{
    public partial class CreateAccount : System.Web.UI.Page
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string inp_Account = Encoder.HtmlEncode(this.txtAccount.Text);
            string inp_PWD = Encoder.HtmlEncode(this.txtPWD.Text);
            string inp_CheckPWD = Encoder.HtmlEncode(this.txtPWDCheck.Text);
            string inp_email = Encoder.HtmlEncode(this.txtMail.Text);
            string inp_userName = Encoder.HtmlEncode(this.txtUserName.Text);

            string checkPWDLength = HttpUtility.HtmlDecode(inp_PWD);
            if (checkPWDLength.Length < 8)
            {
                Response.Write($"<script>alert('密碼太短')</script>");
                return;
            }


            //按下按鈕後，讀取所有Input內的值
            //有欄位為空的話 顯示錯誤訊息

            if (string.IsNullOrWhiteSpace(inp_Account) ||
                 string.IsNullOrWhiteSpace(inp_PWD) ||
                 string.IsNullOrWhiteSpace(inp_CheckPWD) ||
                 string.IsNullOrWhiteSpace(inp_email) ||
                 string.IsNullOrWhiteSpace(inp_userName))
            {
                Response.Write($"<script>alert('請確認所有欄位都有輸入值')</script>");
                return;
            }
            //比較兩次輸入的密碼有沒有相同
            if (string.Compare(inp_PWD, inp_CheckPWD) != 0)
            {
                Response.Write($"<script>alert('兩次輸入的密碼並不相同、請確認')</script>");
                return;
            }
            //比較帳號有沒有重複，若有則告知使用者
            var tempAccount = UserInfoHelper.checkAccountExist(inp_Account);
            if (tempAccount)
            {
                Response.Write($"<script>alert('此帳號重複、請重新輸入')</script>");
                return;
            }



            Random myObject = new Random();
            int ranNum = myObject.Next(10000000, 99999999);
            string emailContent = $@"http://localhost:54101/TryActive.aspx 您的驗證碼為:{ranNum}";
            string email = inp_email /*"ubayproject2021@gmail.com"*/;
            sendMail(email, emailContent, "OTP");


            string tempInp_Account = HttpUtility.HtmlDecode(this.txtAccount.Text);
            string tempInp_PWD = HttpUtility.HtmlDecode(this.txtPWD.Text);
            string tempInp_CheckPWD = HttpUtility.HtmlDecode(this.txtPWDCheck.Text);
            string tempInp_email = HttpUtility.HtmlDecode(this.txtMail.Text);
            string tempInp_userName = HttpUtility.HtmlDecode(this.txtUserName.Text);

            //確認所有欄位送出後，跳轉至使用者資訊頁面引導填入完整使用者訊息
            UserInfoHelper.createAccount(tempInp_Account, tempInp_PWD, tempInp_email, tempInp_userName);

            //存驗證碼進DB
            using (ContextModel context = new ContextModel())
            {
                var query =
                      (from item in context.UserTables
                       where item.userName == tempInp_userName
                       select item);
                foreach (var item in query)
                {
                    item.OTP = ranNum.ToString();
                }

                context.SaveChanges();
            }

            Response.Write("<script type='text/javascript'> alert('申請帳號成功，請前往信箱驗證你的帳號並按下確認前往論壇');location.href = 'MainPage.aspx';</script>");
            //Response.Redirect("MainPage.aspx");

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            //按下欄位，清除所有Input的欄位的輸入值
            this.txtAccount.Text = string.Empty;
            this.txtPWD.Text = string.Empty;
            this.txtPWDCheck.Text = string.Empty;
            this.txtMail.Text = string.Empty;
            this.txtUserName.Text = string.Empty;
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
                //Response.Write("<script type='text/javascript'> alert('申請帳號成功，請前往信箱驗證你的帳號並按下確認前往論壇');location.href = 'MainPage.aspx';</script>");


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