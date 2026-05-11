using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Path_Green.web.Data;
using Path_Green.web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Path_Green.web.Pages.Orders
{
    [Authorize(Roles = "Student,Admin")]
    public class CreateStep2Model : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateStep2Model(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; set; } = new();

        public Dictionary<string, Dictionary<string, List<Product>>> GroupedProducts { get; set; } = new();

        [BindProperty]
        public List<int> SelectedProductIDs { get; set; } = new();

        // Carry forward data from Step 1
        [BindProperty] public string? FirstName { get; set; }
        [BindProperty] public string ?LastName { get; set; }
        [BindProperty] public string ?GradeLevel { get; set; }
        [BindProperty] public string ?Ethnicity { get; set; }
        [BindProperty] public string ?HairType { get; set; }
        [BindProperty] public string ?HairLength { get; set; }
        [BindProperty] public string ?SkinType { get; set; }
        [BindProperty] public string ?Allergies { get; set; }
        [BindProperty] public string ?Notes { get; set; }

        public async Task OnGetAsync(
            string ?FirstName,
            string ?LastName,
            string ?GradeLevel,
            string ?Ethnicity,
            string ?HairType,
            string ?HairLength,
            string ?SkinType,
            string ?Allergies,
            string ?Notes
        )
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.GradeLevel = GradeLevel;
            this.Ethnicity = Ethnicity;
            this.HairType = HairType;
            this.HairLength = HairLength;
            this.SkinType = SkinType;
            this.Allergies = Allergies;
            this.Notes = Notes;

            Products = await _context.Products
                .Include(p => p.Inventory)
                .ToListAsync();

            GroupedProducts = Products
                .GroupBy(p => p.Category ?? "Other")
                .ToDictionary(
                    g => g.Key,
                    g => g.GroupBy(p => p.SubCategory ?? "Basic Items")
                          .ToDictionary(subGroup => subGroup.Key, subGroup => subGroup.ToList())
                );
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var order = new Order
            {
                OrderDate = DateTime.Now,
                OrderStatusID = 1,

                FirstName = FirstName,
                LastName = LastName,
                GradeLevel = GradeLevel,
                Ethnicity = Ethnicity,
                HairType = HairType,
                HairLength = HairLength,
                SkinType = SkinType,
                Allergies = Allergies,
                Notes = Notes
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var productId in SelectedProductIDs)
            {
                var item = new OrderItem
                {
                    OrderID = order.OrderID,
                    ProductID = productId,
                    Quantity = 1
                };

                _context.OrderItems.Add(item);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/Orders/Success", new
            {
                orderId = order.OrderID
            });
        }
    }
}