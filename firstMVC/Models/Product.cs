using System.ComponentModel.DataAnnotations;

namespace firstMVC.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; } = 0.00m;
        public string Description { get; set; } = null!;
        public String Color { get; set; } = null!;

    }
}
