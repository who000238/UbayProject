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
            //取得目前登入使用者ID(by SessionID cookie?) 
            if (this.Session["userID"]==null)
            {
                
            }


            //取得目前顯示使用者資料(QueryString)
            //UserInfoModel GetUserInfo(string encryptedGUID){}
            if (this.Request.QueryString["UserID"] == null)
            {
                //顯示自己的UserInfo(從sessionID找?)
            }
            else { 
                //依據選取的使用者
            }

            //顯示在各個位置
            if (this.Session["userid"]?.ToString() == this.Request.QueryString["UserID"]) 
            {
                //顯示自己的資料同時顯示修改按鈕
                //UserInfoModel.Name , ......
                this.lblUserName.Text = "";
                this.lblUserSex.Text = "";
                this.lblUserBirthday.Text = "";
                this.lblIntro.Text = "";
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