namespace UbayProject.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserTable")]
    public partial class UserTable
    {
        [Key]
        public Guid userID { get; set; }

        [Required]
        [StringLength(20)]
        public string userName { get; set; }

        [Required]
        [StringLength(20)]
        public string account { get; set; }

        [Required]
        [StringLength(20)]
        public string pwd { get; set; }

        public DateTime createDate { get; set; }

        public int userLevel { get; set; }

        [StringLength(1)]
        public string sex { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [Column(TypeName = "date")]
        public DateTime? birthday { get; set; }

        [StringLength(500)]
        public string photoURL { get; set; }

        [StringLength(500)]
        public string intro { get; set; }

        [StringLength(200)]
        public string favoritePosts { get; set; }

        [Required]
        [StringLength(50)]
        public string blackList { get; set; }
    }
}
