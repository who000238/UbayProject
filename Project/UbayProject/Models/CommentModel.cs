using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UbayProject.Models
{
    public class CommentModel
    {
        public int commentID { get; set; }
        public int postID { get; set; }
        public string comment { get; set; }
        public Guid userID { get; set; }
        public DateTime createDate { get; set; }
        public string userName { get; set; }

    }
}