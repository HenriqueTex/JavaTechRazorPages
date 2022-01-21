#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JavaTechPages.Data;
using JavaTechPages.Models;
using Microsoft.EntityFrameworkCore;

namespace JavaTechPages.Pages.ShippingProducts
{
    public class CreateModel : PageModel
    {
        private readonly JavaTechPages.Data.ApplicationDbContext _context;

        public CreateModel(JavaTechPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? id)
        {
            ViewData["ProductId"] = new SelectList(_context.Products.Where(s=>s.Excluded!=1), "Id", "Name");
            ViewData["ShippingId"] = new SelectList(_context.Shippings, "Id", "Id");
            ShippingProducts = _context.ShippingProducts.Include(s=>s.Product).Where(s=>s.ShippingId==id).ToList();
            
            ShippingProduct= new ShippingProduct(); 
            ShippingProduct.ShippingId = id ?? 0;
            
            return Page();
        }

        [BindProperty]
        public ShippingProduct ShippingProduct { get; set; }
        public ICollection<ShippingProduct> ShippingProducts { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAddProductAsync()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var products = _context.ShippingProducts
                .Where(s=>s.ShippingId==ShippingProduct.ShippingId)
                .FirstOrDefault(s=>s.ProductId==ShippingProduct.ProductId);
            
            if (products==null)
            {
                _context.ShippingProducts.Add(ShippingProduct);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Create", new { id = ShippingProduct.ShippingId });
            }
            
            products.Quantity+=ShippingProduct.Quantity;
            await _context.SaveChangesAsync();
            return RedirectToPage("./Create", new {id= ShippingProduct.ShippingId });
        }

        public async Task<IActionResult> OnPostCloseShippingAsync()
        {
            var shipping=_context.Shippings.Include(s=>s.ShippingProduct).FirstOrDefault(s => s.Id == ShippingProduct.ShippingId);
            shipping.Finished = true;
            foreach(var item in shipping.ShippingProduct)
            {
                _context.Products.FirstOrDefault(s => s.Id == item.ProductId).Quantity += item.Quantity;
            }
            
            await _context.SaveChangesAsync();
            return RedirectToPage("../Shippings/Index");
        }
        public async Task<IActionResult> OnPostRemoveProductAsync(int? id)
        {
            var productRemove = _context.ShippingProducts.FirstOrDefault(s => s.Id == id);
            _context.Remove(productRemove);

            await _context.SaveChangesAsync();
            return RedirectToPage("./Create", new { id = ShippingProduct.ShippingId });
        }
    }
}
