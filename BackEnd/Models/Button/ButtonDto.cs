using ChatsConstructor.WebApi.Models.Domains.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChatsConstructor.WebApi.Dto
{
    public class ButtonDto
    {
        public long? Id { get; set; }
        [Required]
        public string Text { get; set; }
    }
}