
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using UbayProject.ORM;

namespace UbayProject
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ContextModel context = new ContextModel())
            {
                var query =
                    (from mainCategoryID in context.MainCategoryTables
                     select mainCategoryID.mainCategoryName);
                foreach (var mainCategoryID in query)
                {
                    HyperLink link = new HyperLink();
                    this.BoardLink.Controls.Add(link);
                    link.Text = mainCategoryID + "</br>";
                    link.NavigateUrl = $"SubPage/{mainCategoryID.ToString()}.aspx";
                }
            }
        }
    }
}