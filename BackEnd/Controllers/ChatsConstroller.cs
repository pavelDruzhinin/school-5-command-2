using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ChatsConstructor.WebApi.Models.Domains;
using ChatsConstructor.WebApi.Dto;
using ChatsConstructor.WebApi.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ChatsConstructor.WebApi.Models.Domains.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Application.Web.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ChatsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ChatsConstructorContext _db;

        public ChatsController(UserManager<User> userManager, ChatsConstructorContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        /// <summary>
        /// Получение списка чатов
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Chats
        ///     
        /// </remarks>
        /// <returns></returns>
        /// <response code='200'>Возвращает список чатов</response>
        /// <response code='401'>Пользователь не авторизован</response>
        [HttpGet]
        public async Task<IActionResult> Get ()
        {
            var user = await _userManager.GetUserAsync(User);

            var chatsList = _db.Chats
                .Where(x => x.UserId == user.Id)
                .Select(x => new { x.Id, x.Name, x.CreateUtcDateTime }).ToList();

            return Ok(chatsList);
        }

        /// <summary>
        /// Получение сессии при заходе по ссылке в чат
        /// </summary>
        /// <param name="chatId">Идентификатор чата</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{chatId:long}")]
        public async Task<IActionResult> GetSession(long chatId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
                return BadRequest();

            var notCompletedSession = user.Sessions.FirstOrDefault(x => x.ChatId == chatId && !x.IsCompleted);

            if (notCompletedSession != null)
            {
                return Json(new { SessionId = notCompletedSession.Id });
            }

            var createdSession = new ChatSession()
            {
                ChatId = chatId,
                Status = SessionProgressType.NotStarted,
                UserId = user.Id
            };

            _db.ChatSessions.Add(createdSession);
            _db.SaveChanges();

            var welcomeQuestion = _db.Questions.FirstOrDefault(x => x.ChatId == chatId && x.QuestionType == QuestionType.Welcome);

            _db.ChatSessionAnswers.Add(new ChatSessionAnswer()
            {
                SessionId = createdSession.Id,
                QuestionId = welcomeQuestion.Id,
            });

            _db.SaveChanges();

            return Json(new { SessionId = createdSession.Id });
        }
        /// <summary>
        /// Создание чата пользователем
        /// </summary>
        /// <param name="Model">Чат</param>
        /// <returns></returns>
        [HttpPost]
        [DefaultValue("Name")]
        [Produces(typeof(ChatDto))]
        [Route("Add")]
        public async Task<IActionResult> Add ([FromBody] ChatDto Model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid) {
                Chat c = new Chat() {
                    UserId = user.Id,
                    Name = Model.Name,
                    CreateUtcDateTime = DateTime.UtcNow
                };

                _db.Chats.Add(c);
                _db.SaveChanges();

                return Ok(new {
                    Id = c.Id,
                    Name = c.Name,
                    CreateUtcDateTime = c.CreateUtcDateTime
                });
            } else {
                return BadRequest(ModelState);
            }
        }

        
    }
}