using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace 登入功能的類別庫
{
    public class 登入功能
    {
        public static void 檢查目前是否已登入()
        {

        }

        public static void 取得目前登入者的資訊()
        {

        }

        public static void 嘗試登入()
        {

        }

        public static void 登出()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null;
        }


    }
}
