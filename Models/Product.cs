using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Product
    {
        [Key]
        public int PId { get; set; }


        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }


        public string Image { get; set; }

        [Required]
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Product Type")]
        [Required]
        public int Id { get; set; }
        [ForeignKey("Id")]

        public Category Category { get; set; }
    }
}
