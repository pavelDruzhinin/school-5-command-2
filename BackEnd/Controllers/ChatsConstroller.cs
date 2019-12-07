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

namespace Application.Web.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> Get ()
        {
            var user = await _userManager.GetUserAsync(User);

            var chatsList = _db.Chats
                .Where(x => x.UserId == user.Id)
                .Select(x => new { x.Id, x.Name, x.CreateUtcDateTime }).ToList();

            return Ok(chatsList);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add (ChatDto Model)
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