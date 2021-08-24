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
            //判斷是否使用者登入了
            //seesion null check，同時沒登入就關閉修改按鈕
            if (this.Session["userID"] == null)
            {
                this.btnUpdateUserBirthday.Visible = false;
                this.btnUpdateUserIntro.Visible = false;
                this.btnUpdateUserName.Visible = false;
                this.btnUpdateUserPhoto.Visible = false;
                this.btnUpdateUserSex.Visible = false;
            }

            //取得目前應顯示的使用者資料(QueryString)
            //var currentUser = UserInfoModel GetUserInfo(string encryptedGUID){}


            if (this.Request.QueryString["UserID"] == null)
            {
                //如果有登入 顯示自己的UserInfo(從sessionID找?)
                //如果沒登入也沒QueryString，禁止訪問回傳狀態403    
            }
            else 
            {
                //依據選取的使用者顯示UserInfo
                string userID =  this.Request.QueryString["UserID"];
                //UserInfoModel.Name , ......
                this.lblUserName.Text = "";
                this.lblUserSex.Text = "";
                this.lblUserBirthday.Text = "";
                this.lblIntro.Text = "";
            }

            //取得目前登入使用者ID(by SessionID cookie?)，如果跟QuereyString一樣，且不等於null時允許編輯 
            if (this.Session["userid"]?.ToString() == this.Request.QueryString["UserID"] && (this.Session["userid"]?.ToString() != null)) 
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

            //重新導向修改頁面，並只修改UserName
            this.Response.Redirect("/UpdateUserInfo.aspx");
        }

        protected void btnUpdateUserIntro_Click(object sender, EventArgs e)
        {
            //再重新確認登入狀況一次
            //重新導向修改頁面，並只修改UserIntro
            this.Response.Redirect("/UpdateUserInfo.aspx");

        }

        protected void btnUpdateUserPhoto_Click(object sender, EventArgs e)
        {
            //再重新確認登入狀況一次
            //重新導向修改頁面，並只修改UserPhoto
            this.Response.Redirect("/UpdateUserInfo.aspx");

        }
    }
}