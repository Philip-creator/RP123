using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RP123.PTApp.Data;
using RP123.PTApp.Models;

namespace RP123.Pages.PTApp.SalesOrders
{
    public class IndexModel : PageModel
    {
        private readonly PTAppDbContext _db;

        public IndexModel(PTAppDbContext db) => _db = db;

        public List<SalesOrder> Orders { get; set; } = new();

        public async Task OnGetAsync()
        {
            Orders = await _db.SalesOrders.Include(o => o.Customer).OrderByDescending(o => o.OrderDate).ToListAsync();
        }
    }
}
