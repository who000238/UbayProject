using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbayProject.Auth
{
    public class UserInfoModel
    {
        public Guid ID { get; set; }
        public string Account { get; set; }
        public string PWD { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
