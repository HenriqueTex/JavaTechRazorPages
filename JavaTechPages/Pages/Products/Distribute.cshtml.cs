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

namespace JavaTechPages.Pages.Products
{
    public class DistributeModel : PageModel
    {
        private readonly JavaTechPages.Data.ApplicationDbContext _context;

        public DistributeModel(JavaTechPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
        ProductUser = new ProductUser();
        ProductUser.ProductId = _context.Products.FirstOrDefault(s => s.Id == id).Id;
            return Page();
        }

        [BindProperty]
        public ProductUser ProductUser { get; set; }
       
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var product = _context.Products.FirstOrDefault(s=>s.Id==ProductUser.ProductId);
            if (product.Quantity<ProductUser.Quantity)
            {
                ModelState.AddModelError("ProductUser.Quantity", "Quantity not available in stock");
            }
            if (ProductUser.Quantity<=0)
            {
                ModelState.AddModelError("ProductUser.Quantity", "Enter a quantity greater than 0");
            }
            if (!ModelState.IsValid)
            {
                //return RedirectToPage("./Distribute", new { id = product.Id });
                ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
                return Page();
            }
            var existProductUser = _context.productUsers.FirstOrDefault(s => s.ProductId == ProductUser.ProductId && s.UserId == ProductUser.UserId);
            if (existProductUser != null)
            {
                existProductUser.Quantity += ProductUser.Quantity;
                product.Quantity -= ProductUser.Quantity;
                _context.SaveChanges();
                return RedirectToPage("./Index");
            }
            ProductUser newProductUser = new ProductUser();
            newProductUser.ProductId = ProductUser.ProductId;
            newProductUser.UserId = ProductUser.UserId;
            newProductUser.Quantity = ProductUser.Quantity;
            product.Quantity -= ProductUser.Quantity;
            _context.productUsers.Add(ProductUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
