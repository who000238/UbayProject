using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UbayProject.ORM;
using DBSource;
using PostAndCommentSource.Model;

namespace UbayProject.TryFIleFolder
{
    /// <summary>
    /// TryASHX 的摘要描述
    /// </summary>
    public class TryASHX : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string actionName = context.Request.QueryString["actionName"];
            string postID = context.Request.QueryString["postID"];
            if (string.IsNullOrEmpty(actionName))               //若沒有指令或為空 回報400代碼
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "text/plain";
                context.Response.Write("ActionName is required");
                context.Response.End();
            }

            if (actionName == "Load")
            {
                List<CommentTable> commentsSource = GetCommentByEF(Convert.ToInt32(postID));

                List<CommentTable> NewestPostList = commentsSource.Select(obj => new CommentTable()
                {
                    commentID = obj.commentID,
                    postID = obj.postID,
                    comment = obj.comment,
                    userID = obj.userID,
                    createDate = obj.createDate
                    //createDate = DateTime.TryParseExact(obj.createDate, "yyyy-MM-dd HH:mm:ss")

                }).ToList();

                List<CommentModel> NewestPostList2 = GetCommentAndUserName(postID);

                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(NewestPostList2);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public static List<CommentTable> GetCommentByEF(int postID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.CommentTables
                         where item.postID == postID
                         select item);
                    var list = query.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
        public static List<CommentModel> GetCommentAndUserName(string postID)
        {
            try
            {
                using (ContextModel context1 = new ContextModel())
                {
                    var query =
                        (from item in context1.CommentTables
                         join UserInfo in context1.UserTables
                          on item.userID equals UserInfo.userID
                         select new
                         {
                             item.commentID,
                             item.postID,
                             item.comment,
                             item.userID,
                             item.createDate,
                             UserInfo.userName
                         });
                    query.Select(item => new
                    {
                        item.commentID,
                        item.postID,
                        item.comment,
                        item.userID,
                        item.createDate,
                        item.userName
                    });
                    return (List<CommentModel>)query;
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