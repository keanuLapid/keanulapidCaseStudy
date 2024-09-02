using keanulapidCaseStudy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace keanulapidCaseStudy.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private const string CartSessionKey = "ShoppingCart";

        
        public List<Product> ShoppingCart
        {
            get
            {
                var kariton = HttpContext.Session.GetString(CartSessionKey);
                return kariton != null ? JsonConvert.DeserializeObject<List<Product>>(kariton) ?? new List<Product>() : new List<Product>();
            }
            set
            {
                HttpContext.Session.SetString(CartSessionKey, JsonConvert.SerializeObject(value));
            }
        }

        [BindProperty]
        public string? ProductName { get; set; }

        [BindProperty]
        public decimal ProductPrice { get; set; }

        [BindProperty]
        public int ProductQuantity { get; set; }

        [BindProperty]
        public string? ProductType { get; set; }

        [BindProperty]
        public DateTime? ExpirationDate { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
            var kariton = ShoppingCart; 
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product? newProduct = null;

            if (ProductType == "Perishable")
            {
                if (ExpirationDate.HasValue)
                {
                    newProduct = new PerishableProduct(ProductName ?? string.Empty, ProductPrice, ProductQuantity, ExpirationDate.Value);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Expiration Date is required for perishable products.");
                    return Page();
                }
            }
            else if (ProductType == "NonPerishable")
            {
                newProduct = new NonPerishableProduct(ProductName ?? string.Empty, ProductPrice, ProductQuantity);
            }

            if (newProduct != null)
            {
                var kariton = ShoppingCart;
                kariton.Add(newProduct);
                ShoppingCart = kariton; 
            }

            return RedirectToPage("/Index");
        }

        public decimal CalculateTotalPrice()
        {
            return ShoppingCart.Sum(p => p.GetTotalPrice());
        }
    }
}
