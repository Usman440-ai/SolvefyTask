
using System.ComponentModel.DataAnnotations;

namespace SolvefyTask.Models
{
    public class ProductDTO
    {
        [MaxLength(100), Required]
        public string Name { get; set; } = "";
        [MaxLength(100),Required]
        
        public string Brand { get; set; } = "";
        [MaxLength(100), Required]
        public string Cateogory { get; set; } = "";
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Descprition { get; set; } = "";
        [Required]
        public IFormFile? ImageFile { get; set; }
    }
}
