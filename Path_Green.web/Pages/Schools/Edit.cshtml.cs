using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Path_Green.web.Data;
using Path_Green.web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Path_Green.web.Pages.Schools
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly Path_Green.web.Data.ApplicationDbContext _context;

        public EditModel(Path_Green.web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public School School { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var school =  await _context.Schools.FirstOrDefaultAsync(m => m.SchoolID == id);
            if (school == null)
            {
                return NotFound();
            }
            School = school;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(School).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolExists(School.SchoolID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SchoolExists(int id)
        {
            return _context.Schools.Any(e => e.SchoolID == id);
        }
    }
}
