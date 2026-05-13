using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Path_Green.web.Data;
using Path_Green.web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Path_Green.web.Pages.Inventories
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly Path_Green.web.Data.ApplicationDbContext _context;

        public IndexModel(Path_Green.web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Inventory> Inventory { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? StockFilter { get; set; }

        public async Task OnGetAsync()
        {
            var inventoryQuery = _context.Inventories
                .Include(i => i.Product)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                inventoryQuery = inventoryQuery.Where(i =>
                    i.Product != null &&
                    i.Product.ProductName!.Contains(SearchString));
            }

            if (!string.IsNullOrWhiteSpace(StockFilter) && StockFilter != "All")
            {
                if (StockFilter == "LowStock")
                {
                    inventoryQuery = inventoryQuery.Where(i =>
                        i.QuantityOnHand > 0 &&
                        i.QuantityOnHand <= i.ReorderLevel);
                }
                else if (StockFilter == "OutOfStock")
                {
                    inventoryQuery = inventoryQuery.Where(i =>
                        i.QuantityOnHand <= 0);
                }
            }

            Inventory = await inventoryQuery
                .OrderBy(i => i.Product!.ProductName)
                .ToListAsync();
        }
    }
}
