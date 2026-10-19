using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RP123.PTApp.Data;
using RP123.PTApp.Models;

namespace RP123.Pages.PTApp.Customers
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly PTAppDbContext _db;

        public IndexModel(PTAppDbContext db) => _db = db;

        public List<Customer> Customers { get; set; } = new();

        public async Task OnGetAsync()
        {
            Customers = await _db.Customers.OrderBy(c => c.Name).ToListAsync();
        }
    }
}
