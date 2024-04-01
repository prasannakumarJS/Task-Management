using Authorization.Data.Enum;
using Microsoft.EntityFrameworkCore;
using TaskManagement.IDAM.Entities;

namespace Authorization.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=WSEINWL-74BLGK3;initial catalog=UserManagement;Trusted_Connection=Yes;application name=EntityFramework;Encrypt=True;TrustServerCertificate=True;Connection Timeout=600");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();

            modelBuilder.Entity<User>()
            .Property(e => e.Created)
            .HasDefaultValue(DateTime.UtcNow);

            modelBuilder.Entity<User>()
            .Property(e => e.LastUpdated)
            .HasDefaultValue(DateTime.UtcNow);

            base.OnModelCreating(modelBuilder);

            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            // Seed data for Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = Guid.Parse("D06802A2-8193-4E92-86F7-F8DAE5109306"), UserName = "admin", Password = "Pass@123", Email = "admin@example.com", FirstName = "Admin", LastName = "Global", RoleType = RoleType.Admin, Created = DateTime.UtcNow, LastUpdated = DateTime.UtcNow }
            );
        }

    }
}
