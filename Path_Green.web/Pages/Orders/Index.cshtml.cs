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

        [BindProperty(SupportsGet = true)]
        public string? StatusFilter { get; set; }

        public async Task OnGetAsync()
        {
            var ordersQuery = _context.Orders?
                .Include(o => o.OrderItems!)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.OrderStatus)
                .AsQueryable();

            if (!string.IsNullOrEmpty(StatusFilter) && StatusFilter != "All")
            {
                ordersQuery = ordersQuery!
                    .Where(o => o.OrderStatus != null && o.OrderStatus.StatusName == StatusFilter);
            }

            Orders = await ordersQuery!
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int OrderID, string StatusName)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems!)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.OrderStatus)
                .FirstOrDefaultAsync(o => o.OrderID == OrderID);

            if (order == null)
                return RedirectToPage();

            var status = await _context.OrderStatuses
                .FirstOrDefaultAsync(s => s.StatusName == StatusName);

            if (status == null)
                return RedirectToPage();

            
            // Only deduct inventory if order was NOT already approved
            if (StatusName == "Approved" &&
                order.OrderStatus != null &&
                order.OrderStatus.StatusName != "Approved")
            {
                foreach (var item in order.OrderItems!)
                {
                    var inventory = await _context.Inventories
                        .FirstOrDefaultAsync(i => i.ProductID == item.ProductID);

                    if (inventory == null)
                        continue;

                    if (inventory.QuantityOnHand < item.Quantity)
                    {
                        ModelState.AddModelError("", $"Not enough stock for {item.Product!.ProductName}");

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

        public async Task<IActionResult> OnPostDeleteOrderAsync(int OrderID)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.OrderStatus)
                .FirstOrDefaultAsync(o => o.OrderID == OrderID);

            if (order == null)
            {
                return RedirectToPage();
            }

            if (order.OrderStatus == null)
            {
                return RedirectToPage();
            }

            if (order.OrderStatus.StatusName != "Pending" &&
                order.OrderStatus.StatusName != "Rejected")
            {
                return RedirectToPage();
            }

            if (order.OrderItems != null)
            {
                _context.OrderItems.RemoveRange(order.OrderItems);
            }

            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}