using Microsoft.EntityFrameworkCore;

namespace JobListing.Models
{
    public class JobDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<JobInfo> JobInfos { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=JobDB.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<JobCategory>().ToTable("Category");
            modelBuilder.Entity<JobInfo>().ToTable("Job");
            modelBuilder.Entity<Company>().ToTable("Company");
        }

    }
}
