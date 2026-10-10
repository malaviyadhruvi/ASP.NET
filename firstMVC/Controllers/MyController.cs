using Microsoft.AspNetCore.Mvc;

namespace firstMVC.Controllers
{
    public class MyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult Service()
        {
            return View();
        }
        public IActionResult Gallery()
        {
            return View();
        }


    }
}
