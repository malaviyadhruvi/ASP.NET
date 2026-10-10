using firstMVC.Data;
using Microsoft.AspNetCore.Mvc;
using firstMVC.Dto;
using firstMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
namespace firstMVC.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> UpdateProductForm(int id)
        {
            var data = context.Products.Select(x => new ProductDto {
                Id = x.Id,
                ProductName = x.ProductName,
                Color = x.Color,
                Description = x.Description,
                Price = x.Price
            }). FirstOrDefault(x => x.Id == id);
            return View(data);

        }
        public async Task<IActionResult> UpdateProduct(ProductDto dto)
        {
            if(dto == null)
            {
                ViewBag.ErrorMessage = "Please Fill All The Details";
                return View("UpdateProductForm");
            }
            var data = context.Products.FirstOrDefault(x => x.Id == dto.Id);
            data.Price = dto.Price;
            data.ProductName = dto.ProductName;
            data.Description = dto.Description;
            data.Color = dto.Color;

            context.Products.Update(data);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
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
        public async Task<IActionResult> DeleteProduct(int productid)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == productid);

            if (product == null)
            {
                return NotFound();
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

       
    }
}
