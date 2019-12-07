using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models.Hubs.Chat.Dto
{
    public class QuestionDto
    {
        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Ответ на вопрос
        /// </summary>
        public string Answer { get; set; }
    }
}
