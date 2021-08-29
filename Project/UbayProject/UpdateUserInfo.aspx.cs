using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace UbayProject
{
    public partial class UpdateUserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //檢查登入
            if (this.Session["UserLoginInfo"] == null)
            {
                Response.Redirect("Login.aspx");
            }


            //檢查QuerryString長度，有不合長度限制的QuerryString就回傳403
            if (this.Request.QueryString.Get("UserID")?.Length != 36 && (this.Request.QueryString.Get("mode")?.Length< 19) )
            {
                Response.StatusCode = 403;
                Response.End();
            }

            // 檢查權限，登入的使用者與修改使用者不同顯示403
            if (this.Session["UserLoginInfo"].ToString() != this.Request.QueryString["UserID"])
            {
                Response.StatusCode = 403;
                Response.End();
            }

            //依QuerryString mode 調整textbox 是否可編輯
            string queryStringMode = this.Request.QueryString["mode"];
            switch (queryStringMode)
            {
                case "UpdateUserName":
                    this.txtUserName.Enabled = true;
                    this.txtUserBirthday.Enabled = false;
                    this.ddlUserSex.Enabled = false;
                    this.txtUserIntro.Enabled = false;
                    this.txtImg.Enabled = false;
                    break;

                case "UpdateUserBirthday":
                    this.txtUserName.Enabled = false;
                    this.txtUserBirthday.Enabled = true;
                    this.txtUserBirthday.TextMode = TextBoxMode.Date;
                    this.ddlUserSex.Enabled = false;
                    this.txtUserIntro.Enabled = false;
                    this.txtImg.Enabled = false;
                    break;
                case "UpdateUserSex":
                    this.txtUserName.Enabled = false;
                    this.txtUserBirthday.Enabled = false;
                    this.ddlUserSex.Enabled = true;
                    this.txtUserIntro.Enabled = false;
                    this.txtImg.Enabled = false;
                    break;

                case "UpdateUserIntro":
                    this.txtUserName.Enabled = false;
                    this.txtUserBirthday.Enabled = false;
                    this.ddlUserSex.Enabled = false;
                    this.txtUserIntro.Enabled = true;
                    this.txtImg.Enabled = false;
                    break;

                case "UpdateUserPhoto":
                    this.txtUserName.Enabled = false;
                    this.txtUserBirthday.Enabled = false;
                    this.ddlUserSex.Enabled = false;
                    this.txtUserIntro.Enabled = false;
                    this.txtImg.Enabled = true;
                    break;

                default:
                    break;
            }
            // 取得QueryString並填入使用者原始資訊(注意顯示時要注意輸出檢查)


            UbayProject.ORM.UserTable loginedUserNow;
            string userIDQueryString = this.Request.QueryString["UserID"];
            using (ORM.ContextModel content = new ORM.ContextModel())
            {
                var temp =
                    (from user in content.UserTables
                     where user.userID.ToString() == userIDQueryString
                     select user).FirstOrDefault();
                //if temp == null?
                loginedUserNow = temp;
            }
            if (!IsPostBack)
            {
                this.txtUserName.Text = loginedUserNow.userName;
                this.txtUserBirthday.Text = loginedUserNow.birthday?.ToString("yyyy/MM/dd");
                this.ddlUserSex.SelectedValue = loginedUserNow.sex;
                this.txtUserIntro.Text = loginedUserNow.intro;
                this.userImg.ImageUrl = (loginedUserNow.photoURL == null || loginedUserNow.photoURL == string.Empty)
                                        ? "https://freerangestock.com/thumbnail/35900/red-question-mark.jpg"
                                        : loginedUserNow.photoURL;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //sessionID 權限檢查，失敗重新導回登入，登入逾時?
            if (this.Session["UserLoginInfo"] == null)
            {
                Response.Redirect("UserInfo.aspx");
            }
            string str = this.txtUserName.Text;
            string userIDQueryString = this.Request.QueryString["UserID"];
            using (ORM.ContextModel content = new ORM.ContextModel())
            {
                var temp =
                    (from user in content.UserTables
                     where user.userID.ToString() == userIDQueryString
                     select user).FirstOrDefault();
                string queryStringMode = this.Request.QueryString["mode"];
                switch (queryStringMode)
                {
                    case "UpdateUserName":
                        temp.userName = this.txtUserName.Text;
                        break;

                    case "UpdateUserBirthday":
                        //UPDATE USER BIRTHDAY
                        temp.birthday = Convert.ToDateTime(this.txtUserBirthday.Text);
                        break;
                    case "UpdateUserSex":
                        temp.sex = this.ddlUserSex.SelectedValue;
                        break;
                    //UPDATE USER SEX
                    case "UpdateUserIntro":
                        temp.intro = this.txtUserIntro.Text;
                        break;

                    case "UpdateUserPhoto":
                        temp.photoURL = this.txtImg.Text;
                        break;

                    default:
                        break;
                }
                content.SaveChanges();
            }

            Response.Redirect("UserInfo.aspx");

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInfo.aspx");
        }

        protected void ibtnToMain_Click(object sender, ImageClickEventArgs e)
        {
            string temp = this.Request.QueryString["userid"];
            this.Response.Redirect($"UserInfo.aspx?userid={temp}");
        }
    }

}