namespace keanulapidCaseStudy.Models
{
    public class Product
    {
        public String? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(string? name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
        
        public virtual decimal GetTotalPrice() 
        {
            return Price * Quantity; 
        }
    }
}
