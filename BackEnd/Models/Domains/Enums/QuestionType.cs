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
        /// Вопрос приветствия
        /// </summary>
        Welcome = 0,

        /// <summary>
        /// Обычный вопрос
        /// </summary>
        Normal = 1,

        /// <summary>
        /// Вопрос окончания диалога
        /// </summary>
        Final = 2
    }
}
