﻿using AutoMapper;
using ChatsConstructor.WebApi.Models.Domains;
using ChatsConstructor.WebApi.Models.Domains.Enums;
using ChatsConstructor.WebApi.Models.Hubs.Chat.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapForSessionToHistory();
        }

        private void MapForSessionToHistory()
        {
            CreateMap<ChatSession, HistoryDto>()
                .ForMember(dest => dest.IsSessionCompleted, opt => opt.MapFrom(x => x.Status == SessionProgressType.Completed))
                .ForMember(dest => dest.QuestionsHistory, opt => opt
                    .MapFrom(x => x.IsCompleted
                        ? x.ChatSessionAnswers.OrderBy(y => y.Question.QueueNumber)
                        : x.ChatSessionAnswers.Except(new List<ChatSessionAnswer>() { x.ChatSessionAnswers.Last() }).OrderBy(y => y.Question.QueueNumber)))
                .ForMember(dest => dest.NextQuestion, opt => opt.MapFrom(x => x.IsCompleted ? null : x.ChatSessionAnswers.Last()));

            CreateMap<ChatSessionAnswer, QuestionBaseDto>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(x => x.Question.Text));

            CreateMap<ChatSessionAnswer, HistoryQuestionDto>()
                .ForMember(dest => dest.Answer, opt => opt.MapFrom(x => x.Text))
                .IncludeBase<ChatSessionAnswer, QuestionBaseDto>();

            CreateMap<ChatSessionAnswer, NextQuestionDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.QuestionId))
                .ForMember(dest => dest.Buttons, opt => opt.MapFrom(x => x.Question.Buttons))
                .ForMember(dest => dest.QuestionAnswerType, opt => opt.MapFrom(x => x.Question.QuestionAnswerType))
                .IncludeBase<ChatSessionAnswer, QuestionBaseDto>();
                

            CreateMap<Button, ButtonDto>();
        }
    }
}
