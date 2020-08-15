namespace CreditCheck.Data
{
    using CreditCheck.Models;
    using Microsoft.EntityFrameworkCore;
    using System;

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
                new Card { Id = new Random().Next(), BankName = "Barclay card", CardType = "Visa", AgeLimit = 18, SalaryMin = 30000.00M, APR = 21.80M, CreatedOn = DateTime.Now, CreatedBy = "SYSTEM" },
                new Card { Id = new Random().Next(), BankName = "Vanquis Card", CardType = "Visa", AgeLimit = 18, SalaryMin = 1.00M, APR = 34.90M, CreatedOn = DateTime.Now, CreatedBy = "SYSTEM" }
            );
        }
    }
}
