using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Path_Green.web.Data;
using Path_Green.web.Models;

namespace Path_Green.web.Pages.Inventories
{
    public class CreateModel : PageModel
    {
        private readonly Path_Green.web.Data.ApplicationDbContext _context;

        public CreateModel(Path_Green.web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ProductName");
            return Page();
        }

        [BindProperty]
        public Inventory Inventory { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ProductName");
                return Page();
            }

            _context.Inventories.Add(Inventory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
