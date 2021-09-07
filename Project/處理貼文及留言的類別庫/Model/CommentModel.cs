using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostAndCommentSource.Model
{
    public class CommentModel
    {
        public int commentID { get; set; }
        public int postID { get; set; }
        public string comment { get; set; }
        public Guid userID { get; set; }
        public DateTime createDate { get; set; }
        public string userName { get; set; }

        public string CreateDateText
        {
            get
            {
                return this.createDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

    }
}
