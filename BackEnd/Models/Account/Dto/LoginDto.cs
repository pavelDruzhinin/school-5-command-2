using System.ComponentModel.DataAnnotations;
 
namespace ChatsConstructor.WebApi.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
         
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
         
        public bool RememberMe { get; set; }
    }
}