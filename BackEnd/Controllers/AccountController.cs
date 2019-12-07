using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ChatsConstructor.WebApi.Models.Domains;
using ChatsConstructor.WebApi.Dto;
using Microsoft.AspNetCore.Authorization;

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


        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <remarks> 
        /// POST **/Register**
        /// {
        ///   'Email' : test@test.ru,
        ///   'Password' : password
        /// }
        ///</remarks>
        /// <param name="model">Имя и почта пользователя </param>
        /// <remarks></remarks>        
        /// <response code='200'>Пользователь успешно зарегистрирован</response>
        /// <response code='401'>Решистрация отклонена(?)</response>

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(RegisterDto model)
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
        /// <param name="model"></param>
        /// <response code='200'> Пользователь успешно авторизован</response>
        /// <response code='401'> Пользователь не прошел аутентификацию</response>
        [HttpPost]
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