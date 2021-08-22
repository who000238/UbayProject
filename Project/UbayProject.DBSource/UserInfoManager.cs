using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbayProject.ORM.DBModels;

namespace UbayProject.DBSource
{
    public class UserInfoManager
    {
        public static UserTable GetUserInfoByAccount(string account)
        {
            try
            {
                using(ContextModel context = new ContextModel())
                {
                    var query =
                        from item in context.UserTables
                        where item.account == account
                        select item;

                    var obj = query.FirstOrDefault();
                    return obj;
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
