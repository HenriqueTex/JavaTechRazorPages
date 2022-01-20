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
    public class DetailsModel : PageModel
    {
        private readonly JavaTechPages.Data.ApplicationDbContext _context;

        public DetailsModel(JavaTechPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Shipping Shipping { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shipping = await _context.Shippings.FirstOrDefaultAsync(m => m.Id == id);

            if (Shipping == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
