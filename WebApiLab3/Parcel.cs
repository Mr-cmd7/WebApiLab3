using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiLab3
{
    public class Parcel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SenderFullName { get; set; }
        [Required]
        public string DepartureCode { get; set; }
        [Required]
        public string DepartureName { get; set; }
        [Required]
        public decimal Weight { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public decimal Cost { get; set; }
    }
}
