using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models.Hubs.Chat.Dto
{
    public class ChatAnswerDto
    {
        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        public long SessionId { get; set; }

        /// <summary>
        /// Идентификатор вопроса на который ответили
        /// </summary>
        public long QuestionId { get; set; }

        /// <summary>
        /// Текст ответа на вопрос
        /// </summary>
        public string Answer { get; set; }
    }
}
