using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 
namespace ChatsConstructor.WebApi.Dto
{
    public class QuestionDto
    {
        public long? Id { get; set; }
        [Required]
        public string Text { get; set; }

        public List<ButtonDto> Buttons { get; set; }
    }
}