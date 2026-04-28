using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Path_Green.web.Data;
using Path_Green.web.Models;

namespace Path_Green.web.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Order> Orders { get; set; } = new List<Order>();

        public async Task OnGetAsync()
        {
            Orders = await _context.Orders
                .Include(o => o.OrderItems!)
                .ThenInclude(oi => oi.Product!)
                .Include(o => o.OrderStatus)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int OrderID, string StatusName)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems!)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderID == OrderID);

            if (order == null)
                return RedirectToPage();

            var status = await _context.OrderStatuses
                .FirstOrDefaultAsync(s => s.StatusName == StatusName);

            if (status == null)
                return RedirectToPage();

            // ONLY when approving => deduct inventory
            if (StatusName == "Approved")
            {
                foreach (var item in order.OrderItems!)
                {
                    var inventory = await _context.Inventories
                        .FirstOrDefaultAsync(i => i.ProductID == item.ProductID);

                    if (inventory == null)
                        continue;

                    // prevent negative stock
                    if (inventory.QuantityOnHand < item.Quantity)
                    {
                        ModelState.AddModelError("", $"Not enough stock for {item.Product?.ProductName}");

                        Orders = await _context.Orders
                            .Include(o => o.OrderItems!)
                            .ThenInclude(oi => oi.Product)
                            .Include(o => o.OrderStatus)
                            .ToListAsync();

                        return Page();
                    }

                    inventory.QuantityOnHand -= item.Quantity;
                    inventory.LastUpdated = DateTime.Now;
                }
            }

            order.OrderStatusID = status.OrderStatusID;

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}