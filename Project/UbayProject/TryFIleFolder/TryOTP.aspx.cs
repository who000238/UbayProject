using AccountSource;
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
    public partial class TryOTP : System.Web.UI.Page
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
            //string text = this.TextBox1.Text;
            //using (ContextModel context = new ContextModel())
            //{
            //    var query =
            //          (from item in context.UserTables
            //           where item.userName == "1234"
            //           select item);
            //    foreach (var item in query)
            //    {
            //        item.OTP = text;
            //    }

            //    context.SaveChanges();
            //}
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
           
            string inp_Account = this.txtAccount.Text;
            string inp_PWD = this.txtPWD.Text;
            string inp_CheckPWD = this.txtPWDCheck.Text;
            string inp_email = this.txtMail.Text;
            string inp_userName = this.txtUserName.Text;
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

            //
            Random myObject = new Random();
            int ranNum = myObject.Next(10000000, 99999999);
            string emailContent = $@"http://localhost:54101/TryActive.aspx
                                     請在輸入框內輸入{ranNum}";
            string email = "ubayproject2021@gmail.com";
            sendMail(email, emailContent, "OTP");
            //

            //確認所有欄位送出後，跳轉至使用者資訊頁面引導填入完整使用者訊息
            UserInfoHelper.createAccount(inp_Account, inp_PWD, inp_email, inp_userName);

           
            using (ContextModel context = new ContextModel())
            {
                var query =
                      (from item in context.UserTables
                       where item.userName == inp_userName
                       select item);
                foreach (var item in query)
                {
                    item.OTP = ranNum.ToString();
                }

                context.SaveChanges();
            }
            //Response.Write($"<script>alert('申請帳號成功、按下確認前往論壇')</script>");
            Response.Redirect("MainPage.aspx");
            

           
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