using AutoMapper;
using ChatsConstructor.WebApi.Models;
using ChatsConstructor.WebApi.Models.Domains;
using ChatsConstructor.WebApi.Models.Domains.Enums;
using ChatsConstructor.WebApi.Models.Hubs.Chat.Dto;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsConstructor.WebApi.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatsConstructorContext _db;
        private readonly IMapper _mapper;

        public ChatHub(ChatsConstructorContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        /// <summary>
        /// Должно срабатывать когда человек зашел в сессию
        /// </summary>
        /// <param name="id">Идентификатор сессии</param>
        /// <returns></returns>
        public async Task EnterToSession(long id)
        {
            // Группа для SignalR (потом мб на гуид переделаем)
            var groupId = Convert.ToString(id);

            // Добавляем это подключение в группу
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);

            // Если нет такой сессии
            if (!_db.ChatSessions.Any(x => x.Id == id))
                return;

            // Получаем историю ответов для сессии
            var answersHistory = _db.ChatSessionAnswers
                .Where(x => x.SessionId == id).ToList();
            
            // Если нет ниодного ответа
            if (answersHistory.Any())
            {
                // Получаем сессию из БД
                var session = _db.ChatSessions
                    .Include(x => x.ChatSessionAnswers)
                        .ThenInclude(x => x.Question)
                            .ThenInclude(x => x.Buttons)
                    .First(x => x.Id == id);

                // Формируем историю для отправки
                var history = _mapper.Map<HistoryDto>(session);

                // Сериализуем в JSON
                var json = JsonConvert.SerializeObject(history);

                // Отправляем историю к новому подключившемуся к сессии человеку
                await Clients.Caller.SendAsync("EnterToSession", json);
            }
            else
            {
                // Получаем сессию из БД
                var session = _db.ChatSessions
                    .Include(x => x.Chat)
                        .ThenInclude(x => x.Questions)
                            .ThenInclude(x => x.Buttons)
                    .First(x => x.Id == id);

                // Получаем первый вопрос
                var firstQuestion = session.Chat.Questions.First(x => x.QueueNumber == 0);

                var answer = new ChatSessionAnswer()
                {
                    QuestionId = firstQuestion.Id,
                    SessionId = id,
                };

                // Добавляем в историю новый вопрос
                _db.ChatSessionAnswers.Add(answer);
                _db.SaveChanges();

                answer.Question = firstQuestion;

                // Формируем историю для отправки
                var history = new HistoryDto()
                {
                    QuestionsHistory = null,
                    NextQuestion = _mapper.Map<NextQuestionDto>(answer),
                    IsSessionCompleted = session.IsCompleted
                };

                // Сериализуем в JSON
                var json = JsonConvert.SerializeObject(history);

                // Отправляем историю к новому подключившемуся к сессии человеку
                await Clients.Caller.SendAsync("EnterToSession", json);
            }
        }

        /// <summary>
        /// Суда нужно присылать ответ на вопрос
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task AnswerForTheQuestion(ChatAnswerDto dto)
        {
            // Группа для SignalR (потом мб на гуид переделаем)
            var groupId = Convert.ToString(dto.SessionId);

            // Получаем сессию из бд
            var session = _db.ChatSessions.Include(x => x.ChatSessionAnswers).FirstOrDefault(x => x.Id == dto.SessionId);

            // Если нет сессии
            if (session == null)
                return;

            // Если вопрос не существует для этого чата не существует
            if (!IsQuestionExist(session.ChatId, dto.QuestionId))
                return;

            // Получаем вопрос из истории сессии
            var questionForAnswer = session.ChatSessionAnswers.FirstOrDefault(x => x.QuestionId == dto.QuestionId);

            // Если нет вопроса в истории сессии
            if (questionForAnswer == null)
                return;

            // Добавляем ответ на вопрос в истории сессии
            questionForAnswer.AnswerUtcDateTime = DateTime.UtcNow;
            questionForAnswer.Text = dto.Answer;

            // Делаем запрос на обновление вопроса в истории сессии
            _db.ChatSessionAnswers.Update(questionForAnswer);

            var questionType = _db.Questions.First(x => x.Id == dto.QuestionId).QuestionType;

            switch (questionType)
            {
                // Если тип вопроса(на который ответили) является приветствием
                case QuestionType.Welcome:

                    // Ставим сессию в статус "в прогрессе"
                    session.Status = SessionProgressType.InProgress;
                    _db.ChatSessions.Update(session);

                    break;

                // Если тип вопроса(на который ответили) является завершающим
                case QuestionType.Final:

                    // Ставим сессию в статус "Завершено"
                    session.Status = SessionProgressType.Completed;
                    _db.ChatSessions.Update(session);

                    break;
            }

            // Получаем следующий вопрос
            var nextQuestion = GetNextQuestion(session.ChatId, dto.QuestionId);

            // Делаем запрос на добавление вопроса в историю
            _db.ChatSessionAnswers.Add(new ChatSessionAnswer()
            {
                SessionId = session.Id,
                QuestionId = nextQuestion.Id,
            });

            // Выполняем все запросы указанные выше
            _db.SaveChanges();

            // Формируем данные для отправки
            var sendDto = new ChatSendDto()
            {
                NextQuestionId = nextQuestion?.Id,
                NextQuestionText = nextQuestion?.Text,
                SessionId = session.Id,
                AnswerForPreviousQuestion = dto.Answer,
                IsQuestionsEnded = nextQuestion == null,
                Buttons = nextQuestion?.Buttons?.Select(x => new ButtonDto { Text = x.Text, ColorType = x.ColorType }).ToList()
            };

            // Сериализуем в JSON
            var json = JsonConvert.SerializeObject(sendDto);

            // Отсылаем на фронт данные для всех людей которые находятся в этой сессии
            await Clients.Group(groupId).SendAsync("GetNextQuestion", json);
        }

        /// <summary>
        /// Если вопрос существует
        /// </summary>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="questionId">Идентификатор вопроса</param>
        /// <returns></returns>
        private bool IsQuestionExist(long chatId, long questionId)
        {
            return _db.Chats.Include(x => x.Questions)
                .Any(x => x.Id == chatId && x.Questions.Any(y => y.Id == questionId));
        }

        /// <summary>
        /// Получить следующий вопрос
        /// </summary>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="questionId">Идентификатор вопроса</param>
        /// <returns></returns>
        private Question GetNextQuestion(long chatId, long questionId)
        {
            var question = _db.Questions.FirstOrDefault(x => x.Id == questionId);

            if (question == null)
                return null;

            var previousQueueNumber = question.QueueNumber;

            var nextQueueNumber = previousQueueNumber + 1;

            var nextQuestion =_db.Questions.Include(x => x.Buttons)
                .FirstOrDefault(x => x.ChatId == chatId && x.QueueNumber == nextQueueNumber);

            return nextQuestion;
        }
    }
}
