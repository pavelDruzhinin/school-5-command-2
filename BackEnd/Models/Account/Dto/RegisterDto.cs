using System.ComponentModel.DataAnnotations;
 
namespace ChatsConstructor.WebApi.Dto
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z�-��-�]+$")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z�-��-�]+$")]
        public string MiddleName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z�-��-�]+$")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}