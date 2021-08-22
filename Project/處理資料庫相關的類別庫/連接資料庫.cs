using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 處理資料庫相關的類別庫
{
    class 連接資料庫
    {
        public static string 取得連線字串()
        {
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }
    
    }
}
