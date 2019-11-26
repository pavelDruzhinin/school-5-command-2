using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models.Domains.Enums
{
    /// <summary>
    /// Тип вопроса
    /// </summary>
    public enum QuestionType : byte
    {
        /// <summary>
        /// Доступен только чат
        /// </summary>
        OnlyChatAvailable,

        /// <summary>
        /// Доступны только кнопки
        /// </summary>
        OnlyButtonsAvailable
    }
}
