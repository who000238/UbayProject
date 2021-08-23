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
            //取得目前登入使用者ID(Session)
            //取得目前顯示使用者資料(QueryString)
            //顯示在各個位置
        }

        protected void btnUpdateUserName_Click(object sender, EventArgs e)
        {
            //重新導向修改頁面，並只修改UserName
        }

        protected void btnUpdateUserIntro_Click(object sender, EventArgs e)
        {
            //重新導向修改頁面，並只修改UserIntro
        }

        protected void btnUpdateUserPhoto_Click(object sender, EventArgs e)
        {
            //重新導向修改頁面，並只修改UserPhoto
        }
    }
}