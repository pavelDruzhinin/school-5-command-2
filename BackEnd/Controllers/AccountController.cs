using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ChatsConstructor.WebApi.Models.Domains;
using ChatsConstructor.WebApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Application.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // compile with: -doc:ChatsConstructor.WebApi.xml 
        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Account/Register
        ///     {
        ///        "id": 1        
        ///     }
        ///
        /// </remarks>
        /// <parameters name="model">Имя и почта пользователя </parameters>
        /// <remarks></remarks>        
        /// <response code='201'>Пользователь успешно зарегистрирован</response>
        /// <response code='400'>Решистрация отклонена</response>

        [HttpPost]
        [Route("Register")]
        //[ResponseType(typeof(RegisterDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register([FromBody] RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var exist = await _userManager.FindByEmailAsync(model.Email);
                if(exist!=null) return BadRequest("User with this Email is Exists");
                User user = new User { Email = model.Email, UserName = model.Email, FirstName = model.FirstName, LastName = model.LastName, MiddleName = model.MiddleName };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return Json(new
                    {
                        email = model.Email,
                        fullname = model.FirstName + " " + model.MiddleName + " " + model.LastName
                    });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        return Unauthorized(error.Description);
                    }
                }
            }
            else return BadRequest("Проблема с моделью");
            return BadRequest("Что-то пошло не так");
        }
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Account/Login
        ///     {
        ///        "email": test@test.ru,
        ///        "password": password",
        ///        "rememberMe": true       
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Пользователь</param>
        /// <response code='200'> Пользователь успешно авторизован</response>
        /// <response code='401'> Пользователь не прошел аутентификацию</response>
        [HttpPost]
        //[ResponseType(typeof(LoginDto))]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await _signInManager.UserManager.FindByEmailAsync(model.Email);
                    return Json(new
                    {
                        email = user.Email,
                        fullname = user.FirstName + " " + user.MiddleName + " " + user.LastName
                    });
                }
                else
                {
                    return Unauthorized("Логин не удался");
                }
            }
            return BadRequest("Модель не верна");
        }
        /// <summary>
        /// Выход пользователя из учетной записи
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Account/Logout
        ///     {
        ///        "id": 1        
        ///     }
        ///
        /// </remarks>
        /// 
        ///<response code='200'></response>
        [HttpPost]
        [Route("Logout")]
        public async Task<ActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return Ok("Выход успешен");
        }
        /// <summary>
        /// Нужен для возвращения состояния аккаунта на Фронтэнд
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Account/Context
        ///     {
        ///        "id": 1        
        ///     }
        ///
        /// </remarks>
        ///<response code='200'>Возврат Json с данными юзера или если он не залогинен</response>
        [HttpGet]
        [Route("Context")]
        public async Task<ActionResult> Context()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                return Json(new
                {
                    email = user.Email,
                    fullname = user.FirstName + " " + user.MiddleName + " " + user.LastName
                });
            }
            return Ok("Не залогинен");
        }
    }
}