using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductRepository _productRepository;

        public IList<Product> Products { get; set; }

        public ProductsModel(ILogger<IndexModel> logger, IProductRepository productRepository)
        {
            _logger = logger;
            this._productRepository = productRepository;
            Products = new List<Product>();
        }

        public void OnGet()
        {
            Products = _productRepository.GetAllProducts().ToList();
            _logger.LogInformation($"getting all {Products.Count} products");
        }
    }
}
