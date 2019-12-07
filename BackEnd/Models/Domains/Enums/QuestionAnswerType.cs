using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models.Domains.Enums
{
    /// <summary>
    /// Тип ответа на вопрос
    /// </summary>
    public enum QuestionAnswerType : byte
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
