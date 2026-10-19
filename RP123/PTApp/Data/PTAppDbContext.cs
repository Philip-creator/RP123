using Microsoft.EntityFrameworkCore;
using RP123.PTApp.Models;

namespace RP123.PTApp.Data
{
    public class PTAppDbContext : DbContext
    {
        public PTAppDbContext(DbContextOptions<PTAppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderLine> SalesOrderLines { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(eb =>
            {
                eb.HasKey(c => c.Id);
                eb.Property(c => c.Name).IsRequired().HasMaxLength(200);
            });

            modelBuilder.Entity<Item>(eb =>
            {
                eb.HasKey(i => i.Id);
                eb.Property(i => i.Sku).IsRequired().HasMaxLength(50);
                eb.Property(i => i.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<SalesOrder>(eb =>
            {
                eb.HasKey(o => o.Id);
                eb.HasOne(o => o.Customer).WithMany().HasForeignKey(o => o.CustomerId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SalesOrderLine>(eb =>
            {
                eb.HasKey(l => l.Id);
                eb.HasOne(l => l.SalesOrder).WithMany(o => o.Lines).HasForeignKey(l => l.SalesOrderId);
                eb.HasOne(l => l.Item).WithMany().HasForeignKey(l => l.ItemId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Invoice>(eb =>
            {
                eb.HasKey(i => i.Id);
                eb.HasOne(i => i.Customer).WithMany().HasForeignKey(i => i.CustomerId).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
