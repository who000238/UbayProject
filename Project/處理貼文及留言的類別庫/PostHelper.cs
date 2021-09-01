using DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbayProject.ORM;

namespace PostAndCommentSource
{
    public class PostHelper
    {
        /// <summary>發布貼文</summary>
        /// <param name="title"></param>
        /// <param name="innerText"></param>
        /// <param name="userID"></param>
        /// <param name="subCategoryID"></param>
        public static void createPost(string title, string innerText, string userID, int subCategoryID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" INSERT INTO PostTable
                    (
                         postTitle
                        ,countOfLikes
                        ,countOfUnlikes
                        ,countOfViewers
                        ,userID
                        ,subCategoryID
                        ,createDate
                        ,postText
                    )    
                    VALUES
                    (
                        @postTitle
                        ,'0'
                        ,'0'
                        ,'0'
                        ,@userID
                        ,@subCategoryID
                        ,@createDate
                        ,@postText
                    )
                  ";
            // connect db & execute
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddWithValue("@postTitle", title);
                    comm.Parameters.AddWithValue("@subCategoryID", subCategoryID);
                    comm.Parameters.AddWithValue("@postText", innerText);
                    comm.Parameters.AddWithValue("@userID", userID);
                    comm.Parameters.AddWithValue("@createDate", DateTime.Now);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                    }
                }
            }
        }
        /// <summary>取得熱門貼文(根據貼文的瀏覽次數)</summary>
        /// <returns></returns>
        public static DataTable GetHotPost()
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" 
                SELECT TOP (5) [postID]
                      ,[postTitle]
                      ,[countOfLikes]
                      ,[countOfUnlikes]
                      ,[countOfViewers]
                      ,[PostTable].[userID]
                      ,[subCategoryID]
                      ,[PostTable].[createDate]
                      ,[postText]
                      ,[userName]
                  FROM [UBayProject].[dbo].[PostTable]
                   INNER JOIN UserTable ON PostTable.userID = UserTable.userID
                  ORDER BY countOfViewers DESC
                ";
            List<SqlParameter> list = new List<SqlParameter>();

            try
            {
                return DBHelper.ReadDataTable(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
        /// <summary>搜尋貼文(標題)</summary>
        /// <param name="Input_txt"></param>
        /// <returns></returns>
        public static Object searchPost(string Input_txt) 
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.PostTables
                         join UserInfo in context.UserTables
                          on item.userID equals UserInfo.userID
                         where item.postTitle.Contains(Input_txt)
                         select new
                         {
                             UserInfo.userID,
                             UserInfo.userName,
                             UserInfo.account,
                             UserInfo.pwd,
                             UserInfo.userLevel,
                             UserInfo.sex,
                             UserInfo.email,
                             UserInfo.birthday,
                             UserInfo.photoURL,
                             UserInfo.intro,
                             UserInfo.favoritePosts,
                             UserInfo.blackList,
                             item.createDate,
                             item.postTitle,
                             item.postID,
                             item.countOfLikes,
                             item.countOfUnlikes,
                             item.countOfViewers,
                             item.subCategoryID,
                             item.postText
                         });

                    var obj = query.ToList();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
        public static Object searchPost(string Input_txt,int subCategoryID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.PostTables
                         join UserInfo in context.UserTables
                          on item.userID equals UserInfo.userID
                         where item.postTitle.Contains(Input_txt)
                         where item.subCategoryID == subCategoryID
                         select new
                         {
                             UserInfo.userID,
                             UserInfo.userName,
                             UserInfo.account,
                             UserInfo.pwd,
                             UserInfo.userLevel,
                             UserInfo.sex,
                             UserInfo.email,
                             UserInfo.birthday,
                             UserInfo.photoURL,
                             UserInfo.intro,
                             UserInfo.favoritePosts,
                             UserInfo.blackList,
                             item.createDate,
                             item.postTitle,
                             item.postID,
                             item.countOfLikes,
                             item.countOfUnlikes,
                             item.countOfViewers,
                             item.subCategoryID,
                             item.postText
                         });

                    var obj = query.ToList();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        /// <summary>取得貼文及發文者的使用者姓名</summary>
        /// <param name="subCategoryID"></param>
        /// <returns></returns>
        public static Object getPostAndUserName(int subCategoryID)
        {
            try
            {
                using (ContextModel context = new ContextModel())  // 用串聯的方式查詢postTable的同時也去把user
                {
                    var query =
                        (from item in context.PostTables
                         join UserInfo in context.UserTables
                             on item.userID equals UserInfo.userID
                         where item.subCategoryID == subCategoryID
                         orderby item.createDate descending
                         select new
                         {
                             UserInfo.userID,
                             UserInfo.userName,
                             UserInfo.account,
                             UserInfo.pwd,
                             UserInfo.userLevel,
                             UserInfo.sex,
                             UserInfo.email,
                             UserInfo.birthday,
                             UserInfo.photoURL,
                             UserInfo.intro,
                             UserInfo.favoritePosts,
                             UserInfo.blackList,
                             item.createDate,
                             item.postTitle,
                             item.postID,
                             item.countOfLikes,
                             item.countOfUnlikes,
                             item.countOfViewers,
                             item.subCategoryID,
                             item.postText
                         });
                    var obj = query.ToList();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
        /// <summary>取得貼文 </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static DataRow getPostByPostID(string postQueryString)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@"SELECT PostTable.postID
                        ,postTitle
                        ,countOfLikes
                        ,countOfUnlikes
                        ,countOfViewers
                        ,createDate
                        ,subCategoryID
                        ,userID
                        ,postText
                        FROM PostTable
                    WHERE postID =  @postID
                ";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@postID", postQueryString));
            try
            {
                return DBHelper.ReadDataRow(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
    }
}
