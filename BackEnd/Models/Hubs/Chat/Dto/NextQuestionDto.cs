﻿using ChatsConstructor.WebApi.Models.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models.Hubs.Chat.Dto
{
    public class NextQuestionDto : QuestionBaseDto
    {
        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Тип возможности ответа на вопрос
        /// </summary>
        public QuestionAnswerType QuestionAnswerType { get; set; }

        /// <summary>
        /// Кнопки ответа
        /// </summary>
        public List<ButtonDto> Buttons { get; set; }
    }
}
