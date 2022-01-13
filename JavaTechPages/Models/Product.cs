using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JavaTechPages.Models
{
    public class Product
    {
        public Product()
        {
            Quantity = 0;
            Excluded = 0;
        }

        [Key]
        public int Id { get; set; }
        
        [DisplayName("Produto")]
        [Required,MinLength(3)]
        public string? Name { get; set; }
        
        [DisplayName("Quantidade")]
        public int? Quantity { get; set; }

        public DateTime RegisterDate { get; set; }

        public int Excluded { get; set; }
        public int ProductImageId { get; set; }
        
        public virtual ProductImage? ProductImage { get; set; }
        
    }
}
