﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using 登入功能的類別庫;
using 處理資料庫相關的類別庫;

namespace UbayProject
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //頁面讀取
            //檢查是否有登入狀態
            if (this.Session["UserLoginInfo"] != null)
            {
                Response.Redirect("MainPage.aspx");
            }
            else
            {
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
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
            if(string.Compare(inp_PWD,inp_CheckPWD) != 0)
            {
                Response.Write($"<script>alert('兩次輸入的密碼並不相同、請確認')</script>");
                return;
            }
            //比較帳號有沒有重複，若有則告知使用者
            var tempAccount = 使用者相關功能.查詢帳號是否重複(inp_Account);
            if (tempAccount)
            {
                Response.Write($"<script>alert('此帳號重複、請重新輸入')</script>");
                return;
            }

            //確認所有欄位送出後，跳轉至使用者資訊頁面引導填入完整使用者訊息
            使用者相關功能.申請帳號(inp_Account, inp_PWD, inp_email, inp_userName);
            //Response.Write($"<script>alert('申請帳號成功、按下確認前往論壇')</script>");
            Response.Redirect("MainPage.aspx");

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            //按下欄位，清除所有Input的欄位的輸入值
            this.txtAccount.Text = string.Empty;
            this.txtPWD.Text = string.Empty;
            this.txtPWDCheck.Text = string.Empty;
            this.txtMail.Text = string.Empty;
            this.txtUserName.Text = string.Empty;
        }
    }
}