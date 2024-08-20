using BOOKSTORE.Data;
using BOOKSTORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BOOKSTORE.Controllers
{
    public class HomeController : Controller
    {
        private readonly QlbhContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, QlbhContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string? categoryId)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(categoryId))
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }

            var productList = products.ToList(); //lấy danh sách sản phẩm

            return View(productList);
        }

        public IActionResult AboutApp()
        {
            var introduction = _context.Introductions.AsQueryable();
            return View(introduction);
        }
        public IActionResult AboutTeam()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
