using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Path_Green.web.Pages.Orders
{
    [Authorize(Roles = "Student,Admin")]
    public class SuccessModel : PageModel
    {
        public int OrderId { get; set; }
        public void OnGet(int orderId)
        {
            OrderId = orderId;
        }
    }
}
