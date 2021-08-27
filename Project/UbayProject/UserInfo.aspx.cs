using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UbayProject
{
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //預設登入(測試用)
            //this.Session["UserLoginInfo"] = "4B50687F-45B3-4B24-B830-14CCFB4F0126";

            //判斷是否使用者登入了
            //seesion null check，同時沒登入就關閉修改按鈕
            if (this.Session["UserLoginInfo"] == null)
            {
                this.btnUpdateUserBirthday.Visible = false;
                this.btnUpdateUserIntro.Visible = false;
                this.btnUpdateUserName.Visible = false;
                this.btnUpdateUserPhoto.Visible = false;
                this.btnUpdateUserSex.Visible = false;
            }
            //取得使用者
            UbayProject.ORM.UserTable loginedUserNow;
            string userIDQueryString = this.Request.QueryString["UserID"];
            if (this.Request.QueryString["UserID"] == null)
            {
                //沒登入又沒QueryString 禁止進入
                if (this.Session["UserLoginInfo"] == null)
                {
                    //禁止訪問 回傳403
                    Response.StatusCode = 403;
                    Response.End();
                }
                //有登入沒QueryString querysting = session
                else
                {
                    string str = this.Session["UserLoginInfo"].ToString();

                    Response.Redirect($"/UserInfo.aspx?userid={str}");
                }
            }
            using (ORM.ContextModel content = new ORM.ContextModel())
            {
                var temp =
                    (from user in content.UserTables
                     where user.userID.ToString() == userIDQueryString
                     select user).FirstOrDefault();
                //if temp == null?
                loginedUserNow = temp;
            }

            //取得目前應顯示的使用者資料(QueryString)
            //var currentUser = UserInfoModel GetUserInfo(string encryptedGUID){}

            //依據選取的使用者顯示UserInfo
            string userID = this.Request.QueryString["UserID"];
            //UserInfoModel.Name , ......
            this.lblUserName.Text = System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(loginedUserNow.userName, true);
            this.lblUserSex.Text = (loginedUserNow.sex == "無")
                                         ?("不公開")
                                         :loginedUserNow.sex;
            this.lblUserBirthday.Text = loginedUserNow.birthday?.ToString("yyyy/MM/dd");
            //this.txtUserIntro.Text = System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(loginedUserNow.intro, true);
            this.txtUserIntro.Text = loginedUserNow.intro;

            //顯示URL
            //無法正常顯示該怎麼判定?
            this.userImg.ImageUrl = (loginedUserNow.photoURL == null || loginedUserNow.photoURL == string.Empty)
                                        ? "https://freerangestock.com/thumbnail/35900/red-question-mark.jpg"
                                        : loginedUserNow.photoURL;
            this.userImg.Width = 225;
            //怎麼做輸出檢查?


            //取得目前登入使用者ID(by SessionID cookie?)，如果跟QuereyString一樣，且不等於null時允許編輯 
            if (this.Session["UserLoginInfo"]?.ToString() == this.Request.QueryString["UserID"] && (this.Session["UserLoginInfo"] != null))
            {
                this.btnUpdateUserBirthday.Visible = true;
                this.btnUpdateUserIntro.Visible = true;
                this.btnUpdateUserName.Visible = true;
                this.btnUpdateUserPhoto.Visible = true;
                this.btnUpdateUserSex.Visible = true;
            }
            else
            {
            }
        }

        protected void btnUpdateUserName_Click(object sender, EventArgs e)
        {
            //再重新確認登入狀況一次
            if (this.Session["UserLoginInfo"]?.ToString() == this.Request.QueryString["UserID"] && (this.Session["UserLoginInfo"] != null))
            {
                //重新導向修改頁面，並只修改UserName
                string queryString = this.Request.QueryString["userID"];
                this.Response.Redirect($"/UpdateUserInfo.aspx?userID={queryString}&mode=UpdateUserName");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnUpdateUserIntro_Click(object sender, EventArgs e)
        {
            //再重新確認登入狀況一次
            if (this.Session["UserLoginInfo"]?.ToString() == this.Request.QueryString["UserID"] && (this.Session["UserLoginInfo"] != null))
            {
                //重新導向修改頁面，並只修改UserIntro
                string queryString = this.Request.QueryString["userID"];
                this.Response.Redirect($"/UpdateUserInfo.aspx?userID={queryString}&mode=UpdateUserIntro");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnUpdateUserPhoto_Click(object sender, EventArgs e)
        {
            //再重新確認登入狀況一次
            if (this.Session["UserLoginInfo"]?.ToString() == this.Request.QueryString["UserID"] && (this.Session["UserLoginInfo"] != null))
            {
                //重新導向修改頁面，並只修改UserPhoto
                string queryString = this.Request.QueryString["userID"];
                this.Response.Redirect($"/UpdateUserInfo.aspx?userID={queryString}&mode=UpdateUserPhoto");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

        }
        protected void btnUpdateUserSex_Click(object sender, EventArgs e)
        {
            //再重新確認登入狀況一次
            if (this.Session["UserLoginInfo"]?.ToString() == this.Request.QueryString["UserID"] && (this.Session["UserLoginInfo"] != null))
            {
                //重新導向修改頁面，並只修改UserSex
                string queryString = this.Request.QueryString["userID"];
                this.Response.Redirect($"/UpdateUserInfo.aspx?userID={queryString}&mode=UpdateUserSex");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

        }

        protected void btnUpdateUserBirthday_Click(object sender, EventArgs e)
        {
            //再重新確認登入狀況一次
            if (this.Session["UserLoginInfo"]?.ToString() == this.Request.QueryString["UserID"] && (this.Session["UserLoginInfo"] != null))
            {
                //重新導向修改頁面，並只修改生日
                string queryString = this.Request.QueryString["userID"];
                this.Response.Redirect($"/UpdateUserInfo.aspx?userID={queryString}&mode=UpdateUserBirthday");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

        }
        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            //確認使用者權限等級
            //刪除此使用者
        }

    }
}