using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Path_Green.web.Data;
using Path_Green.web.Models;


namespace Path_Green.web.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int TotalOrders { get; set; }
        public int PendingOrders { get; set; }
        public int ApprovedOrders { get; set; }
        public int RejectedOrders { get; set; }
        public int DeliveredOrders { get; set; }

        public int TotalProducts { get; set; }
        public int LowStockProducts { get; set; }
        public int TotalInventoryQuantity { get; set; }

        public IList<Order> RecentOrders { get; set; } = new List<Order>();
        public IList<Inventory> LowStockItems { get; set; } = new List<Inventory>();

        public async Task OnGetAsync()
        {
            TotalOrders = await _context.Orders.CountAsync();

            PendingOrders = await _context.Orders
                .Include(o => o.OrderStatus)
                .CountAsync(o => o.OrderStatus != null && o.OrderStatus.StatusName == "Pending");

            ApprovedOrders = await _context.Orders
                .Include(o => o.OrderStatus)
                .CountAsync(o => o.OrderStatus != null && o.OrderStatus.StatusName == "Approved");

            RejectedOrders = await _context.Orders
                .Include(o => o.OrderStatus)
                .CountAsync(o => o.OrderStatus != null && o.OrderStatus.StatusName == "Rejected");

            DeliveredOrders = await _context.Orders
                .Include(o => o.OrderStatus)
                .CountAsync(o => o.OrderStatus != null && o.OrderStatus.StatusName == "Delivered");

            TotalProducts = await _context.Products.CountAsync();

            LowStockProducts = await _context.Inventories
                .CountAsync(i => i.QuantityOnHand <= i.ReorderLevel);

            TotalInventoryQuantity = await _context.Inventories
                .SumAsync(i => i.QuantityOnHand);

            RecentOrders = await _context.Orders
                .Include(o => o.OrderStatus!)
                .Include(o => o.OrderItems!)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .ToListAsync();

            LowStockItems = await _context.Inventories
                .Include(i => i.Product)
                .Where(i => i.QuantityOnHand <= i.ReorderLevel)
                .OrderBy(i => i.QuantityOnHand)
                .Take(5)
                .ToListAsync();
        }
    }
}