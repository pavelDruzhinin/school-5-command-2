using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatsConstructor.WebApi.Models;
using ChatsConstructor.WebApi.Models.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private ChatsConstructorContext db;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,ChatsConstructorContext context)
        {
            _logger = logger;
            db = context;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        public IActionResult Register(User user)
        {
            User CheckUser = db.Users.FirstOrDefault(u => u.Email == user.Email);

            if (CheckUser == null)
            {
                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction();
            }
            else
            {

            }

            return ViewComponent()
        }
    }
}
