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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            //LEMBRAR DE ADICIONAR VERIFICAÇÂO SE A REMESSA PODE SER EXCLUIDA, DE ACORDO COM A SAIDA DOS PRODUTOS QUE CHEGARAM NELA
            if (id == null)
            {
                return NotFound();
            }

            Shipping = await _context.Shippings.FindAsync(id);

            if (Shipping != null)
            {
                _context.Shippings.Remove(Shipping);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
