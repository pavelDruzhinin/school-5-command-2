using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models.Hubs.Chat.Dto
{
    public class NextQuestionDto : QuestionDto
    {
        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Кнопки ответа
        /// </summary>
        public List<ButtonDto> Buttons { get; set; }
    }
}
