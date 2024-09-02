namespace keanulapidCaseStudy.Models
{
    public class PerishableProduct : Product
    {
        public DateTime ExpirationDate { get; set; }

        public PerishableProduct(string name, decimal price, int quantity, DateTime expirationDate)
            : base(name, price, quantity)
        {
            ExpirationDate = expirationDate;
        }
    }
}