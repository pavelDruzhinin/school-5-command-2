using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 
namespace ChatsConstructor.WebApi.Dto
{
    public class QuestionsDto
    {
        [Required]
        public List<QuestionDto> Questions { get; set; }
    }
}