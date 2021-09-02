using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostAndCommentSource.Model
{
    public class PostModel
    {
        public string userName { get; set; }
        public int postID { get; set; }
        public string postTitle { get; set; }
        public int countOfLikes { get; set; }
        public int countOfUnlikes { get; set; }
        public int countOfViewers { get; set; }
        public Guid userID { get; set; }
        public int subCategoryID { get; set; }
        public DateTime createDate { get; set; }
        public string postText { get; set; }

    }
}
