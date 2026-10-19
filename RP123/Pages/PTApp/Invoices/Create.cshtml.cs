using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using RP123.PTApp.Data;
using RP123.PTApp.Models;

namespace RP123.Pages.PTApp.Invoices
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly PTAppDbContext _db;
        public CreateModel(PTAppDbContext db) => _db = db;

        [BindProperty]
        public Invoice Invoice { get; set; } = new();

        public List<SelectListItem> CustomerList { get; set; } = new();

        public void OnGet()
        {
            CustomerList = _db.Customers.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _db.Invoices.Add(Invoice);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
