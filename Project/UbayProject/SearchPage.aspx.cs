using PostAndCommentSource;
using PostAndCommentSource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Security.Application;

namespace UbayProject.TryFIleFolder
{
    public partial class SearchPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //檢查登入
            if (this.Session["UserLoginInfo"] != null)
            {
                this.linkLogout.Visible = true;
                this.a_Login.Visible = false;
                this.UserInfoLink.Visible = true;
            }
            else
            {
                this.linkLogout.Visible = false;
                this.a_Login.Visible = true;
                this.UserInfoLink.Visible = false;
            }

            string txtSearch = Request.QueryString["Search"];
            string txtMainCateID = Request.QueryString["mainCateID"];
            int mainCateID = Convert.ToInt32(txtMainCateID);
            string txtSubCateID = Request.QueryString["subCateID"];
            int subCateID = Convert.ToInt32(txtSubCateID);


            if (txtMainCateID != null ||
                txtSubCateID != null)
            {
                List<PostModel> list = PostHelper.searchPost(txtSearch, subCateID);
                if (list.Count > 0)
                {

                    var pagedList = this.GetPagedDataTable(list);

                    this.Repeater1.DataSource = pagedList;
                    this.Repeater1.DataBind();


                    this.ucPagerForSearch.TotalSize = list.Count;
                    this.ucPagerForSearch.CurrentMainCategoryID = mainCateID;
                    this.ucPagerForSearch.CurrentSubCategoryID = subCateID;
                    this.ucPagerForSearch.Bind();
                }
            }
            else
            {
                List<PostModel> list = PostHelper.searchPost(txtSearch);
                if (list.Count > 0)
                {

                    var pagedList = this.GetPagedDataTable(list);

                    this.Repeater1.DataSource = pagedList;
                    this.Repeater1.DataBind();


                    this.ucPagerForSearch.TotalSize = list.Count;
                    this.ucPagerForSearch.txtSearch = txtSearch;
                    this.ucPagerForSearch.Bind();
                }
                else
                {
                    Response.Write("<script>alert('查無貼文')</script>");
                    this.ucPagerForSearch.Visible = false;
                }
            }


        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string txtSearch = Encoder.HtmlEncode(this.SearchBar.Text);
            List<PostModel> list = PostHelper.searchPost(txtSearch);


            if (list.Count > 0)
            {

                var pagedList = this.GetPagedDataTable(list);

                this.Repeater1.DataSource = pagedList;
                this.Repeater1.DataBind();


                this.ucPagerForSearch.TotalSize = list.Count;
                this.ucPagerForSearch.txtSearch = txtSearch;
                this.ucPagerForSearch.Bind();
                Response.Redirect($"SearchPage.aspx?Search={txtSearch}&page=1");
            }
            else
            {
                Response.Write("<script>alert('查無貼文!!')</script>");
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

        protected void linkLogout_Click(object sender, EventArgs e)
        {

        }
    }
}