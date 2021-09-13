using DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UbayProject.ORM;

namespace UbayProject.SubPage
{
    public partial class SubCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string SubCateIDtxt = Request.QueryString["subCategoryID"];
            int SubCateID = Convert.ToInt32(SubCateIDtxt);
            var list = GetCategoryTableInfo(SubCateID);
            this.Repeater1.DataSource = list;
            this.Repeater1.DataBind();
            this.lblSubCateName.Text = list[0].SubCategoryName.ToString();
        }
        public static List<CategoryModel> GetCategoryTableInfo(int SubCateID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.SubCategoryTables
                         join MainCateInfo in context.MainCategoryTables
                         on item.mainCategoryID equals MainCateInfo.mainCategoryID
                         where item.subCategoryID == SubCateID
                         select new CategoryModel
                         {
                             MainCategoryName = MainCateInfo.mainCategoryName,
                             SubCategoryName = item.subCategoryName,
                             MainCategoryID = item.mainCategoryID,
                             SubCategoryID = item.subCategoryID
                         }).Take(1).ToList();

                    return query;
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