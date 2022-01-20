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

namespace JavaTechPages.Pages.Shippings
{
    public class CreateModel : PageModel
    {
        private readonly JavaTechPages.Data.ApplicationDbContext _context;

        public CreateModel(JavaTechPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Shipping Shipping { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Shippings.Add(Shipping);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
