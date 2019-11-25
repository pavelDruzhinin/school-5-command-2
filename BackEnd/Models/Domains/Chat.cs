using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models.Domains
{
    /// <summary>
    /// Представляет собой чат, который хранится в БД
    /// </summary>
    public class Chat
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Название чата
        /// </summary>
        [StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор создателя чата
        /// </summary>
        public Guid UserId { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Дата создания чата
        /// </summary>
        public DateTime CreateUtcDateTime { get; set; }

        /// <summary>
        /// Дата удаления чата
        /// </summary>
        public DateTime? DeleteUtcDateTime { get; set; }

        /// <summary>
        /// Текст приветствия
        /// </summary>
        public string WelcomeText { get; set; }

        /// <summary>
        /// Текст завершающий диалог
        /// </summary>
        public string FinalText { get; set; }

        /// <summary>
        /// Вопросы
        /// </summary>
        public List<Question> Questions { get; set; }

        /// <summary>
        /// Сессии чата
        /// </summary>
        public List<ChatSession> Sessions { get; set; }
    }
}
