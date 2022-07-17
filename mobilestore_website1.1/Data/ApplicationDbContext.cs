using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mobilestore_website1._1.Models;

namespace mobilestore_website1._1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
      
        public DbSet<mobilestore_website1._1.Models.Model>? Model { get; set; }
        public DbSet<mobilestore_website1._1.Models.Order>? Order { get; set; }
        public DbSet<mobilestore_website1._1.Models.Product>? Product { get; set; }
        public DbSet<mobilestore_website1._1.Models.User>? User { get; set; }
    }
}