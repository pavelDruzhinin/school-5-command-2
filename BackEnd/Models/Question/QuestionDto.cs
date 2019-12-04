

using ChatsConstructor.WebApi.Models.Domains.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChatsConstructor.WebApi.Dto
{
    public class QuestionDto
    {
        [Required]
        public short QueueNumber { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        [EnumDataType(typeof(QuestionType), ErrorMessage = "Вопросы могут быть с кнопками или без")]
        public QuestionType QuestionType { get; set; }
    }
}
