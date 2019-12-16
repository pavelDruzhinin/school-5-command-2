using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models.Domains.Enums
{
    /// <summary>
    /// Прогресс
    /// </summary>
    public enum SessionProgressType : byte
    {
        /// <summary>
        /// Только зашел в чат, еще не начал
        /// </summary>
        NotStarted,

        /// <summary>
        /// Прохождение опроса в процессе
        /// </summary>
        InProgress,
        
        /// <summary>
        /// Прохождение опроса заверешно
        /// </summary>
        Completed
    }
}
