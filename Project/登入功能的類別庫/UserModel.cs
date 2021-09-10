using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountSource
{
    public class UserModel
    {

        public string userID { get; set; }
        public string userName { get; set; }
        public string account { get; set; }
        public DateTime createDate { get; set; }
        public string userLevel { get; set; }
        public string sex { get; set; }
        public string email { get; set; }
        public DateTime birthday { get; set; }
        public string photoURL { get; set; }
        public string intro { get; set; }
        public string favoritePosts { get; set; }
        public string blackList { get; set; }

        public string OTP { get; set; }

       }
}
