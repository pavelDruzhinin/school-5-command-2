using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ChatsConstructor.WebApi.Models.Domains;
using ChatsConstructor.WebApi.ViewModels;

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

        //remarks описание метода
        //GET /GetUser
        //  {
        //    "id":1
        //}

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="model">Имя и почта пользователя </param>
        /// <remarks></remarks>        
        /// <response code='200'>Пользователь успешно зарегистрирован</response>
        /// <response code='401'>Решистрация отклонена(?)</response>
        [HttpPost]
        [Route("Register")]
        public async Task<string> Register (RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return "{ error: 0, msg: \"Successful Registration\" }";
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        //ModelState.AddModelError(string.Empty, error.Description);
                        return "{ error: 2, msg: \"Wrong Data\" }";
                    }
                }
            }
            return "{ error: 1, msg: \"Invalid Model\" }";
        }
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <response code='200'> Пользователь успешно авторизован</response>
        /// <response code='401'> Пользователь не прошел аутентификацию</response>

        [HttpPost]
        [Route("Login")]
        public async Task<string> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return "{ error: 0, msg: \"Successful Login\" }";
                }
                else
                {
                    return "{ error: 2,  msg: \"Wrong Email or Password\" }";
                }
            }
            return "{ error: 1,  msg: \"Invalid Model\" }";
        }
        /// <summary>
        /// Выход пользователя из учетной записи
        /// </summary>
        ///<response code='200'></response>
        [HttpPost]
        [Route("Logout")]
        public async Task<string> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return "{ error: 0,  msg: \"Successful Logout\" }";
        }
    }
}