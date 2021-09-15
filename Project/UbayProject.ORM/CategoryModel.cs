using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbayProject.ORM
{
    public class CategoryModel
    {
        public string MainCategoryName { get; set; }
        public string SubCategoryName { get; set; }

        public int MainCategoryID { get; set; }
        public int SubCategoryID { get; set; }
    }
}
