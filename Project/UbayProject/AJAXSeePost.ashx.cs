using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UbayProject.ORM;
using DBSource;
using PostAndCommentSource;

namespace UbayProject
{
    /// <summary>承接SeePost頁面有使用者回覆新的留言時即時顯示該使用者的留言</summary>
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
                Object commentList = CommentHelper.GetCommentAndUserName(intPostID);
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
    
    }
}