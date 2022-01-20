using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JavaTechPages.Models
{
    public class Shipping
    {
        [Key]
        [Display(Name ="Shipping Code")]
        public int Id { get; set; }
        [Display(Name ="Create Date")]
        public DateTime dateTime { get; set; }
        [Display(Name ="User ID")]
        public string UserId { get; set; }
        [Display(Name ="User")]
        public string UserName { get; set; }
        [Display(Name ="Status")]
        public bool Finished { get; set; }
        public Shipping()
        {

        }
        public Shipping(string userId,string userName)
        {
            this.dateTime = DateTime.Today;
            UserId = userId;
            Finished = false;
            UserName= userName;
            this.ShippingProduct = new HashSet<ShippingProduct>();
        }

        
        public ICollection<ShippingProduct> ShippingProduct { get; set; }

        
    }
}
