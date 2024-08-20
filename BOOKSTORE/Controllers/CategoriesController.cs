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
        public async Task<IActionResult> Index(string Name = "")
        {
           
            return View();
        }
    }
}
