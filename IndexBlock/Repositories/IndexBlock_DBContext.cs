using IndexBlock.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace IndexBlock.Repositories
{
    public class IndexBlock_DBContext : DbContext
    {
        public IndexBlock_DBContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<Blocks> Blocks { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new TableNameEntityClassConfiguration());
        }
    }
}
