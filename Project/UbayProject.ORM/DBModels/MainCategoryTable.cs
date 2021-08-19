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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int mainCategoryID { get; set; }

        [Required]
        [StringLength(10)]
        public string mainCategoryName { get; set; }

        public DateTime createDate { get; set; }
    }
}
