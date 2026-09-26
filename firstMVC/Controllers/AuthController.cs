using Microsoft.AspNetCore.Mvc;

namespace firstMVC.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult CreateUser(UserDto dto)
        {

        }
    }
}
