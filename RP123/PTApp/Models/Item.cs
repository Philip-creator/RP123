using System.ComponentModel.DataAnnotations;

namespace RP123.PTApp.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Sku { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public decimal Cost { get; set; }

        public decimal Price { get; set; }

        public bool IsStocked { get; set; } = true;
    }
}
