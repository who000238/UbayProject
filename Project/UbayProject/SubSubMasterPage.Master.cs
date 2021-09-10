using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using UbayProject.ORM;
using AccountSource;
using DBSource;
using PostAndCommentSource;
using PostAndCommentSource.Model;
using Microsoft.Security.Application;

namespace UbayProject
{
    public partial class SubSubMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //檢查登入
            if (this.Session["UserLoginInfo"] != null)
            {
                this.linkLogout.Visible = true;
                this.a_Login.Visible = false;
                //檢查黑名單
                UserModel currentUser = UserInfoHelper.GetCurrentUser();
                if (currentUser.blackList == "Y")
                    this.postArea.Visible = false;
                else
                    this.postArea.Visible = true;
                if (currentUser.userLevel == "0")
                {
                    this.AddSubCategoryArea.Visible = true;
                }
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
                string subCategoryName = GetSubCategoryName(subCategoryID);
                var list = PostHelper.getPostAndUserName(subCategoryID);

                if (list.Count > 0)
                {
                    var pagedList = this.GetPagedDataTable(list);
                    this.Repeater1.DataSource = pagedList;
                    this.Repeater1.DataBind();

                    this.ucPager.TotalSize = list.Count;
                    this.ucPager.CurrentSubCategoryName = subCategoryName;
                    this.ucPager.CurrentMainCategoryID = tempCatID2;
                    this.ucPager.CurrentSubCategoryID = subCategoryID;
                    this.ucPager.Bind();

                }
                else
                {
                    this.ucPager.Visible = false;
                    Literal ltMsg = new Literal();
                    this.CenterArea.Controls.Add(ltMsg);
                    ltMsg.Text = "查無貼文!!";
                }
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
            string txtTitle = Encoder.HtmlEncode(this.postTitle.Text);
            string txtInner = Encoder.HtmlEncode(this.postInner.Text);
            string tempQuery2 = Request.QueryString["subCategoryID"];
            int tempCatID2 = Convert.ToInt32(tempQuery2);
            if (txtTitle.Length > 50)
            {
                Response.Write("<script>alert('標題過長')</script>");
                return;

            }
            if (txtInner.Length > 4000)
            {
                Response.Write("<script>alert('內文過長')</script>");
                return;

            }

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



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //取得使用者搜尋值&QueryString參數
            int subCategoryID = Convert.ToInt32(Request.QueryString["subCategoryID"]);
            string txtSearch_input = this.SearchBar.Text;
            string subCategoryName = GetSubCategoryName(subCategoryID);
            string tempQuery2 = Request.QueryString["mainCategoryID"];
            int tempCatID2 = Convert.ToInt32(tempQuery2);
            //檢查輸入值
            if (string.IsNullOrWhiteSpace(txtSearch_input) == true)
            {
                Response.Write("<script>alert('搜尋字串不得留空或者輸入空格、請檢查後重新輸入')</script>");
                Response.Write("<script>document.location=document.location</script>");

            }

            var list = PostHelper.searchPost(txtSearch_input, subCategoryID);
            if (list.Count  > 0)
            {

                var pagedList = this.GetPagedDataTable(list);

                this.Repeater1.DataSource = pagedList;
                this.Repeater1.DataBind();


                this.ucPager.TotalSize = list.Count;
                this.ucPager.CurrentSubCategoryName = subCategoryName;
                this.ucPager.CurrentMainCategoryID = tempCatID2;
                this.ucPager.CurrentSubCategoryID = subCategoryID;
                this.ucPager.Bind();
                Response.Redirect($"{this.ucPager.Url}.aspx?Search={txtSearch_input}&mainCateID={tempCatID2}&subCateID={subCategoryID}&page=1");


            }
            else /*if (list.Count ==0)*/
            {
                Response.Write("<script>alert('查無貼文!!')</script>");
            }


   


        }
        public static string GetSubCategoryName(int SubCategoryID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.SubCategoryTables
                         where item.subCategoryID == SubCategoryID
                         select item.subCategoryName).FirstOrDefault();
                    string tempSubCategoryName = query.ToString();
                    return query;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        private int GetCurrentPage()
        {
            string pageText = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(pageText))
                return 1;

            int intPage;
            if (!int.TryParse(pageText, out intPage))
                return 1;

            if (intPage <= 0)
                return 1;

            return intPage;
        }

        private List<PostModel> GetPagedDataTable(List<PostModel> list)
        {
            int startIndex = (this.GetCurrentPage() - 1) * 10;
            return list.Skip(startIndex).Take(10).ToList();
        }

        protected void btnAddSubCategory_Click(object sender, EventArgs e)
        {
            string txtMainCategoryID = Request.QueryString["mainCategoryID"];
            int MainCategoryID = Convert.ToInt32(txtMainCategoryID);
            string NewSubCategoryName = this.AddSubCategoryName.Text;
            if (!string.IsNullOrWhiteSpace(NewSubCategoryName))
            {
                addSubCategory(NewSubCategoryName, MainCategoryID);
            }
            Response.Write("<script>alert('新增分類成功');location.href='/MainPage.aspx'</script>");
        }

        public static void addSubCategory(string SubCategoryName,int mainCategoryID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@"
                    INSERT INTO SubCategoryTable
                        (
                        subCategoryName,
                        createDate,
                        countOfPosts,
                        countOfViewers,
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
    }

}