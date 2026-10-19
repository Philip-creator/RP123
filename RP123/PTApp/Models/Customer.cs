using System.ComponentModel.DataAnnotations;

namespace RP123.PTApp.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string ShortName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Email { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }

        public decimal CreditLimit { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
