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

namespace JavaTechPages.Pages.MyProducts
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
        [BindProperty]
        public ICollection<ProductUser> ProductUser { get;set; }
        public async Task OnGetAsync()
        {
            var userLoggedIn = await _userManager.GetUserAsync(User);
            ProductUser = await _context.productUsers
                .Where(s => s.UserId == userLoggedIn.Id)
                .Include(p => p.Product)
                .Include(p => p.User)
                .ToListAsync();
            foreach(var item in ProductUser)
            {
                item.Product.ProductImage = _context.ProductImages.FirstOrDefault(s => s.Id == item.Product.ProductImageId);
            }
            
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            
            var productUser= _context.productUsers.FirstOrDefault(s=>s.Id==id);

            if (productUser.Quantity<=0)
            {
                ModelState.AddModelError("Whitdrwawal.QuantityExit", "Selected quantity greater than available");
            }
            productUser.Quantity -= 1;

            Whitdrawal whitdrawal = new Whitdrawal();
            whitdrawal.ExitDate = DateTime.Now;
            whitdrawal.QuantityExit = 1;
            whitdrawal.ProductUserId = productUser.Id;
            _context.Whitdrawals.Add(whitdrawal);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
