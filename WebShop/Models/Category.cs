using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models
{
    public class Category
    {
        public int Id{ get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public int Name { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ICollection<ProductCategory> ProductCategory{ get; set; }
    }
}
