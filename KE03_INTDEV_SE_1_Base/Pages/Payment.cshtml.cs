using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class PaymentModel : PageModel
    {
        public decimal Totaal { get; set; }

        public void OnGet(decimal id)
        {
            Totaal = id;
        }
    }
}
