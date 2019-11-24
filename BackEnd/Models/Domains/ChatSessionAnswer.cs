using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models.Domains
{
    /// <summary>
    /// Представляет собой ответ на вопрос в сессии, который хранится в БД
    /// </summary>
    public class ChatSessionAnswer
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        public long SessionId { get; set; }
        public ChatSession Session { get; set; }

        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public long QuestionId { get; set; }
        public Question Question { get; set; }

        /// <summary>
        /// Дата ответа на вопрос
        /// </summary>
        public DateTime AnswerUtcDateTime { get; set; }

        /// <summary>
        /// Дата удаления ответа на вопрос
        /// </summary>
        public DateTime? DeleteUtcDateTime { get; set; }

        /// <summary>
        /// Текст ответа на вопрос
        /// </summary>
        public string Text { get; set; }
    }
}
