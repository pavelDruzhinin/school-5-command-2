using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using ChatsConstructor.WebApi.Dto;
using ChatsConstructor.WebApi.Models;
using ChatsConstructor.WebApi.Models.Domains;
using ChatsConstructor.WebApi.Models.Domains.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatsConstructor.WebApi.Controllers
{

    [Produces("application/json")]
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ChatsConstructorContext _db;
        public QuestionsController(UserManager<User> userManager, ChatsConstructorContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        /// <summary>
        /// Получение вопросов по идентификационному номеру чата 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Questions
        ///     {
        ///        "ChatId": 1        
        ///     }
        ///
        /// </remarks>
        /// <param name="ChatId">Идентификационный номер чата</param>
        /// <returns>Возвращает список вопросов</returns>
        /// <response code='200'>Возвращает список вопросов</response>
        /// <response code='401'>Пользователь не авторизован</response>
        [HttpGet]
        [Route("{ChatId}")]
        public IActionResult Get(long ChatId)
        {
            var QuestionsList = _db.Questions
                    .Include(q => q.Buttons)
                    .Where(q => q.ChatId == ChatId && q.DeleteUtcDateTime == null)
                    .OrderBy(q => q.QueueNumber)
                    .Select(x => new { 
                        Id = x.Id, 
                        Text = x.Text, 
                        QueueNumber = x.QueueNumber, 
                        QuestionType = x.QuestionType.ToString(), 
                        Buttons = x.Buttons.Select(b => new { b.Id, b.Text })
                    })
                    .ToList();

            return Ok(QuestionsList);
        }
        /// <summary>
        /// Добавление/редактирование вопросов в выбранном чате
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Questions
        ///     {
        ///        "ChatId": 1        
        ///     }
        ///
        /// </remarks>
        /// <param name="ChatId">Идентификационный номер чата</param>
        /// <param name="Model">Вопрос</param>
        /// <returns></returns>
        /// <response code='200'>Редактирование чата завершено успешно</response>
        /// <response code='401'>Пользователь не авторизован</response>
        [HttpPost]
        [Produces(typeof(List<QuestionsDto>))]
        [Route("{ChatId}")]
        public IActionResult Add(long ChatId, List<QuestionDto> Model)
        {
            if (ModelState.IsValid)
            {
                short queueNumber = 0;

                foreach (QuestionDto questionDto in Model)
                {   
                    Question q;
                    QuestionType qt;
                    QuestionAnswerType qat;

                    if (queueNumber == 0) 
                        qt = QuestionType.Welcome;
                    else if (queueNumber == Model.Count() - 1)
                        qt = QuestionType.Final;
                    else 
                        qt = QuestionType.Normal;

                    if (questionDto.Buttons == null)
                        qat = QuestionAnswerType.OnlyChatAvailable;
                    else
                        qat = QuestionAnswerType.OnlyButtonsAvailable;

                    if (questionDto.Id == null) {
                        q = new Question()
                        {
                            ChatId = ChatId,
                            Text = questionDto.Text,
                            QueueNumber = queueNumber++,
                            QuestionType = qt,
                            QuestionAnswerType = qat
                        };

                        _db.Questions.Add(q);
                    } else {
                        q = _db.Questions.FirstOrDefault(x => x.Id == questionDto.Id);
                        
                        q.Text = questionDto.Text;
                        q.QueueNumber = queueNumber++;
                        q.QuestionType = qt;
                        q.QuestionAnswerType = qat;

                        _db.Questions.Update(q);
                    }

                    _db.SaveChanges();

                    if (questionDto.Buttons != null)
                    {
                        foreach (ButtonDto buttonDto in questionDto.Buttons)
                        {
                            if (buttonDto.Id == null) {
                                Button b = new Button()
                                {
                                    QuestionId = q.Id,
                                    Text = buttonDto.Text
                                };

                                 _db.Buttons.Add(b);
                            } else {
                                Button b = _db.Buttons.FirstOrDefault(x => x.Id == buttonDto.Id);

                                b.Text = buttonDto.Text;

                                _db.Buttons.Update(b);
                            }
                           
                            _db.SaveChanges();
                        }
                    } else {
                        var b = _db.Buttons.Where(x => x.QuestionId == questionDto.Id).ToList();
                        foreach(var button in b) {
                            _db.Buttons.Remove(button);
                        }
                        _db.SaveChanges();
                    }
                }

                var QuestionsList = _db.Questions
                    .Include(q => q.Buttons)
                    .Where(q => q.ChatId == ChatId && q.DeleteUtcDateTime == null)
                    .OrderBy(q => q.QueueNumber)
                    .Select(x => new { 
                        Id = x.Id, 
                        Text = x.Text, 
                        QueueNumber = x.QueueNumber, 
                        QuestionType = x.QuestionType.ToString(), 
                        Buttons = x.Buttons.Select(b => new { b.Id, b.Text })
                    })
                    .ToList();

                return Ok(QuestionsList);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        /// <summary>
        /// Удаление вопроса из чата
        /// </summary>
        /// <param name="questionid">Идентификационный номер вопроса</param>
        /// <returns></returns>
        /// <response code='200'>Вопрос удален из чата</response>
        /// <response code='401'>Пользователь не авторизован</response>
        [HttpPost]
        [Route("delete/{questionid}")]
        public  IActionResult Delete(long questionid){
            var question = _db.Questions.FirstOrDefault(x=>x.Id==questionid);
            question.DeleteUtcDateTime = DateTime.UtcNow;
            _db.Questions.Update(question);
            _db.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Удаление варианта ответа из чата
        /// </summary>
        /// <param name="buttonId">Идентификационный номер варианта</param>
        /// <returns></returns>
        /// <response code='200'>Вариант удален</response>
        /// <response code='401'>Пользователь не авторизован</response>
        [HttpPost]
        [Route("DeleteVariant/{buttonId}")]
        public IActionResult DeleteVariant (long buttonId) {
            var b = _db.Buttons.FirstOrDefault(x => x.Id == buttonId);
            
            _db.Buttons.Remove(b);
            _db.SaveChanges();

            return Ok();
        }
    }
}