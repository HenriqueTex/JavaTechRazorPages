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

namespace JavaTechPages.Pages.ShippingProducts
{
    public class IndexModel : PageModel
    {
        private readonly JavaTechPages.Data.ApplicationDbContext _context;

        public IndexModel(JavaTechPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ShippingProduct> ShippingProduct { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ShippingProduct = await _context.ShippingProducts
                .Where(s => s.Id == id)
                .Include(s => s.Product)
                .Include(s => s.Shipping).ToListAsync();
            return Page();
        }
    }
}
