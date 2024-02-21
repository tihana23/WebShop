using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int ProductId { get; set; }

        [NotMapped]
        public int ProductName { get; set; }
        [NotMapped]
        public int CategoryName { get; set; }

    }
}