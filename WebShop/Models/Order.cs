using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Total { get; set; }
        public DateTime DateCreated { get; set; }

        public string BillingFirstName { get; set; }
        public string BillingLastName { get; set; }
        public string BillingEmail { get; set; }
        public string BillingPhone { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCountry { get; set; }
        public string BillingZipCode { get; set; }
     


        [ForeignKey("OrderId")]
        public virtual ICollection<OrderProducts> OrderItem { get; set; }
    }
}
