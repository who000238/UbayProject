using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UbayProject.ORM;
using DBSource;

namespace UbayProject
{
    public partial class TySerach : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var dt = 取得熱門貼文();
                this.GridView1.DataSource = dt;
                this.GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return;
            }
        }

        protected void btnASP_Click(object sender, EventArgs e)
        {
            string txtASP_input = this.txtASP_input.Text;
            string txtHTML_input = this.txtHTML_input.Value;
            var obj = 搜尋貼文EF(txtASP_input);
            if (obj != null)
            {
                this.GridView1.Visible = false;
                this.GridView2.DataSource = obj;
                this.GridView2.DataBind();
            }
            //var dt = 搜尋貼文Row(txtASP_input);
            //this.lbl.Text = dt["postTitle"] as string;
            //var dt = 搜尋貼文(txtASP_input);
            //if (dt != null)
            //{
            //    this.GridView1.Visible = false;
            //    this.GridView2.DataSource = dt;
            //    this.GridView2.DataBind();
            //}
            //else
            //{
            //    Response.Write("<script>alert('查無貼文')</script>");
            //}
        }
        //測試失敗 0828 04:55 測試成功 
        public static Object 搜尋貼文EF(string Input_txt) //與subsubmaster 取得貼文及UserNameEF版相同
        {
            try
            {
                using(ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.PostTables
                         join UserInfo in context.UserTables
                          on item.userID equals UserInfo.userID
                         //where SqlMethods.Like(item.postTitle, $"%{Input_txt}%")
                         where item.postTitle.Contains(Input_txt)
                         select new
                         {
                             UserInfo.userID,
                             UserInfo.userName,
                             UserInfo.account,
                             UserInfo.pwd,
                             UserInfo.userLevel,
                             UserInfo.sex,
                             UserInfo.email,
                             UserInfo.birthday,
                             UserInfo.photoURL,
                             UserInfo.intro,
                             UserInfo.blackList,
                             item.createDate,
                             item.postTitle,
                             item.postID,
                             item.countOfLikes,
                             item.countOfUnlikes,
                             item.countOfViewers,
                             item.subCategoryID,
                             item.postText
                         });
                 
                    var obj = query.ToList();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        public static DataTable 搜尋貼文(string Input_txt)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@"SELECT * FROM PostTable 
                    JOIN UserTable ON PostTable.userID = UserTable.userID
                    WHERE postTitle Like '%{Input_txt}%' ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@Input_txt", Input_txt));
            try
            {
                return DBHelper.ReadDataTable(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        //public static DataRow 搜尋貼文Row(string Input_txt)
        //{
        //    string connStr = 資料庫相關.取得連線字串();
        //    string dbCommand =
        //        $@"SELECT * FROM PostTable WHERE postTitle Like '%{Input_txt}%' ";
        //    List<SqlParameter> list = new List<SqlParameter>();
        //    list.Add(new SqlParameter("@Input_txt", Input_txt));
        //    try
        //    {
        //        return 資料庫相關.查詢單筆資料(connStr, dbCommand, list);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteLog(ex);
        //        return null;
        //    }
        //}  //測試版本 隨後可刪除

        public static DataTable 取得熱門貼文()
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" 
                SELECT TOP (5) [postID]
                      ,[postTitle]
                      ,[countOfLikes]
                      ,[countOfUnlikes]
                      ,[countOfViewers]
                      ,[PostTable].[userID]
                      ,[subCategoryID]
                      ,[PostTable].[createDate]
                      ,[postText]
                      ,[userName]
                  FROM [UBayProject].[dbo].[PostTable]
                   INNER JOIN UserTable ON PostTable.userID = UserTable.userID
                  ORDER BY countOfViewers DESC
                ";
            List<SqlParameter> list = new List<SqlParameter>();

            try
            {
                return DBHelper.ReadDataTable(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

       
    }
}