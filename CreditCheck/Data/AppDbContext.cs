namespace CreditCheck.Data
{
    using CreditCheck.Models;
    using CreditCheck.Models.Shared;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>().HasData(
                new Card { Id = new Random().Next(), BankName = "Barclay", CardType = "Visa", AgeLimit = 18, SalaryMin = 30000.00M, APR = 21.80M, CreatedOn = DateTime.Now, CreatedBy = "SYSTEM" },
                new Card { Id = new Random().Next(), BankName = "Vanquis", CardType = "Visa", AgeLimit = 18, SalaryMin = 1.00M, APR = 34.90M, CreatedOn = DateTime.Now, CreatedBy = "SYSTEM" }
            );
        }

        public override int SaveChanges()
        {
            AddAuitInfo();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            AddAuitInfo();
            return (await base.SaveChangesAsync(true, cancellationToken));
        }

        private void AddAuitInfo()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is IEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((IEntity)entry.Entity).CreatedOn = DateTime.UtcNow;
                    ((IEntity)entry.Entity).CreatedBy = "SYSTEM";  // User details could be able to get it from logged in details. Ex.HttpContext.User.Identity.Name;
                }
                else
                {
                    ((IEntity)entry.Entity).UpdatedOn = DateTime.UtcNow;
                    ((IEntity)entry.Entity).UpdatedBy = "SYSTEM"; // User details could be able to get it from logged in details. Ex.HttpContext.User.Identity.Name;
                }
            }
        }
    }
}
