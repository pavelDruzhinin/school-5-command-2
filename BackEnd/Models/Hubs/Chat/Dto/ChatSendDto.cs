using ChatsConstructor.WebApi.Models.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models.Hubs.Chat.Dto
{
    public class ChatSendDto
    {
        /// <summary>
        /// Идентификатор чата
        /// </summary>
        //public long ChatId { get; set; }

        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        public long SessionId { get; set; }

        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public long? NextQuestionId { get; set; }

        /// <summary>
        /// Текст ответа на прошлый вопрос
        /// </summary>
        public string AnswerForPreviousQuestion { get; set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string NextQuestionText { get; set; }

        /// <summary>
        /// Тип возможности ответа на вопрос
        /// </summary>
        public QuestionAnswerType? QuestionAnswerType { get; set; }

        /// <summary>
        /// Кнопки
        /// </summary>
        public List<ButtonDto> Buttons { get; set; }

        /// <summary>
        /// Закончились ли вопросы (если true, значит вопросы кончились)
        /// </summary>
        public bool IsQuestionsEnded { get; set; }
    }
}
