namespace HackerNews.Database
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using HackerNews.Database.Tables;

    public partial class HackerNewsDB : DbContext
    {
        public HackerNewsDB()
            : base("name=HackerNewsDB")
        {
        }

        public virtual DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
