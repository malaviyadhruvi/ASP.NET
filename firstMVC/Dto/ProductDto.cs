namespace firstMVC.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; } = 0.00m;
        
        public String Color { get; set; } = null!;
    }
}
