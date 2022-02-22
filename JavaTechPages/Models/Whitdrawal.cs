using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JavaTechPages.Models
{
    public class Whitdrawal
    {
        [Key]
        public int  Id{ get; set; }
        public DateTime ExitDate { get; set; }
        [DisplayName("Quantity")]
        public int QuantityExit { get; set; }
        public int ProductUserId { get; set; }
        
        

        public virtual ProductUser? ProductUser { get; set; }
        
    }
}
