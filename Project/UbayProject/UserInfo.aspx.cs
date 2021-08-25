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
            
            //判斷是否使用者登入了(沒有、一般、限制、管理員)
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
            string queryString = this.Request.QueryString["userID"];
            this.Response.Redirect($"/UpdateUserInfo.aspx?userID={queryString}&mode=UpdateUserName");
        }

        protected void btnUpdateUserIntro_Click(object sender, EventArgs e)
        {
            //再重新確認登入狀況一次
            //重新導向修改頁面，並只修改UserIntro
            string queryString = this.Request.QueryString["userID"];
            this.Response.Redirect($"/UpdateUserInfo.aspx?userID={queryString}&mode=UpdateUserIntro");
        }

        protected void btnUpdateUserPhoto_Click(object sender, EventArgs e)
        {
            //再重新確認登入狀況一次
            //重新導向修改頁面，並只修改UserPhoto
            string queryString = this.Request.QueryString["userID"];
            this.Response.Redirect($"/UpdateUserInfo.aspx?userID={queryString}&mode=UpdateUserPhoto");
        }
        protected void btnUpdateUserSex_Click(object sender, EventArgs e)
        {
            //再重新確認登入狀況一次
            //重新導向修改頁面，並只修改UserPhoto
            string queryString = this.Request.QueryString["userID"];
            this.Response.Redirect($"/UpdateUserInfo.aspx?userID={queryString}&mode=UpdateUserSex");

        }

        protected void btnUpdateUserBirthday_Click(object sender, EventArgs e)
        {
            //再重新確認登入狀況一次
            //重新導向修改頁面，並只修改UserPhoto
            string queryString = this.Request.QueryString["userID"];
            this.Response.Redirect($"/UpdateUserInfo.aspx?userID={queryString}&mode=UpdateUserBirthday");
        }
        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            //確認使用者權限等級
            //刪除此使用者
        }

    }
}