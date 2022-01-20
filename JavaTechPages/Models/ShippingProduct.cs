using System.ComponentModel.DataAnnotations;

namespace JavaTechPages.Models
{
    public class ShippingProduct
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int Quantity { get; set; }

        //navigation
        public int ShippingId { get; set; }
        public int ProductId { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Shipping? Shipping { get; set; }
    }
}
