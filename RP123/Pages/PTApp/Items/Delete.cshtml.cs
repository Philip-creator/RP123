using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using RP123.PTApp.Data;
using RP123.PTApp.Models;

namespace RP123.Pages.PTApp.Items
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly PTAppDbContext _db;

        public DeleteModel(PTAppDbContext db) => _db = db;

        [BindProperty]
        public Item Item { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var i = await _db.Items.FindAsync(id);
            if (i == null) return NotFound();
            Item = i;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var i = await _db.Items.FindAsync(Item.Id);
            if (i != null)
            {
                _db.Items.Remove(i);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage("Index");
        }
    }
}
