using Microsoft.EntityFrameworkCore;
using SULS.App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-AGCLSI5\SQLEXPRESS;Database=SULS;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Submission>(e =>
            {
                e.HasOne(s => s.Problem).WithMany(p => p.Submissions).HasForeignKey(s => s.ProblemId);
                e.HasOne(s => s.User).WithMany(u => u.Submissions).HasForeignKey(s => s.UserId);
            });
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Problem> Problems { get; set; }

        public DbSet<Submission> Submissions { get; set; }
    }
}
