#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JavaTechPages.Data;
using JavaTechPages.Models;

namespace JavaTechPages.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly JavaTechPages.Data.ApplicationDbContext _context;
        public IList<Product> Product { get; set; }
        public IList<ProductImage> ProductImages { get; set; }

        public IndexModel(JavaTechPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task OnGetAsync()
        {
            Product = await _context.Products.Where(p=>p.Excluded==0).ToListAsync();
            ProductImages = await _context.ProductImages.ToListAsync();

        }
    }
}
