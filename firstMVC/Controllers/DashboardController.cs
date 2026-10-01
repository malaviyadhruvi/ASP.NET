using firstMVC.Data;
using Microsoft.AspNetCore.Mvc;
using firstMVC.Dto;
namespace firstMVC.Controllers
{
    public class DashBoardController(appDbContext context) : Controller
    {
        public IActionResult Index()
        {
            var list = context.Products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Color = p.Color
            }).ToList();
            return View(list);
        }
    }
}
