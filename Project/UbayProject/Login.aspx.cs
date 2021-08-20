using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UbayProject
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string acc = "Admin";
            string pwd = "123";
            string inp_acc = this.txtAccount.Text;
            string inp_pwd = this.txtPassowrd.Text;
            //判別是否為空值
            if (string.IsNullOrWhiteSpace(inp_acc) || string.IsNullOrWhiteSpace(inp_pwd))
            {
                Response.Write("<script>alert('帳號或密碼不得為空')</script>"); //使用alert告知使用者訊息
            }

            if (string.Compare(acc, inp_acc, true) == 0 && (string.Compare(pwd, inp_pwd)==0))
            {
                Response.Write("<script>alert('Success')</script>");
                Response.Redirect("MainPage.aspx");
            }
        }
    }
}