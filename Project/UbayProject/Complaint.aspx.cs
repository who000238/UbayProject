using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UbayProject
{
    public partial class complaint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //確認使用者目前有登入
            if (this.Session["UserLoginInfo"] == null)
            {
                Response.Write("<script type='text/javascript'> alert('請先登入');location.href = 'MainPage.aspx';</script>");
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //確認有登入的使用者
            if (this.Session["UserLoginInfo"] == null)
            {
                Response.Write("<script type='text/javascript'> alert('請先登入');location.href = 'MainPage.aspx';</script>");
            }
            //取得目前登入使用者資料
            UbayProject.ORM.UserTable loginedUserNow;
            string logineduserID = this.Session["UserLoginInfo"]?.ToString();
            using (ORM.ContextModel content = new ORM.ContextModel())
            {
                var temp =
                    (from user in content.UserTables
                     where user.userID.ToString() == logineduserID
                     select user).FirstOrDefault();
                loginedUserNow = temp;
            }
            
            //產生寄信內容String
            string emailContent = 
                    $@"申訴使用者訊息:
                       申訴標題:{this.txtContent.Text}
                       申訴內容:{this.txtTitle.Text}";

            
            //找出所有使用者中管理員權限為0的使用者(管理員)
            List<string> ltAdminEmailAddress = new List<string>();
            using (ORM.ContextModel content = new ORM.ContextModel())
            {
                var temp =
                    (from user in content.UserTables
                     where user.userLevel == 0
                     select user);
                
                foreach(var user in temp)
                {
                    ltAdminEmailAddress.Add(user.email);
                }
            }
            //寄出
            foreach (string email in ltAdminEmailAddress)
            {
                sendMail(email,emailContent,$"論壇使用者{loginedUserNow.userID}申訴");
            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            //清除Input
            this.txtTitle.Text = "";
            this.txtContent.Text = "";
        }


        protected void sendMail(string receiveEmailAddress,string emailHtmlContent,string title) 
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