using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using ChatsConstructor.WebApi.Dto;
using ChatsConstructor.WebApi.Models;
using ChatsConstructor.WebApi.Models.Domains;
using ChatsConstructor.WebApi.Models.Domains.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatsConstructor.WebApi.Controllers
{
    [ApiController]
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

        [HttpGet]
        [Route("{ChatId}")]
        public IActionResult Get(long ChatId)
        {
            var QuestionsList = _db.Questions
                    .Where(x => x.ChatId == ChatId && x.DeleteUtcDateTime == null)
                    .OrderBy(x => x.QueueNumber)
                    .Select(x => new { x.Id, x.Text, x.QueueNumber }).ToList();

            var QuestionsListResponse = new List<Object>() { };

            foreach (var QuestionsListItem in QuestionsList)
            {
                QuestionsListResponse.Add(new
                {
                    Id = QuestionsListItem.Id,
                    Text = QuestionsListItem.Text,
                    QueueNumber = QuestionsListItem.QueueNumber,
                    Buttons = _db.Buttons.Where(x => x.QuestionId == QuestionsListItem.Id).Select(x => new { x.Id, x.Text }).ToList()
                });
            }

            return Ok(QuestionsListResponse);
        }

        [HttpPost]
        [Route("{ChatId}")]
        public IActionResult Add(long ChatId, QuestionsDto Model)
        {
            if (ModelState.IsValid)
            {
                short queueNumber = 0;

                foreach (QuestionDto question in Model.Questions)
                {
                    QuestionType qt;
                    if (question.Buttons == null)
                    {
                        qt = QuestionType.OnlyChatAvailable;
                    }
                    else
                    {
                        qt = QuestionType.OnlyButtonsAvailable;
                    }

                    Question q = new Question()
                    {
                        ChatId = ChatId,
                        Text = question.Text,
                        QueueNumber = ++queueNumber,
                        QuestionType = qt
                    };

                    _db.Questions.Add(q);
                    _db.SaveChanges();

                    if (question.Buttons != null)
                    {
                        foreach (ButtonDto button in question.Buttons)
                        {
                            Button b = new Button()
                            {
                                QuestionId = q.Id,
                                Text = button.Text
                            };

                            _db.Buttons.Add(b);
                            _db.SaveChanges();
                        }
                    }
                }

                var QuestionsList = _db.Questions
                    .Where(x => x.ChatId == ChatId && x.DeleteUtcDateTime == null)
                    .OrderBy(x => x.QueueNumber)
                    .Select(x => new { x.Id, x.Text, x.QueueNumber }).ToList();

                var QuestionsListResponse = new List<Object>() { };

                foreach (var QuestionsListItem in QuestionsList)
                {
                    QuestionsListResponse.Add(new
                    {
                        Id = QuestionsListItem.Id,
                        Text = QuestionsListItem.Text,
                        QueueNumber = QuestionsListItem.QueueNumber,
                        Buttons = _db.Buttons.Where(x => x.QuestionId == QuestionsListItem.Id).Select(x => new { x.Id, x.Text }).ToList()
                    });
                }

                return Ok(QuestionsListResponse);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}