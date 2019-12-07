using ChatsConstructor.WebApi.Models.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models.Hubs.Chat.Dto
{
    public class ButtonDto
    {
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
