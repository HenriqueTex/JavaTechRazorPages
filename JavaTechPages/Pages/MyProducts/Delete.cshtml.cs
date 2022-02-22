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

namespace JavaTechPages.Pages.MyProducts
{
    public class DeleteModel : PageModel
    {
        private readonly JavaTechPages.Data.ApplicationDbContext _context;

        public DeleteModel(JavaTechPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductUser ProductUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductUser = await _context.productUsers
                .Include(p => p.Product)
                .Include(p => p.User).FirstOrDefaultAsync(m => m.Id == id);

            if (ProductUser == null)
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

            ProductUser = await _context.productUsers.FindAsync(id);

            if (ProductUser != null)
            {
                _context.productUsers.Remove(ProductUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
