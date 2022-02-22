using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JavaTechPages.Models;

namespace JavaTechPages.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<JavaTechPages.Models.Product> Products { get; set; }
        public DbSet<JavaTechPages.Models.ProductImage> ProductImages { get; set; }
        public DbSet<JavaTechPages.Models.Shipping> Shippings { get; set; }
        public DbSet<JavaTechPages.Models.ShippingProduct> ShippingProducts { get; set; }
        public DbSet<JavaTechPages.Models.ProductUser> productUsers { get; set; }
        public DbSet<JavaTechPages.Models.Whitdrawal> Whitdrawals { get; set; }
    }
}