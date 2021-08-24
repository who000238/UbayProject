namespace UbayProject.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubCategoryTable")]
    public partial class SubCategoryTable
    {
        [Key]
        public int subCategoryID { get; set; }

        public int mainCategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string subCategoryName { get; set; }

        public int? countOfPost { get; set; }

        public int? countOfViewers { get; set; }

        public DateTime? createDate { get; set; }
    }
}
