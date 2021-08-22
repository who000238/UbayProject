using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UbayProject.ORM.DBModels;
using 處理資料庫相關的類別庫;

namespace 登入功能的類別庫
{
    public class 登入功能
    {
        public static void 檢查目前是否已登入()
        {

        }

        public static void 取得目前登入者的資訊()
        {

        }

        public static bool 嘗試登入(string account, string pwd, out string errorMsg)
        {
            //check empty
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errorMsg = "帳號及密碼為必填";
                return false;
            }
            //read db and check
            var userInfo = GetUserInfoByAccount(account);

            //check null
            if (userInfo == null)
            {
                errorMsg = $"{account}不存在";
                return false;
            }

            //check account / pwd
            if (string.Compare(userInfo.account, account, true) == 0 &&
                string.Compare(userInfo.pwd, pwd, false) == 0)
            {
                HttpContext.Current.Session["UserLoginInfo"] = userInfo.account;
                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "登入失敗，請檢查帳號及密碼";
                return false;

            }
        }

        public static void 登出()
        {

        }

        public static UserTable GetUserInfoByAccount(string account)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UserTables
                         where item.account == account
                         select item);

                    var obj = query.FirstOrDefault();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }

        }

    }
}
