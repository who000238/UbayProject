namespace UbayProject.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PostTable")]
    public partial class PostTable
    {
        [Key]
        public int postID { get; set; }

        [Required]
        [StringLength(50)]
        public string postTitle { get; set; }

        public int countOfLikes { get; set; }

        public int countOfUnlikes { get; set; }

        public int countOfViewers { get; set; }

        public Guid userID { get; set; }

        public int subCategoryID { get; set; }

        public DateTime createDate { get; set; }

        [Required]
        [StringLength(4000)]
        public string postText { get; set; }
    }
}
