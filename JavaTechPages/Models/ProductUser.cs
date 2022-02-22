using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JavaTechPages.Models
{
    public class ProductUser
    {
        [Key]
        public int  Id { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public virtual IdentityUser? User  { get; set; }
        public virtual ICollection<Whitdrawal> Whitdrawals { get; set; }

    }
}
