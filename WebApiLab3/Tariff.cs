using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiLab3
{
    public class Tariff
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DepartureCode { get; set; }
        [Required]
        public string DepartureName { get; set; }
        [Required]
        public decimal PricePerWeightUnit { get; set; }
    }
}
