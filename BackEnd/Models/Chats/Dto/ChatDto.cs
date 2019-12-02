using System.ComponentModel.DataAnnotations;
 
namespace ChatsConstructor.WebApi.Dto
{
    public class ChatDto
    {
        [Required]
        public string Name { get; set; }
    }
}