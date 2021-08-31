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
            //QuerryString null check
            if (this.Request.QueryString["UserID"] == null)
            {
                //沒登入又沒QueryString 要求登入
                if (this.Session["UserLoginInfo"] == null)
                {
                    this.Response.Redirect("Login.aspx");
                }

                //有登入沒QueryString querysting = session
                else
                {
                    string str = this.Session["UserLoginInfo"].ToString();

                    Response.Redirect($"/UserInfo.aspx?userid={str}");
                }
            }
            //檢查QuerryString長度，有不合長度的QuerryString就回傳403
            if (this.Request.QueryString.Get("UserID")?.Length != 36 )
            {
                //禁止訪問 回傳403
                Response.StatusCode = 403;
                Response.End();
            }

            //取得目前應顯示的使用者資料(QueryString)的使用者
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
            //有找到QuerrySting使用者
            if (queriedUserNow != null)
            {
                //他人檢視在黑名單的使用者時會看到提示 "(封鎖中)"
                //依據選取的使用者顯示UserInfo
                this.lblUserName.Text = HttpUtility.HtmlEncode(queriedUserNow.userName);
                this.lblBlackList.Text = queriedUserNow.blackList;
                this.lblUserSex.Text = (queriedUserNow.sex == "無")
                                             ? ("不公開")
                                             : queriedUserNow.sex;
                this.lblUserBirthday.Text = (queriedUserNow.birthday != null)
                                             ? queriedUserNow.birthday?.ToString("yyyy/MM/dd")
                                             : ("不公開");
                if (queriedUserNow.blackList == "Y")
                {
                    this.lblNameAlert.Text += "(封鎖中)";
                    this.txtUserIntro.Text = "(該使用者目前封鎖中)";
                }
                else
                {
                    this.txtUserIntro.Text = queriedUserNow.intro;
                }
                //顯示URL
                string[] imagendnames = { ".jpg" ,".png",".jpeg"};
                if (queriedUserNow.photoURL != null && queriedUserNow.photoURL != string.Empty && imagendnames.Any(x => queriedUserNow.photoURL.EndsWith(x) ))
                {
                    this.userImg.ImageUrl =queriedUserNow.photoURL;
                }
                else
                {
                    this.userImg.ImageUrl = "https://icons.veryicon.com/png/o/education-technology/alibaba-cloud-iot-business-department/image-load-failed.png";                }
            }
            //沒找到QuerrySting使用者
            //else { }


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
                loginedUserNow = temp;
            }
            //有找到QuerryString的使用者，且有找到Seesion的使用者
            if (loginedUserNow != null && queriedUserNow != null)
            {
                //登入者為黑名單使用者提示已被封鎖，並禁止查看自己/其他使用者資料
                if (loginedUserNow.blackList == "Y")
                {

                    //提示你已被封鎖
                    Response.Write("<script type='text/javascript'> alert('您的帳號已被封鎖，如有疑義請至網站申訴');location.href = 'MainPage.aspx';</script>");
                    //this.Response.Redirect("MainPage.aspx");
                }
                if (loginedUserNow.userLevel == 0)
                {
                    this.trA.Visible = true;
                    if (queriedUserNow.userID.ToString() != logineduserID && queriedUserNow.userLevel != 0)
                    {
                        this.btnBlackList.Visible = true;
                        this.btnDeleteUser.Visible = true;
                    }
                }
                else
                {
                    this.trA.Visible = false;
                }
            }
            //沒找到QuerryString的使用者()或沒找到Session使用者
            else
            {
                Response.Write("<script type='text/javascript'> alert('請先登入');location.href = 'MainPage.aspx';</script>");
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
                temp.blackList = (temp.blackList.ToString() == "N")
                               ? "Y"
                               : "N";
                content.SaveChanges();
            }
            this.Response.Redirect($"UserInfo.aspx?userid={userIDQueryString}");

        }

        protected void btnToMain_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("MainPage.aspx");
        }
    }
}