using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using RP123.PTApp.Data;
using RP123.PTApp.Models;

namespace RP123.Pages.PTApp.Customers
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly PTAppDbContext _db;

        public CreateModel(PTAppDbContext db) => _db = db;

        [BindProperty]
        public Customer Customer { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _db.Customers.Add(Customer);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
