using ChatsConstructor.WebApi.Models.Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models
{
    public class ChatsConstructorContext : IdentityDbContext<User>
    {
        public ChatsConstructorContext(DbContextOptions<ChatsConstructorContext> options) : base(options)
        {
        }

        /// <summary>
        /// Таблица с чатами
        /// </summary>
        public DbSet<Chat> Chats { get; set; }

        /// <summary>
        /// Таблица с сессиями у чатов
        /// </summary>
        public DbSet<ChatSession> ChatSessions { get; set; }

        /// <summary>
        /// Таблица с ответами на вопросы в сессии
        /// </summary>
        public DbSet<ChatSessionAnswer> ChatSessionAnswers { get; set; }

        /// <summary>
        /// Таблица с вопросами
        /// </summary>
        public DbSet<Question> Questions { get; set; }

        /// <summary>
        /// Таблица с кнопками
        /// </summary>
        public DbSet<Button> Buttons { get; set; }
    }
}
