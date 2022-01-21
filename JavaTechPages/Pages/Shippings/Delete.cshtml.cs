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

namespace JavaTechPages.Pages.Shippings
{
    public class DeleteModel : PageModel
    {
        private readonly JavaTechPages.Data.ApplicationDbContext _context;

        public DeleteModel(JavaTechPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Shipping Shipping { get; set; }
        [BindProperty]
        public ICollection<ShippingProduct> ShippingProducts { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shipping = await _context.Shippings.FirstOrDefaultAsync(m => m.Id == id);
            ShippingProducts= _context.ShippingProducts.Include(s=>s.Product).Where(s=>s.ShippingId==id).ToList();


            if (Shipping == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            //LEMBRAR DE ADICIONAR VERIFICAÇÂO SE A REMESSA PODE SER EXCLUIDA, DE ACORDO COM A SAIDA DOS PRODUTOS QUE CHEGARAM NELA
            if (id == null)
            {
                return NotFound();
            }
            ShippingProducts = _context.ShippingProducts.Include(s => s.Product).Where(s => s.ShippingId == id).ToList();
            Shipping = await _context.Shippings.FirstOrDefaultAsync(m => m.Id == id);
            foreach (var products in ShippingProducts)
            {
                var product = _context.Products.FirstOrDefault(s => s.Id == products.ProductId);
                if(product.Quantity < products.Quantity)
                {
                    TempData["Error"] += "The product " + product.Name + " já tem saidas registradas";
                }
                product.Quantity -= products.Quantity;
                _context.ShippingProducts.Remove(products);
            }

            if (TempData["Error"] != null)
            {
                return Page();
            }
            
            Shipping = await _context.Shippings.FindAsync(id);

            if (Shipping == null)
            {
                return NotFound();
            }
            _context.Shippings.Remove(Shipping);
            await _context.SaveChangesAsync();
            TempData["success"] = "Shipping and related products excluded sucessfully deleted";

            return RedirectToPage("./Index");
        }
    }
}
