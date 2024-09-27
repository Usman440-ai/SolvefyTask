using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SolvefyTask.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = "";
        [MaxLength(100)]
        public string Brand { get; set; } = "";
        [MaxLength(100)]
        public string Cateogory { get; set; } = "";
        [Precision(16,2)]
        public decimal Price { get; set; } 
        public string Descprition { get; set; } = "";
        [MaxLength(100)]
        public string ImageFileName { get; set; } = "";
        public DateTime CreatedAt { get; set; } 
        
    }
}
