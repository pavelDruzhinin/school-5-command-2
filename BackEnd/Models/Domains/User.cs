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
    public class User : IdentityUser
    {
        /// <summary>
        /// Созданные чаты пользователя
        /// </summary>
        public List<Chat> Chats { get; set; }
    }
}
