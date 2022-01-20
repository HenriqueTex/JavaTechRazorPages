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
    public class DeleteModel : PageModel
    {
        private readonly JavaTechPages.Data.ApplicationDbContext _context;

        public DeleteModel(JavaTechPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ShippingProduct ShippingProduct { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ShippingProduct = await _context.ShippingProducts
                .Include(s => s.Product)
                .Include(s => s.Shipping).FirstOrDefaultAsync(m => m.Id == id);
            
            if (ShippingProduct == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ShippingProduct = await _context.ShippingProducts.FindAsync(id);

            if (ShippingProduct != null)
            {
                _context.ShippingProducts.Remove(ShippingProduct);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
