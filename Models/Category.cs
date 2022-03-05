using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Category

    {
        [Key]
        public int Id { get; set; }


        [Required]
        [Display(Name = "Product Type")]
        public string ProductType { get; set; }
    }
}
