using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class OrderProduct
    {

        public int Id { get; set; }
       
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; }
        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Total { get; set; }


        [NotMapped]
        public string ProductName { get; set; }
    }
}
