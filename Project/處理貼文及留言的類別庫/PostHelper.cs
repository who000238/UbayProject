using DBSource;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostAndCommentSource
{
    public class PostHelper
    {
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
    }
}
