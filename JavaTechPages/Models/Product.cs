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
        [Display(Name ="Product")]
        public int Id { get; set; }
        
        [DisplayName("Product")]
        [Required,MinLength(3)]
        public string? Name { get; set; }
        
        [DisplayName("Quantity")]
        public int? Quantity { get; set; }

        public DateTime RegisterDate { get; set; }

        public int Excluded { get; set; }
        public int ProductImageId { get; set; }
        
        public virtual ProductImage? ProductImage { get; set; }
        
        public virtual List<ShippingProduct>? ShippingProduct { get; set; }

    }
}
