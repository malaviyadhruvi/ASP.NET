using firstMVC.Data;
using firstMVC.Dto;
using firstMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace firstMVC.Controllers
{
    public class AuthController(appDbContext _context) : Controller
    {
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
                ViewBag.ErrorMessage = "please enter valid details";
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
    }
}
