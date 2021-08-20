using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UbayProject
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //頁面讀取
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //按下按鈕後，讀取所有Input內的值
            //有欄位為空的話 顯示錯誤訊息
            //比較帳號有沒有重複，若有則告知使用者

            //確認所有欄位送出後，跳轉至登入頁面
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            //按下欄位，清除所有Input的欄位的輸入值
        }
    }
}