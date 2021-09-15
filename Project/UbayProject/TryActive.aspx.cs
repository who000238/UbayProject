using AccountSource;
using Microsoft.Security.Application;
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
                Response.Write($"<script>alert('驗證帳號成功、按下確認前往論壇');location.href='MainPage.aspx'</script>");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //string inp_Account = Encoder.HtmlEncode(this.txtAccount.Text);
            //string inp_PWD = Encoder.HtmlEncode(this.txtPWD.Text);
            //string inp_CheckPWD = Encoder.HtmlEncode(this.txtPWDCheck.Text);
            //string inp_email = Encoder.HtmlEncode(this.txtMail.Text);
            //string inp_userName = Encoder.HtmlEncode(this.txtUserName.Text);

            //Random myObject = new Random();
            //int ranNum = myObject.Next(10000000, 99999999);
            //string emailContent = $@"http://localhost:54101/TryActive.aspx 請在輸入框內輸入{ranNum}";
            //string email = inp_email /*"ubayproject2021@gmail.com"*/;
            //sendMail(email, emailContent, "OTP");


            ////確認所有欄位送出後，跳轉至使用者資訊頁面引導填入完整使用者訊息
            //UserInfoHelper.createAccount(inp_Account, inp_PWD, inp_email, inp_userName);

            ////存驗證碼進DB
            //using (ContextModel context = new ContextModel())
            //{
            //    var query =
            //          (from item in context.UserTables
            //           where item.userName == inp_userName
            //           select item);
            //    foreach (var item in query)
            //    {
            //        item.OTP = ranNum.ToString();
            //    }

            //    context.SaveChanges();
            //}
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
                Response.Write("<script type='text/javascript'> alert('申請帳號成功，請前往信箱驗證你的帳號並按下確認前往論壇');location.href = 'MainPage.aspx';</script>");


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