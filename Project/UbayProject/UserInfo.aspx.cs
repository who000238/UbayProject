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
            //seesion null check，同時沒登入就隱藏修改按鈕
            if (this.Session["UserLoginInfo"] == null)
            {
                this.btnUpdateUserBirthday.Visible = false;
                this.btnUpdateUserIntro.Visible = false;
                this.btnUpdateUserName.Visible = false;
                this.btnUpdateUserPhoto.Visible = false;
                this.btnUpdateUserSex.Visible = false;
            }
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

            //檢查QuerryString長度，有不合長度的QuerryString就回傳403
            if (this.Request.QueryString.Get("UserID")?.Length != 36)
            {
                //禁止訪問 回傳403
                Response.StatusCode = 403;
                Response.End();
            }

            //取得目前應顯示的使用者資料(QueryString)，依據選取的使用者顯示UserInfo
            UbayProject.ORM.UserTable queriedUserNow;
            string userIDQueryString = this.Request.QueryString["UserID"];
            using (ORM.ContextModel content = new ORM.ContextModel())
            {
                var temp =
                    (from user in content.UserTables
                     where user.userID.ToString() == userIDQueryString
                     select user).FirstOrDefault();
                queriedUserNow = temp;
            }
            //if temp == null? 沒找到使用者，顯示預設"查無此人"
            if (queriedUserNow != null)
            {
                this.lblUserName.Text = queriedUserNow.userName;
                this.lblBlackList.Text = queriedUserNow.blackList;
                this.lblUserSex.Text = (queriedUserNow.sex == "無")
                                             ? ("不公開")
                                             : queriedUserNow.sex;
                this.lblUserBirthday.Text = queriedUserNow.birthday?.ToString("yyyy/MM/dd");
                this.txtUserIntro.Text = queriedUserNow.intro;

                //顯示URL
                //無法正常顯示該怎麼判定?
                this.userImg.ImageUrl = (queriedUserNow.photoURL == null || queriedUserNow.photoURL == string.Empty)
                                            ? "https://freerangestock.com/thumbnail/35900/red-question-mark.jpg"
                                            : queriedUserNow.photoURL;
                //怎麼做輸出檢查?
            }


            //取得目前登入使用者ID(by SessionID cookie)，如果跟QuereyString一樣，開啟編輯按鈕 
            if (this.Session["UserLoginInfo"]?.ToString() == this.Request.QueryString["UserID"])
            {
                this.btnUpdateUserBirthday.Visible = true;
                this.btnUpdateUserIntro.Visible = true;
                this.btnUpdateUserName.Visible = true;
                this.btnUpdateUserPhoto.Visible = true;
                this.btnUpdateUserSex.Visible = true;
            }

            //檢查目前登入者資料，如果UserLevel == 0，且不是自己的帳號，且對方權限不是管理者顯示刪除按鈕、黑名單欄位

            UbayProject.ORM.UserTable loginedUserNow;
            string logineduserID = this.Session["UserLoginInfo"]?.ToString();
            using (ORM.ContextModel content = new ORM.ContextModel())
            {
                var temp =
                    (from user in content.UserTables
                     where user.userID.ToString() == logineduserID
                     select user).FirstOrDefault();
                //if temp == null? 沒找到使用者，提示無相關資料
                loginedUserNow = temp;
            }
            if (loginedUserNow != null && queriedUserNow!= null)
            {
                if (loginedUserNow.userLevel == 0)
                {
                    this.trBlackList.Visible = true;
                    if (queriedUserNow.userID.ToString() != logineduserID && queriedUserNow.userLevel != 0)
                    {
                        this.btnBlackList.Visible = true;
                        this.btnDeleteUser.Visible = true;
                    }
                }
                else
                {
                    this.trBlackList.Visible = false;
                }

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
            //alert

            //確認使用者權限等級(session)(取得登入者資訊>確認Level)

            //刪除(querystring user)
            string userIDQueryString = this.Request.QueryString["UserID"];
            using (ORM.ContextModel content = new ORM.ContextModel())
            {
                var temp =
                    (from user in content.UserTables
                     where user.userID.ToString() == userIDQueryString
                     select user).FirstOrDefault();
                //if temp == null? 沒找到使用者，提示無相關資料

                content.UserTables.Remove(temp);
                content.SaveChanges();
            }

            this.Response.Redirect($"UserInfo.aspx?userid={userIDQueryString}");

        }

        protected void ibtnToMain_Click(object sender, ImageClickEventArgs e)
        {
            this.Response.Redirect("MainPage.aspx");
        }

        protected void btnBlackList_Click(object sender, EventArgs e)
        {
            string userIDQueryString = this.Request.QueryString["UserID"];

            using (ORM.ContextModel content = new ORM.ContextModel())
            {
                var temp =
                    (from user in content.UserTables
                     where user.userID.ToString() == userIDQueryString
                     select user).FirstOrDefault();
                //if temp == null? 沒找到使用者，提示無相關資料

                //修改黑名單值
                temp.blackList = (temp.blackList.ToString() == "Y")
                               ? "N"
                               : "Y";
                content.SaveChanges();
            }
            this.Response.Redirect($"UserInfo.aspx?userid={userIDQueryString}");

        }
    }
}