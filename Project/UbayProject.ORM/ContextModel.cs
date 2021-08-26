using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace UbayProject.ORM
{
    public partial class ContextModel : DbContext
    {
        public ContextModel()
            : base("name=DefaultConnectionString")
        {
        }

        public virtual DbSet<CommentTable> CommentTables { get; set; }
        public virtual DbSet<MainCategoryTable> MainCategoryTables { get; set; }
        public virtual DbSet<PostTable> PostTables { get; set; }
        public virtual DbSet<SubCategoryTable> SubCategoryTables { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UserTable> UserTables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTable>()
                .Property(e => e.account)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.pwd)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.sex)
                .IsFixedLength();

            modelBuilder.Entity<UserTable>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.photoURL)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.favoritePosts)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.blackList)
                .IsUnicode(false);
        }
    }
}
