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
using Microsoft.AspNetCore.Authorization;

namespace Path_Green.web.Pages.Products
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly Path_Green.web.Data.ApplicationDbContext _context;

        public IndexModel(Path_Green.web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var productsQuery = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                productsQuery = productsQuery.Where(p =>
                    p.ProductName!.Contains(SearchString) ||
                    p.Category!.Contains(SearchString));
            }

            Products = await productsQuery
                .OrderBy(p => p.Category)
                .ThenBy(p => p.ProductName)
                .ToListAsync();
        }

    }
}
