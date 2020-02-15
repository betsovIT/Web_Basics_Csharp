using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PandaWebApp.Models;
using PandaWebApp.Models.Enums;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-AGCLSI5\SQLEXPRESS;Database=PANDA;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var roleConverter = new EnumToNumberConverter<IdentityRole, int>();
            var statusConverter = new EnumToNumberConverter<Status, int>();

            modelBuilder.Entity<User>(e =>
            {
                e.Property(x => x.Role).HasConversion(roleConverter);
            });

            modelBuilder.Entity<Package>(e =>
            {
                e.Property(v => v.Status).HasConversion(statusConverter);
                e.HasOne(x => x.Recipient).WithMany(r => r.Packages).HasForeignKey(x => x.RecipientId);
                e.HasData(new Package { Description = "abcdefgh", EstimatedDeliveryDate = DateTime.UtcNow.AddDays(30), ShippingAddress = "Bor N2", Status = Status.Pendin, Weight = 2, RecipientId = "0e69d161-4fe0-430d-b2aa-3347a06fce88" });
                e.HasData(new Package { Description = "12121212", EstimatedDeliveryDate = DateTime.UtcNow.AddDays(30), ShippingAddress = "Bor N2", Status = Status.Shipped, Weight = 2, RecipientId = "0e69d161-4fe0-430d-b2aa-3347a06fce88" });
                e.HasData(new Package { Description = "a34a123", EstimatedDeliveryDate = DateTime.UtcNow.AddDays(30), ShippingAddress = "Bor N2", Status = Status.Delivered, Weight = 2, RecipientId = "0e69d161-4fe0-430d-b2aa-3347a06fce88" });
            });

            modelBuilder.Entity<Receipt>(e =>
            {
                e.HasOne(x => x.Package).WithMany(p => p.Receipts).HasForeignKey(x => x.PackageId);
                e.HasOne(x => x.Recepient).WithMany(r => r.Receipts).HasForeignKey(x => x.RecepientId).OnDelete(DeleteBehavior.NoAction);
            });
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<Package> Packages { get; set; }
    }
}