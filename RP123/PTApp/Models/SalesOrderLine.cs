using System.ComponentModel.DataAnnotations;

namespace RP123.PTApp.Models
{
    public class SalesOrderLine
    {
        public int Id { get; set; }

        [Required]
        public int SalesOrderId { get; set; }

        public SalesOrder? SalesOrder { get; set; }

        [Required]
        public int ItemId { get; set; }

        public Item? Item { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal LineTotal => Quantity * UnitPrice;
    }
}
