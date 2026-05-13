using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Path_Green.web.Data;
using Path_Green.web.Models;


namespace Path_Green.web.Pages.Orders
{
    public class CreateStep1Model : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateStep1Model(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string StudentID { get; set; } = string.Empty;

        [BindProperty]
        public string SchoolName { get; set; } = string.Empty;

        [BindProperty]
        public string GradeLevel { get; set; } = string.Empty;

        [BindProperty]
        public string Ethnicity { get; set; } = string.Empty;

        [BindProperty]
        public string HairType { get; set; } = string.Empty;

        [BindProperty]
        public string HairLength { get; set; } = string.Empty;

        [BindProperty]
        public string SkinType { get; set; } = string.Empty;

        [BindProperty]
        public string? Allergies { get; set; }

        [BindProperty]
        public string? Notes { get; set; }

        public IList<School> Schools { get; set; } = new List<School>();

        public async Task OnGetAsync(
             string? StudentID,
             string? SchoolName,
             string? GradeLevel,
             string? Ethnicity,
             string? HairType,
             string? HairLength,
             string? SkinType,
             string? Allergies,
             string? Notes)
        {
             this.StudentID = StudentID ?? "";
             this.SchoolName = SchoolName ?? "";
             this.GradeLevel = GradeLevel ?? "";
             this.Ethnicity = Ethnicity ?? "";
             this.HairType = HairType ?? "";
             this.HairLength = HairLength ?? "";
             this.SkinType = SkinType ?? "";
             this.Allergies = Allergies;
             this.Notes = Notes;

            Schools = await _context.Schools
                 .Where(s => s.IsActive)
                 .OrderBy(s => s.SchoolName)
                 .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Schools = await _context.Schools
                    .Where(s => s.IsActive)
                    .OrderBy(s => s.SchoolName)
                    .ToListAsync();

                return Page();
            }

            return RedirectToPage("/Orders/CreateStep2", new
            {
                StudentID,
                SchoolName,
                GradeLevel,
                Ethnicity,
                HairType,
                HairLength,
                SkinType,
                Allergies,
                Notes
            });
        }
    }
}
