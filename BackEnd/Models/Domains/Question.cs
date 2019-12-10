using ChatsConstructor.WebApi.Models.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models.Domains
{
    /// <summary>
    /// Представляет собой вопрос, который хранится в БД
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор чата
        /// </summary>
        public long ChatId { get; set; }
        public Chat Chat { get; set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Номер вопроса
        /// </summary>
        public short QueueNumber { get; set; }

        /// <summary>
        /// Дата удаления вопроса
        /// </summary>
        public DateTime? DeleteUtcDateTime { get; set; }

        /// <summary>
        /// Тип вопроса
        /// </summary>
        public QuestionType QuestionType { get; set; }

        /// <summary>
        /// Тип возможности ответа на вопрос
        /// </summary>
        public QuestionAnswerType QuestionAnswerType { get; set; }

        /// <summary>
        /// Ответы на этот вопрос
        /// </summary>
        public List<ChatSessionAnswer> ChatSessionAnswers { get; set; }

        /// <summary>
        /// Кнопки вопроса
        /// </summary>
        public List<Button> Buttons { get; set; }
    }
}
