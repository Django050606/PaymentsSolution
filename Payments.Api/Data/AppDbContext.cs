using Microsoft.EntityFrameworkCore;
using Payments.Api.Models;

namespace Payments.Api.Data
{
    // [cite: 72, 74]
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)"); // 18 digits total, 2 after the decimal point
        }
    }


    
}