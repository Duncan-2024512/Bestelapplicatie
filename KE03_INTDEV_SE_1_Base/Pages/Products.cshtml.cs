using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

public class ProductsModel : PageModel
{
    private readonly MatrixIncDbContext _context;
    public List<CartItem> Cart { get; set; } = new List<CartItem>();

    public IList<Product> Products { get; set; }

    public ProductsModel(MatrixIncDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        Products = await _context.Products
            .Include(p => p.Orders)
            .ToListAsync();
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

        return RedirectToPage();
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