using ChatsConstructor.WebApi.Models.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models.Domains
{
    /// <summary>
    /// Представляет собой сессию прохождения в чате, которая хранится в БД
    /// </summary>
    public class ChatSession
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя (если есть)
        /// </summary>
        public Guid? UserId { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Идентификатор чата
        /// </summary>
        public long ChatId { get; set; }
        public Chat Chat { get; set; }

        /// <summary>
        /// Прогресс прохождения
        /// </summary>
        public SessionProgressType Status { get; set; }

        /// <summary>
        /// Ответы на эту сессию
        /// </summary>
        public List<ChatSessionAnswer> ChatSessionAnswers { get; set; }

        /// <summary>
        /// Сессия завершена?
        /// </summary>
        public bool IsCompleted { get { return Status == SessionProgressType.Completed; } }
    }
}
