using AccountSource;
using PostAndCommentSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UbayProject.ORM;

namespace UbayProject
{
    public partial class TryReapeater : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //檢查登入
            if (this.Session["UserLoginInfo"] != null)
            {
                this.linkLogout.Visible = true;
                this.a_Login.Visible = false;
                this.postArea.Visible = true;
            }
            else
            {
                this.linkLogout.Visible = false;
                this.a_Login.Visible = true;
                this.postArea.Visible = false;
            }
            //取得subCategoryID並轉成INT
            string tempQuery2 = Request.QueryString["mainCategoryID"];
            int tempCatID2 = Convert.ToInt32(tempQuery2);
            using (ContextModel context = new ContextModel())
            {
                //產生子版連結
                var query =
                      (from item in context.SubCategoryTables
                       where item.mainCategoryID == tempCatID2
                       select item);
                foreach (var item in query)
                {
                    HyperLink link = new HyperLink();
                    this.BoardLink.Controls.Add(link);
                    link.Text = item.subCategoryName + "</br>";
                    link.NavigateUrl = $"/SubPage/{item.subCategoryName}.aspx?mainCategoryID={item.mainCategoryID}&subCategoryID={item.subCategoryID}";
                }
            }

            try
            {
                string tempQuery3 = Request.QueryString["subCategoryID"];
                int subCategoryID = Convert.ToInt32(tempQuery3);
                var obj = PostHelper.getPostAndUserName(subCategoryID); //取得貼文
                if(obj != null)
                {
                    //var pagedList =
                }
                this.Repeater1.DataSource = obj;
                this.Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('{ex}')</script>");
            }

        }
        protected void linkLogout_Click(object sender, EventArgs e)
        {
            UserInfoHelper.Logout();
            Response.Redirect("/MainPage.aspx");
        }


        protected void postSubmit_Click(object sender, EventArgs e)
        {
            string txtTitle = this.postTitle.Text;
            string txtInner = this.postInner.Text;
            string tempQuery2 = Request.QueryString["subCategoryID"];
            int tempCatID2 = Convert.ToInt32(tempQuery2);

            if (string.IsNullOrWhiteSpace(txtTitle) ||
                string.IsNullOrWhiteSpace(txtInner))
            {
                Response.Write("<script>alert('標題和內文不得為空')</script>");
                return;
            }
            UserModel currentUser = UserInfoHelper.GetCurrentUser();
            string userID = currentUser.userID;

            PostHelper.createPost(txtTitle, txtInner, userID, tempCatID2);
            this.postTitle.Text = string.Empty;
            this.postInner.Text = string.Empty;
            Response.Write("<script>document.location=document.location</script>");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;

            var data = e.Row.DataItem as PostTable;
            Guid uid = data.userID;


            var userInfo = UserInfoHelper.getUserNameByUserID(uid);


            var lblUserName = row.FindControl("ltlUserName") as Label;
            lblUserName.Text = userInfo.account;
        }

        public void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string tempQuery3 = Request.QueryString["subCategoryID"];
            int subCategoryID = Convert.ToInt32(tempQuery3);
            var obj = PostHelper.getPostAndUserName(subCategoryID);
            //GridView1.PageIndex = e.NewPageIndex;
            //this.GridView1.DataSource = obj;
            //GridView1.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int subCategoryID = Convert.ToInt32(Request.QueryString["subCategoryID"]);
            string txtSearch_input = this.SearchBar.Text;
            if (string.IsNullOrWhiteSpace(txtSearch_input) == true)
            {
                Response.Write("<script>alert('搜尋字串不得留空或者輸入空格、請檢查後重新輸入')</script>");
                Response.Write("<script>document.location=document.location</script>");

            }
            var obj = PostHelper.searchPost(txtSearch_input, subCategoryID);
            if (obj != null)
            {
                //this.GridView1.Visible = false;
                //this.GridView2.DataSource = obj;
                //this.GridView2.DataBind();
            }
        }
        //#region 測試ucPager
        //private int GetCurrentPage()
        //{
        //    string pageText = Request.QueryString["Page"];

        //    if (string.IsNullOrWhiteSpace(pageText))
        //        return 1;

        //    int intPage;
        //    if (!int.TryParse(pageText, out intPage))
        //        return 1;

        //    if (intPage <= 0)
        //        return 1;

        //    return intPage;
        //}

        //private List<Accounting> GetPagedDataTable(List<Accounting> list)
        //{
        //    int startIndex = (this.GetCurrentPage() - 1) * 10;
        //    return list.Skip(startIndex).Take(10).ToList();
        //}

        //private DataTable GetPagedDataTable(DataTable dt)
        //{
        //    int pageSize = this.ucPager2.PageSize;
        //    DataTable dtPaged = dt.Clone();                     //複製 DataTable dt 的結構給 dtPaged
        //    int startIndex = (this.GetCurrentPage() - 1) * pageSize;  //設定分頁控制項分別頁面的起點、終點
        //    int endIndex = (this.GetCurrentPage()) * pageSize;
        //    if (endIndex > dt.Rows.Count)                       //讓最後一頁的終點可以跟資料筆數相同、否則會有超過資料筆數無法讀取的問題 out of index 的錯誤訊息
        //        endIndex = dt.Rows.Count;
        //    for (var i = startIndex; i < endIndex; i++)         // 設定一個for迴圈 起點為第一筆資料 終點為最後一筆資料
        //    {
        //        DataRow dr = dt.Rows[i];                        //dt.Row的第i筆資料放到 dr資料列裡面
        //        var drNew = dtPaged.NewRow();                   //新資料列drNew等同於dtPaged的資料列

        //        foreach (DataColumn dc in dt.Columns)           //
        //        {
        //            drNew[dc.ColumnName] = dr[dc];              //
        //        }

        //        dtPaged.Rows.Add(drNew);                        //
        //    }

        //    return dtPaged;
        //}
        ////#endregion
    }

}