using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiLab3
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
