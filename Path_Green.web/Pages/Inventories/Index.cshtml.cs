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

        public async Task OnGetAsync()
        {
            Inventory = await _context.Inventories
                .Include(i => i.Product)
                .ToListAsync();
        }
    }
}
