using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UbayProject.ORM;
using DBSource;

namespace AccountSource
{
    public class UserInfoHelper
    {
        /// <summary>頁面讀取檢查是否有使用者登入</summary>
        /// <returns></returns>
        public static bool IsLogined()
        {
            if (HttpContext.Current.Session["UserLoginInfo"] == null)
                return false;
            else
                return true;
        }
        /// <summary>取得目前登入的使用者資訊 </summary>
        /// <returns></returns>
        public static UserModel GetCurrentUser()
        {
            string userID = HttpContext.Current.Session["UserLoginInfo"].ToString();

            if (userID == null)
                return null;

            DataRow dr = getUserInfoByUserID(userID);
            if (dr == null)
            {
                HttpContext.Current.Session["UserLoginInfo"] = null;
                return null;
            }

            UserModel model = new UserModel();
            model.userID = dr["userID"].ToString();
            model.userName = dr["userName"].ToString();
            model.account = dr["account"].ToString();
            model.createDate = DateTime.Parse(dr["createDate"].ToString());
            model.userLevel = dr["userLevel"].ToString();
            model.sex = dr["sex"].ToString();
            model.email = dr["email"].ToString();
            model.birthday = DateTime.Parse(dr["birthday"].ToString());
            model.photoURL = dr["photoURL"].ToString();
            model.intro = dr["intro"].ToString();
            model.favoritePosts = dr["favoritePosts"].ToString();
            model.blackList = dr["blackList"].ToString();

            return model;
        }
        /// <summary>
        /// 取得使用者資訊ByUserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataRow getUserInfoByUserID(string userID)
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString =
                @" SELECT 
                        [userID]
                        ,[userName]
                        ,[account]
                        ,[createDate]
                        ,[userLevel] 
                        ,[sex] 
                        ,[email]
                        ,[birthday]
                        ,[photoURL]
                        ,[intro]
                        ,[favoritePosts]
                        ,[blackList]
                    FROM UserTable
                    WHERE [userID] = @userID
                ";


            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userID", userID));

            try
            {
                return DBHelper.ReadDataRow(connectionString, dbCommandString, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        /// <summary>登入功能</summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static bool TryLogin(string account, string pwd, out string errorMsg)
        {
            //check empty
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errorMsg = "帳號及密碼為必填";
                return false;
            }
            //read db and check
            var userInfo = getUserInfoByAccount(account);

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
                HttpContext.Current.Session["UserLoginInfo"] = userInfo.userID;
                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "登入失敗，請檢查帳號及密碼";
                return false;

            }
        }
        /// <summary>登出
        /// </summary>
        public static void Logout()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null;
        }
        /// <summary>取得使用者資訊ByAccount</summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static UserTable getUserInfoByAccount(string account)
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

        /// <summary>申請帳號</summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        public static void createAccount(string account, string pwd, string email, string userName)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" INSERT INTO [dbo].[UserTable]
                    (
	                   [userID]
                      ,[userName]
                      ,[account]
                      ,[pwd]
                      ,[createDate]
                      ,[userLevel]
                      ,[sex]
                      ,[email]
                      ,[birthday]
                      ,[photoURL]
                      ,[intro]
                      ,[blackList]
                    )
                    VALUES
                    (
                        NEWID()
                        ,@userName
                        ,@account
                        ,@pwd
                        ,@createDate
                        ,'1'
                        ,'1'
                        ,@userEmail
                        ,'2000-1-1'
                        ,'https://cdn4.iconfinder.com/data/icons/people-97/100/Male_Business_Formal-256.png'
                        ,'Hello'
                        ,'Y'
                    ) ";

            // connect db & execute

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@account", account));
            paramList.Add(new SqlParameter("@pwd", pwd));
            paramList.Add(new SqlParameter("@userName", userName));
            paramList.Add(new SqlParameter("@userEmail", email));
            paramList.Add(new SqlParameter("@createDate", DateTime.Now));
            try
            {
                int effectRows = DBHelper.ModifyData(connStr, dbCommand, paramList);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        /// <summary>檢查帳號是否重複</summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool checkAccountExist(string account)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" SELECT *FROM [UBayProject].[dbo].[UserTable]
                        
                    WHERE account = @account"
                 ;

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@account", account));

            try
            {
                if (DBHelper.ReadDataRow(connStr, dbCommand, list) != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
        /// <summary>取得使用者姓名ByUserID</summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static UserTable getUserNameByUserID(Guid userID) 
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UserTables
                         where item.userID == userID
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
