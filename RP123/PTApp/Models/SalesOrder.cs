using System.ComponentModel.DataAnnotations;

namespace RP123.PTApp.Models
{
    public class SalesOrder
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public DateTime? ShipDate { get; set; }

        public string? Status { get; set; }

        public decimal TotalAmount { get; set; }

        public List<SalesOrderLine> Lines { get; set; } = new();
    }
}
