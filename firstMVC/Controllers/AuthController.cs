using firstMVC.Data;
using firstMVC.Dto;
using firstMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace firstMVC.Controllers
{
    public class AuthController(appDbContext _context) : Controller
    {
        private readonly string token;

        //private readonly appDbContext _context;
        //private AuthController(appDbContext context)
        //{
        //    _context = context;
        //}
        public IActionResult Login()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> CreateUser(UserDto dto)
        {
            if(dto == null || string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            {
                ViewBag.ErrorMessage = "please enter all details";
                return View("Register");
            }
            var existingUser = await _context.Users.FirstOrDefaultAsync(u=>u.Email == dto.Email);

            if(existingUser == null)
            {
                var user = new User
                {
                    Email = dto.Email,
                    Password = dto.Password,
                    Username = dto.Username
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                ViewBag.ErrorMessage = "User with this Email already registred!";
                return View("Register");
            }
            TempData["SuccessMessage"] = "User created successfully! login please";
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> LoginUser(UserDto dto)
        {
            var isUserExist = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (isUserExist == null)
            {
                ViewBag.ErrorMessage = "User not found!";
                return View("Login");

            }
            else
            {
                if (isUserExist.Password == dto.Password)
                {
                    GenerateJwtToken(dto);
                    Response.Cookies.Append("jwtToken",token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddMinutes(30)
                    });
                    TempData["SuccessMessage"] = "Login successful!";
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.ErrorMessage = "Password is incorrect!";
                    return View("Login");
                }
            }
        }

        private String GenerateJwtToken(UserDto dto)
        {
            var jwthandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("59a914182cd05caca577b534e6bd116d20c39a328c04902edc6a528221e2a87e");
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,dto.Email),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwthandler.CreateToken(tokenDescripter);
            return jwthandler.WriteToken(token);

        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwtToken");
            TempData["SuccessMessage"] = "Logout successful!";
            return RedirectToAction("Login");
        }
    }
}
