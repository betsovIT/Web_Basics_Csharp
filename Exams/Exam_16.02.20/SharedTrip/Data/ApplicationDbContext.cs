﻿namespace SharedTrip
{
    using Microsoft.EntityFrameworkCore;
    using SharedTrip.Models;

    public class ApplicationDbContext : DbContext
    { 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>(e =>
            {
                e.HasKey(ut => new { ut.UserId, ut.TripId });
                e.HasOne(ut => ut.User).WithMany(u => u.UserTrips).HasForeignKey(ut => ut.UserId);
                e.HasOne(ut => ut.Trip).WithMany(t => t.UserTrips).HasForeignKey(ut => ut.TripId);
            });
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<UserTrip> UsersTrips { get; set; }
    }
}
