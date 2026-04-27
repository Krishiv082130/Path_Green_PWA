using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Path_Green.web.Data;
using Path_Green.web.Models;

namespace Path_Green.web.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly Path_Green.web.Data.ApplicationDbContext _context;

        public IndexModel(Path_Green.web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }
        public async Task<IActionResult> OnPostAddToOrderAsync(int ProductID, int Quantity)
        {
            if (Quantity <= 0)
                return RedirectToPage();

            var userId = await _context.Users
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage();
            }

            // User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // if (string.IsNullOrEmpty(userId))
            //  {
            //  return RedirectToPage("/Account/Login");
            // }

            // Ensure status exists
            var status = await _context.OrderStatuses.FirstOrDefaultAsync(s => s.OrderStatusID == 1);

            if (status == null)
            {
                status = new OrderStatus { StatusName = "Pending" };
                _context.OrderStatuses.Add(status);
                await _context.SaveChangesAsync();
            }

            // Create Order
            var order = new Order
            {
                UserID = userId,
                OrderStatusID = status.OrderStatusID,
                OrderDate = DateTime.Now,
                TotalAmount = 0
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Get Product
            var product = await _context.Products.FindAsync(ProductID);
            if (product == null)
                return RedirectToPage();

            // Create OrderItem
            var item = new OrderItem
            {
                OrderID = order.OrderID,
                ProductID = ProductID,
                Quantity = Quantity,
                UnitPrice = product.UnitPrice,
                LineTotal = product.UnitPrice * Quantity
            };

            _context.OrderItems.Add(item);

            // Update total
            order.TotalAmount = item.LineTotal;

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
