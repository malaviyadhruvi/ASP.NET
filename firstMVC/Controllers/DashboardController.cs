using firstMVC.Data;
using Microsoft.AspNetCore.Mvc;
using firstMVC.Dto;
using firstMVC.Models;
namespace firstMVC.Controllers
{
    public class DashBoardController(appDbContext context) : Controller
    {
        public IActionResult Index()
        {
            var list = context.Products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Description = p.Description,
                Price = p.Price,
                Color = p.Color
            }).ToList();
            return View(list);
        }
        public IActionResult ProductForm()
        {
            return View();
        }
        public async Task<IActionResult> CreateProduct(ProductDto dto)
        {
            context.Products.Add(new Product
            {
                ProductName = dto.ProductName,
                Description = dto.Description,
                Price = dto.Price,
                Color = dto.Color
            });

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }   
    }
}
