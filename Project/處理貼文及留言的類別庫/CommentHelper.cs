using DBSource;
using PostAndCommentSource.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbayProject.ORM;

namespace PostAndCommentSource
{
    public  class CommentHelper
    {
        /// <summary>發布留言 </summary>
        /// <param name="txtComment"></param>
        /// <param name="userID"></param>
        /// <param name="postID"></param>
        /// <returns></returns>
        public static bool addComment(string txtComment, string userID, int postID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@"  INSERT INTO CommentTable
                (
                          postID
                          ,comment
                          ,userID
                          ,createDate
                )
                          VALUES 
                (
                              @postID
                              ,@comment
                              ,@userID
                              ,@createDate
                )
                ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@postID", postID));
            list.Add(new SqlParameter("@comment", txtComment));
            list.Add(new SqlParameter("@userID", userID));
            list.Add(new SqlParameter("@createDate", DateTime.Now));
            try
            {
                int effectRows = DBHelper.ModifyData(connStr, dbCommand, list);

                if (effectRows == 1)
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

        /// <summary>
        /// 在page_unload的時候把VIewer+1
        /// </summary>
        /// <param name="postID"></param>
        /// <param name="tempcountOfViewers"></param>
        /// <returns></returns>
        public static bool UpdateViewers(int postID, int tempcountOfViewers)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@"
                     UPDATE PostTable
                    SET
                               countOfViewers           =   @countOfViewers 
                    WHERE
                        postID = @postID
                     ";
            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@countOfViewers", tempcountOfViewers + 1));
            paramList.Add(new SqlParameter("@postID", postID));

            try
            {
                int effectRows = DBHelper.ModifyData(connStr, dbCommand, paramList);

                if (effectRows == 1)
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
    

        /// <summary>取得回覆及回覆者的使用者姓名</summary>
        /// <param name="postID"></param>
        /// <returns></returns>
        public static List<CommentModel> GetCommentAndUserName(int postID)
        {
            try
            {
                using (ContextModel context1 = new ContextModel())
                {
                    var query =
                        (from item in context1.CommentTables
                         join UserInfo in context1.UserTables
                          on item.userID equals UserInfo.userID
                         where item.postID == postID
                         select new CommentModel()
                         {
                             commentID = item.commentID,
                             postID = item.postID,
                             comment = item.comment,
                             userID = item.userID,
                             createDate = item.createDate,
                             userName = UserInfo.userName,
                         }).ToList();
                    return query;
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
