using BOOKSTORE.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BOOKSTORE.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly QlbhContext _context;
        public CategoriesController(QlbhContext context)
        {
            _context = context;
        }
        //public async Task<IActionResult> Index(string Name="")
        //{
        //    Category category = _context.Categories.Where(c => c.Name == Name).FirstOrDefault();
        //    if (category == null) return RedirectToAction("Index");
        //    var productsByCategory = _context.Products.Where(p => p.CategoryId ==  category.Id);
        //    //return View(await productsByCategory.OrderByDescending(p => p.Id).ToListAsync;
        //}
    }
}
