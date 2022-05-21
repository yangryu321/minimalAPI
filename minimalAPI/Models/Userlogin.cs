using System.ComponentModel.DataAnnotations;

namespace minimalAPI.Models
{
    public class Userlogin
    {
        public string Id { get; set; }
         
        [Required (ErrorMessage = "Account name is requried")]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(7, ErrorMessage = "Password must be minimum 7 characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
