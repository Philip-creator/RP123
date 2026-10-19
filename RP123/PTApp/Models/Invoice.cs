using System.ComponentModel.DataAnnotations;

namespace RP123.PTApp.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;

        public DateTime? DueDate { get; set; }

        public decimal Amount { get; set; }

        public decimal PaidAmount { get; set; }

        public string? Status { get; set; }
    }
}
