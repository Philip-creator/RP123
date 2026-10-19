using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RP123.PTApp.Data;
using RP123.PTApp.Models;

namespace RP123.Pages.PTApp.Customers
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly PTAppDbContext _db;

        public EditModel(PTAppDbContext db) => _db = db;

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
            if (!ModelState.IsValid) return Page();

            _db.Attach(Customer).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
