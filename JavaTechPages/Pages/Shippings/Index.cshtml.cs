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
using Microsoft.AspNetCore.Identity;

namespace JavaTechPages.Pages.Shippings
{
    public class IndexModel : PageModel
    {
        private readonly JavaTechPages.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(JavaTechPages.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Shipping> Shipping { get;set; }
        public List<string> Usernames { get; set; }

        public async Task OnGetAsync()
        {
            Shipping = await _context.Shippings.ToListAsync();
            
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userLoggedIn = await _userManager.GetUserAsync(User);
            var userName = userLoggedIn.UserName;
            Shipping shipping = new Shipping(userLoggedIn.Id,userName);

            _context.Shippings.Add(shipping);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
