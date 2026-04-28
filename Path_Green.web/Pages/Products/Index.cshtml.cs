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

        public IList<Product> Products { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Products = await _context.Products.ToListAsync();
        }
        public async Task<IActionResult> OnPostAddToOrderAsync(int ProductID, int Quantity)
        {
            if (Quantity <= 0)
            {
                return RedirectToPage();
            }

            // Get a real user from AspNetUsers for now
            var userId = await _context.Users
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage();
            }

            // Get product
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductID == ProductID);

            if (product == null)
            {
                return RedirectToPage();
            }

            // Get inventory
            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(i => i.ProductID == ProductID);

            if (inventory == null)
            {
                return RedirectToPage();
            }

            // Check stock
            if (inventory.QuantityOnHand < Quantity)
            {
                ModelState.AddModelError("", "Not enough stock available.");

                Products = await _context.Products
                    .Include(product => product.Inventory)
                    .ToListAsync();

                return Page();
            }

            // Get or create Pending status
            var status = await _context.OrderStatuses
                .FirstOrDefaultAsync(s => s.StatusName == "Pending");

            if (status == null)
            {
                status = new OrderStatus
                {
                    StatusName = "Pending",
                    Description = "Order submitted and waiting for review"
                };

                _context.OrderStatuses.Add(status);
                await _context.SaveChangesAsync();
            }

            // Create order
            var order = new Order
            {
                UserID = userId,
                OrderStatusID = status.OrderStatusID,
                OrderDate = DateTime.Now,
                TotalAmount = product.UnitPrice * Quantity
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Create order item
            var orderItem = new OrderItem
            {
                OrderID = order.OrderID,
                ProductID = product.ProductID,
                Quantity = Quantity,
                UnitPrice = product.UnitPrice,
                LineTotal = product.UnitPrice * Quantity
            };

            _context.OrderItems.Add(orderItem);

            // Deduct inventory
            inventory.QuantityOnHand -= Quantity;
            inventory.LastUpdated = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
    }
