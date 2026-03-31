using ETicketing.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ETicketing.API.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<LedgerEntry> LedgerEntries => Set<LedgerEntry>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(x => x.Name).IsRequired().HasMaxLength(50);
                entity.Property(x => x.Price).HasColumnType("decimal(18,2)");
                entity.Property(x => x.RowVersion).IsRowVersion();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
                entity.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");
                entity.Property(x => x.Status).HasMaxLength(20);
                entity.Property(x => x.PaymentMethod).HasMaxLength(20);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(x => x.Amount).HasColumnType("decimal(18,2)");
                entity.Property(x => x.TransactionReference).HasMaxLength(100);
                entity.Property(x => x.Status).HasMaxLength(20);
                entity.Property(x => x.PaymentMethod).HasMaxLength(20);
            });

            modelBuilder.Entity<LedgerEntry>(entity =>
            {
                entity.Property(x => x.Amount).HasColumnType("decimal(18,2)");
                entity.Property(x => x.EntryType).HasMaxLength(10);
                entity.Property(x => x.AccountName).HasMaxLength(100);
            });

            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { Id=1,Name="Gold",Price=100,TotalQuota=100,RemainingQuota=100,IsActive=true,CreatedAt=DateTime.UtcNow },
                new Ticket { Id=2,Name="Premium",Price=200,TotalQuota=100,RemainingQuota=100,IsActive=true,CreatedAt=DateTime.UtcNow },
                new Ticket { Id=3,Name="VIP",Price=500,TotalQuota=100,RemainingQuota=100,IsActive=true,CreatedAt=DateTime.UtcNow }
            );
        }
    }
}
