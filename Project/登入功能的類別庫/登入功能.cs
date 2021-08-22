using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using 處理資料庫相關的類別庫;

namespace 登入功能的類別庫
{
    public class 登入功能
    {
        public static bool 檢查目前是否已登入()
        {

            if (HttpContext.Current.Session["UserLoginInfo"] == null)
                return false;
            else
                return true;
        }

        public static UserInfoModel 取得目前登入者的資訊()
        {
            string account = HttpContext.Current.Session["UserLoginInfo"] as string;

            if (account == null)
                return null;

            var userInfo = 管理使用者相關資料.GetUserInfoByAccount(account);

            if (userInfo == null)
            {
                HttpContext.Current.Session["UserLoginIngo"] = null;
                return null;
            }

            UserInfoModel model = new UserInfoModel();
            model.ID = userInfo.userID; //這個userID是int還是guid ?
            model.Account = userInfo.account;
            model.Name = userInfo.userName;
            model.Email = userInfo.email;
            return model;
        }

        public static bool 嘗試登入(string account, string pwd, out string errorMsg)
        {
            //check empty
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errorMsg = "帳號或密碼不得為空";
                return false;
            }

            //read db and check account is exist
            var userInfo = 管理使用者相關資料.GetUserInfoByAccount(account);

            //check null
            if (userInfo == null)
            {
                errorMsg = $"查無{account}此帳號的資料、請確認後重新輸入";
                return false;
            }

            //check account / pwd
            if (string.Compare(userInfo.account, account, true) == 0 &&
                string.Compare(userInfo.pwd, pwd) == 0)
            {
                //HttpContext.Current.Session["UserLoginInfo"] = userInfo.Account;
                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "登入失敗、請檢察帳號、密碼";
                return false;
            }
        }

        public static void 登出()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null;
        }


    }
}
