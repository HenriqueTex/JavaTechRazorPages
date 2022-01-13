using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaTechPages.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        public string? ImageName { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        
        public virtual Product? Product { get; set; }
    }
}
