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
    }
}