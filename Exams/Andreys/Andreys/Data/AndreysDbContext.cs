namespace Andreys.Data
{
    using Andreys.Models;
    using Andreys.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class AndreysDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-AGCLSI5\SQLEXPRESS;Database=Andreys;Integrated Security=True;");
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
