namespace UbayProject.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommentTable")]
    public partial class CommentTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int commentID { get; set; }

        [Required]
        [StringLength(4000)]
        public string comment { get; set; }

        public Guid userID { get; set; }

        public int postID { get; set; }

        public DateTime createDate { get; set; }
    }
}