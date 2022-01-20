#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JavaTechPages.Data;
using JavaTechPages.Models;

namespace JavaTechPages.Pages.ShippingProducts
{
    public class EditModel : PageModel
    {
        private readonly JavaTechPages.Data.ApplicationDbContext _context;

        public EditModel(JavaTechPages.Data.ApplicationDbContext context)
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
           ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
           ViewData["ShippingId"] = new SelectList(_context.Shippings, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ShippingProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingProductExists(ShippingProduct.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ShippingProductExists(int id)
        {
            return _context.ShippingProducts.Any(e => e.Id == id);
        }
    }
}
