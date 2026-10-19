using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using RP123.PTApp.Data;
using RP123.PTApp.Models;

namespace RP123.Pages.PTApp.Customers
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly PTAppDbContext _db;

        public DeleteModel(PTAppDbContext db) => _db = db;

        [BindProperty]
        public Customer Customer { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var c = await _db.Customers.FindAsync(id);
            if (c == null) return NotFound();
            Customer = c;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var c = await _db.Customers.FindAsync(Customer.Id);
            if (c != null)
            {
                _db.Customers.Remove(c);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage("Index");
        }
    }
}
