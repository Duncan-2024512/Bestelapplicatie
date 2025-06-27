using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;

namespace KE03_INTDEV_SE_1_Base.Pages.Shared
{
    public class ProductDetailsModel : PageModel
    {
        private readonly MatrixIncDbContext _context;

        public ProductDetailsModel(MatrixIncDbContext context)
        {
            _context = context;
        }

        public Product? Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _context.Products
                .Include(p => p.Orders)
                .Include(p => p.Parts)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
