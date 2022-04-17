using System.ComponentModel.DataAnnotations;

namespace minimalAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Address { get; set; } = string.Empty ;
    }
}
