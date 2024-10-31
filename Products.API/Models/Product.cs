using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.API.Models
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Field {0} must be maximum {1} characters")]
        [Required(ErrorMessage = "Field {0} is required")]
        public string Name { get; set; } = null!;

        [Range(0, int.MaxValue, ErrorMessage = "Field {0} must be a positive number")]
        [Required(ErrorMessage = "Field {0} is required")]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "Field {0} is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Field {0} must be a positive number")]
        public decimal Price { get; set; }
    }
}
