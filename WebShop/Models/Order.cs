using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebShop.Models
{
    public class Order
    {

        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        
        [Required(ErrorMessage = "Total price is required")]
        [Column(TypeName = "decimal(9,2)")]
        
        public decimal Total { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50)]
        public string BillingFirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50)]
        public string BillingLastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        public string BillingEmail { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        [StringLength(50)]
        public string BillingPhone { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [StringLength(50)]
        public string BillingAddress { get; set; }
        [Required(ErrorMessage = "Country is required")]
        [StringLength(50)]
        public string BillingCountry { get; set; }
        [Required(ErrorMessage = "Zip code is required")]
        [StringLength(50)]
        public string BillingZipCode { get; set; }
    
        public string Message { get; set; }


        [ForeignKey("OrderId")]
        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        [NotMapped]
        public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();
    }
}
