using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models.Hubs.Chat.Dto
{
    public class HistoryDto
    {
        /// <summary>
        /// История ответов на вопросы
        /// </summary>
        public QuestionDto[] QuestionsHistory { get; set; }

        /// <summary>
        /// Следующий вопрос
        /// </summary>
        public NextQuestionDto NextQuestion { get; set; }

        /// <summary>
        /// Сессия завершена?
        /// </summary>
        public bool IsSessionCompleted { get; set; }
    }
}
