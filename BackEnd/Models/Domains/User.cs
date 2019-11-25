using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models.Domains
{
    /// <summary>
    /// Представляет собой пользователя, который хранится в БД
    /// </summary>
    public class User : IdentityUser<Guid>
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Созданные чаты пользователя
        /// </summary>
        public List<Chat> Chats { get; set; }

        /// <summary>
        /// Сессии которые есть у пользователя
        /// </summary>
        public List<ChatSession> Sessions { get; set; }
    }
}
