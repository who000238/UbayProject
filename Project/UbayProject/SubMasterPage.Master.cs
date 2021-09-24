using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using UbayProject.ORM;
using AccountSource;
using DBSource;

namespace UbayProject
{
    public partial class SubPageMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //檢查登入
            if (this.Session["UserLoginInfo"] != null)
            {
                this.linkLogout.Visible = true;
                this.a_Login.Visible = false;
                //this.postArea.Visible = true;
                UserModel currentUser = UserInfoHelper.GetCurrentUser();
                if (currentUser.userLevel == "0")
                {
                    this.AddMainCategoryArea.Visible = true;
                    this.AddSubCategoryArea.Visible = true;
                }
            }
            else
            {
                this.linkLogout.Visible = false;
                this.a_Login.Visible = true;
                //this.postArea.Visible = false;
            }

            //取得mainCategoryID並轉成INT
            string tempQuery = Request.QueryString["mainCategoryID"];
            int tempCatID = Convert.ToInt32(tempQuery);
            using (ContextModel context = new ContextModel())
            {
                //產生母版連結
                var query =
                      (from item in context.MainCategoryTables
                       select item);
                foreach (var item in query)
                {
                    HyperLink link = new HyperLink();
                    this.BoardLink.Controls.Add(link);
                    link.Text = item.mainCategoryName + "</br>";
                    link.NavigateUrl = $"SubPage/MainCategory.aspx?mainCategoryID={item.mainCategoryID}";
                }

                //產生子版連結
                var query2 =
                      (from item in context.SubCategoryTables
                       where item.mainCategoryID == tempCatID
                       select item);
                foreach (var item in query2)
                {
                    HyperLink link = new HyperLink();
                    this.ContentPlaceHolder1.Controls.Add(link);
                    link.ImageUrl = $"Pics/{item.subCategoryName}.jpg";
                    //link.ImageHeight = 80;                                                                                                                    //調整超連結圖片的大小
                    //link.ImageWidth = 480;
                    link.Text = item.subCategoryName + "</br>";
                    link.NavigateUrl = $"/SubPage/SubCategory.aspx?mainCategoryID={item.mainCategoryID}&subCategoryID={item.subCategoryID}";
                }

            }

           

        }
        protected void linkLogout_Click(object sender, EventArgs e)
        {
            UserInfoHelper.Logout();
            Response.Redirect("/MainPage.aspx");
        }


     
        private int GetCurrentPage()
        {
            string pageText = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(pageText))
            {
                return 1;
            }
            int intPage;
            if (!int.TryParse(pageText,out intPage))
            {
                return 1;
            }

            if(intPage <= 0)
            {
                return 1;
            }

            return intPage;
        }

        private DataTable GetPagedDataTable(DataTable dt)
        {
            DataTable dtPaged = (dt.Rows.Count==0)?dt.Clone() :
            dt.Copy();

            foreach(DataRow dr in dt.Rows)
            {
                var drNew = dtPaged.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    drNew[dc.ColumnName] = dr[dc];
                }

                dtPaged.Rows.Add(drNew);
            }
            return dtPaged;
        }

        protected void btnAddMainCategory_Click(object sender, EventArgs e)
        {
            string NewMainCategoryName = this.AddMainCategoryName.Text;
            if (!string.IsNullOrWhiteSpace(NewMainCategoryName))
            {
                addMainCategory(NewMainCategoryName);
            }
            Response.Write("<script>alert('新增分類成功');location.href='/MainPage.aspx'</script>");
        }

        public static void addMainCategory(string MainCategoryName)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@"
                    INSERT INTO MainCategoryTable
                        (
                        mainCategoryName,
                        createDate
                        )
                        VALUES
                        (
                        @MainCategoryName
                        ,GETDATE()
                        )
                ";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@MainCategoryName", MainCategoryName));
            try
            {
                int effectRows = DBHelper.ModifyData(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }

        protected void btnAddSubCategory_Click(object sender, EventArgs e)
        {
            string MainCategoryName = this.AddSubCateUnderMainName.Text;
            var dr = getMainCategoryIDByMainCategoryName(MainCategoryName);
            int MainCategoryID = Convert.ToInt32(dr["mainCategoryID"]);
            string NewSubCategoryName = this.AddSubCategoryName.Text;

            if(!string.IsNullOrEmpty(MainCategoryName)&&
                !string.IsNullOrEmpty(NewSubCategoryName))
            {
                addSubCategory(NewSubCategoryName, MainCategoryID);
                Response.Write("<script>alert('新增分類成功');location.href='/MainPage.aspx'</script>");

            }
        }

        public static void addSubCategory(string SubCategoryName, int mainCategoryID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@"
                    INSERT INTO SubCategoryTable
                        (
                        subCategoryName,
                        createDate,
                        mainCategoryID
                        )
                        VALUES
                        (
                        @MainCategoryName
                        ,GETDATE()
                        ,0
                        ,0
                        ,@mainCategoryID
                        )
                ";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@MainCategoryName", SubCategoryName));
            list.Add(new SqlParameter("@mainCategoryID", mainCategoryID));

            try
            {
                int effectRows = DBHelper.ModifyData(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }

        public static DataRow getMainCategoryIDByMainCategoryName(string MainCategoryName)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                    @" SELECT 
                        [mainCategoryID]
                        FROM [MainCategoryTable]
                        WHERE [mainCategoryName] = @MainCategoryName
                     ";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@MainCategoryName", MainCategoryName));
            try
            {
   
                return DBHelper.ReadDataRow(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        #region 子分類用圖片上傳區
        public string GetSaveFoldPath()
        {
            return Server.MapPath("~/Pics");
        }

        protected void btnPicUP_Click(object sender, EventArgs e)
        {
            string newCatePicName = this.PicNameInp.Text+".jpg";
            string PicPath = System.IO.Path.Combine(this.GetSaveFoldPath(), newCatePicName);
            this.PicUP.SaveAs(PicPath);
            this.ltMsg.Text = "上傳成功";
        }
        //public string setNewCatePicName(FileUpload fileUpload)
        //{
        //    string newCatePicName = this.PicNameInp.Text;
        //    return newCatePicName;
        //}
        #endregion
    }

}
