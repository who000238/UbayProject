namespace UbayProject.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MainCategoryTable")]
    public partial class MainCategoryTable
    {
        [Key]
        public int mainCategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string mainCategoryName { get; set; }

        public DateTime createDate { get; set; }
    }
}
