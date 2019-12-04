using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatsConstructor.WebApi.Dto;
using ChatsConstructor.WebApi.Models;
using ChatsConstructor.WebApi.Models.Domains;
using ChatsConstructor.WebApi.Models.Domains.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



namespace ChatsConstructor.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private ChatsConstructorContext _db;
        private readonly UserManager<User> _userManager;
        public QuestionsController(UserManager<User> userManager, ChatsConstructorContext db)
        {
            _db = db;

            _userManager = userManager;
        }

        ///<summary>
        ///Метод GetQuestionsByChatId выводит
        ///вопросы в выбранном чате
        ///</summary>
        ///<param name="chatId">Идентификатор чата</param>

        [HttpGet]
        [Route("{chatId}")]
        public async Task<IActionResult> GetQuestionsByChatId(long chatId)
        {
            var user = await _userManager.GetUserAsync(User);

            var questions = _db.Questions
                            .Where(x => x.ChatId == chatId).ToList()
                            .Select(x => new { x.QueueNumber, x.Text, x.QuestionType, x.Chat })
                            .ToList();
            return Ok(questions);
        }
#nullable enable
        ///<summary>
        ///Метод CreateQuestion создает
        ///вопросы с кнопками или без
        ///</summary>
        /// <response code="201">Возвращает созданный вопрос</response>
        /// <response code="400">Не удалось создать вопрос</response>     
        // Представляю так: юзер ищет сначала вопросы по чатАйди,  открыл. Там есть кнопка "Создать" с ссылкой на мой криэйт.
        [HttpPost]
        [Route("{chatId}/create")]
        public async Task<IActionResult> CreateQuestion([FromRoute]long chatId, QuestionDto ModelQ, [FromBody]ButtonDto? ModelB)
        {
            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                Question question = new Question()
                {
                    ChatId = chatId,
                    QueueNumber = ModelQ.QueueNumber,
                    Text = ModelQ.Text,
                    QuestionType = ModelQ.QuestionType
                };

                if ((int)ModelQ.QuestionType == 1)

                {
                    Button button = new Button()
                    {
                        QuestionId = question.Id,
                        Text = ModelB.Text,
                        ColorType = ModelB.ColorType
                    };
                    // Нам надо добавить минимум 2 кнопки, а я здесь добавляю только 1?? Нужно ли дописать в параметры ButtonDto? ModelB2 ?? а если кнопок >2?? 
                    _db.Questions.Add(question);
                    _db.Buttons.Add(button);
                    _db.SaveChanges();
                }
                else
                {
                    _db.Questions.Add(question);
                    _db.SaveChanges();
                }
                return Ok(new
                {
                    QueueNumber = ModelQ.QueueNumber,
                    Text = ModelQ.Text,
                    QuestionType = ModelQ.QuestionType
                }
                );
            }
            else
            {
                return BadRequest(ModelState);
            }


        }
    }
}