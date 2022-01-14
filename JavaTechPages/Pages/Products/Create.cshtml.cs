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
    public class CreateModel : PageModel
    {
        private readonly JavaTechPages.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _host;

        public CreateModel(JavaTechPages.Data.ApplicationDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public ProductImage ProductImage { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //Add Custom Validations
            if (_context.Products.Where(p => p.Name == Product.Name).Count() > 0)
            {
                ModelState.AddModelError("Product.Name", "A product with that name already exist");
            }
            if (ProductImage.ImageFile==null)
            {
                ModelState.AddModelError("ProductImage.ImageFile", "Product image is required");
            }
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Criando ProductImage.Name
            string wwwRootPath = _host.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(ProductImage.ImageFile.FileName);
            string extension = Path.GetExtension(ProductImage.ImageFile.FileName);
            
            ProductImage.ImageName = fileName + DateTime.Now.ToString("yymmddffff") + extension;

            //Salvando a Imagem
            string path = Path.Combine(wwwRootPath +"/Image", ProductImage.ImageName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await ProductImage.ImageFile.CopyToAsync(fileStream);
            }
            Product.RegisterDate = DateTime.Now;
            
            _context.ProductImages.Add(ProductImage);
            await _context.SaveChangesAsync();
                        
            Product.ProductImageId = ProductImage.Id;
            _context.Products.Add(Product);            
            await _context.SaveChangesAsync();

            TempData["success"] = "Product Add sucess";
            return RedirectToPage("./Index");
        }
    }
}
