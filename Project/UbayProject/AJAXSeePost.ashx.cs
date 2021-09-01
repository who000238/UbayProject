﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UbayProject.ORM;
using DBSource;

namespace UbayProject
{
    /// <summary>
    /// AJAXSeePost 的摘要描述
    /// </summary>
    public class AJAXSeePost : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string actionName = context.Request.QueryString["actionName"];
            string postID = context.Request.QueryString["postID"];
            int intPostID = Convert.ToInt32(postID);
            if (string.IsNullOrEmpty(actionName))               //若沒有指令或為空 回報400代碼
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "text/plain";
                context.Response.Write("ActionName is required");
                context.Response.End();
            }

            if (actionName == "Load")
            {
                //List<CommentTable> commentsSource = GetCommentByEF(intPostID); // 貌似可刪
                Object commentList = GetCommentAndUserName(intPostID);
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(commentList);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
            }
            else
            {
                context.Response.StatusCode = 404;
                context.Response.ContentType = "text/plain";
                context.Response.Write("Error");
                context.Response.End();
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
        public static Object GetCommentAndUserName(int postID)
        {
            try
            {
                using (ContextModel context1 = new ContextModel())
                {
                    //var query =
                    //    (from item in context1.CommentTables
                    //     join UserInfo in context1.UserTables
                    //      on item.userID equals UserInfo.userID
                    //     select new
                    //     {
                    //         item.commentID,
                    //         item.postID,
                    //         item.comment,
                    //         item.userID,
                    //         item.createDate,
                    //         UserInfo.userName,
                    //     }).ToList();

                    var query =
                        (from item in context1.CommentTables
                         join UserInfo in context1.UserTables
                          on item.userID equals UserInfo.userID
                         where item.postID == postID
                         select new
                         {
                             item.commentID,
                             item.postID,
                             item.comment,
                             item.userID,
                             item.createDate,
                             UserInfo.userName,
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