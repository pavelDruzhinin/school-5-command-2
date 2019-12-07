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

                foreach (QuestionDto questionDto in Model.Questions)
                {   
                    Question q;
                    QuestionType qt;

                    if (questionDto.Buttons == null)
                        qt = QuestionType.OnlyChatAvailable;
                    else
                        qt = QuestionType.OnlyButtonsAvailable;

                    if (questionDto.Id == null) {
                        q = new Question()
                        {
                            ChatId = ChatId,
                            Text = questionDto.Text,
                            QueueNumber = ++queueNumber,
                            QuestionType = qt
                        };

                        _db.Questions.Add(q);
                    } else {
                        q = _db.Questions.FirstOrDefault(x => x.Id == questionDto.Id);
                        
                        q.Text = questionDto.Text;
                        q.QueueNumber = ++queueNumber;
                        q.QuestionType = qt;

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