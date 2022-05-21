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
        public string Address { get; set; } = string.Empty;


        [Required(ErrorMessage = "Password is required")]
        [MinLength(7, ErrorMessage = "Password must be minimum 7 characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        [MinLength(7, ErrorMessage = "Password must be minimum 7 characters long")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }
    }

}
