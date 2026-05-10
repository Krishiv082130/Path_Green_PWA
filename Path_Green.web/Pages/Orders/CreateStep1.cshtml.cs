using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Path_Green.web.Pages.Orders
{
    public class CreateStep1Model : PageModel
    {
        [BindProperty]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty]
        public string LastName { get; set; } = string.Empty;

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

        public void OnGet(
             string? FirstName,
             string? LastName,
             string? GradeLevel,
             string? Ethnicity,
             string? HairType,
             string? HairLength,
             string? SkinType,
             string? Allergies,
             string? Notes)
        {
             this.FirstName = FirstName ?? "";
             this.LastName = LastName ?? "";
             this.GradeLevel = GradeLevel ?? "";
             this.Ethnicity = Ethnicity ?? "";
             this.HairType = HairType ?? "";
             this.HairLength = HairLength ?? "";
             this.SkinType = SkinType ?? "";
             this.Allergies = Allergies;
             this.Notes = Notes;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("/Orders/CreateStep2", new
            {
                FirstName,
                LastName,
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
