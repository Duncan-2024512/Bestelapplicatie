using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace KE03_INTDEV_SE_1_Base.Pages.Shared
{
    public class ProductDetailsModel : PageModel
    {
        private readonly MatrixIncDbContext _context;

        public ProductDetailsModel(MatrixIncDbContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }
        public List<Product> RelevantProducts { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _context.Products.FindAsync(id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPostAddToCart(int productId, int quantity)
        {
            var product = _context.Products.Find(productId);
            if (product == null || quantity <= 0)
            {
                return BadRequest("Invalid product or quantity.");
            }

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            var cartItem = cart.FirstOrDefault(ci => ci.Product.Id == productId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem { Product = product, Quantity = quantity });
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToPage("Cart");
        }
    }

    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}