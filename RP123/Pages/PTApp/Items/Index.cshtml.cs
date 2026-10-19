using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RP123.PTApp.Data;
using RP123.PTApp.Models;

namespace RP123.Pages.PTApp.Items
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly PTAppDbContext _db;

        public IndexModel(PTAppDbContext db) => _db = db;

        public List<Item> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            Items = await _db.Items.OrderBy(i => i.Sku).ToListAsync();
        }
    }
}
