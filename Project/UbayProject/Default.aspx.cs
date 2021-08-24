
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UbayProject.ORM.DBModels;
using 處理資料庫相關的類別庫;

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
                    Page.Controls.Add(link);
                    link.Text = mainCategoryID;
                    link.NavigateUrl = $"SubPage/{mainCategoryID.ToString()}.aspx";
                }
            }


        }


    }
}