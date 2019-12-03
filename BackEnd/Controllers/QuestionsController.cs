using System;
using System.Collections.Generic;
using System.Linq;
using ChatsConstructor.WebApi.Models;
using ChatsConstructor.WebApi.Models.Domains;
using ChatsConstructor.WebApi.Models.Domains.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace ChatsConstructor.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class QuestionsController : ControllerBase
    {
        private ChatsConstructorContext _db;
        // private readonly UserManager<User> _userManager;
        public QuestionsController(ChatsConstructorContext db)
        {
            _db = db;

           // _userManager = userManager;
        }

        ///<summary>
        ///Метод GetQuestionsByChatId выводит
        ///вопросы в выбранном чате
        ///</summary>
        ///<param name="chatId">Идентификатор чата</param>
        /*[HttpGet]
         [Route("{chatId}/questions")]
        public IEnumerable<Question> GetQuestionsByChatId(long chatId)
        {
            var questions = _db.Questions.Where(x => x.ChatId == chatId).ToList();
            return questions; 
        }*/
        [HttpGet]
        [Route("questions")]
        public IActionResult GetQuestionsByChatId(long chatId)
        {
            var questions = _db.Questions.Where(x => x.ChatId == chatId).ToList();
            if (questions==null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(questions);
            }
        }
#nullable enable
        ///<summary>
        ///Метод CreateQuestion создает
        ///вопросы с кнопками или без
        ///</summary>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>     
        // Представляю так: юзер ищет сначала вопросы по чатАйди, открыл. Там есть кнопка "Создать" с ссылкой на мой криэйт.
        [HttpPost]
        [Route("questions/create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateQuestion(Question question, [FromForm]Button? button1)
        {
            _db.Questions.Add(question);
            _db.Buttons.Add(button1);
            _db.SaveChanges();

            return CreatedAtRoute("GetQuestionsByChatId", new { chatId = question.ChatId }, question);


        }
    }
}