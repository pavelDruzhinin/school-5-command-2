using ChatsConstructor.WebApi.Models.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models.Domains
{
    /// <summary>
    /// Представляет собой кнопку, которая хранится в БД
    /// </summary>
    public class Button
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public long QuestionId { get; set; }
        public Question Question { get; set; }

        /// <summary>
        /// Текст кнопки
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Цвет кнопки
        /// </summary>
        public ColorType ColorType { get; set; }
    }
}
