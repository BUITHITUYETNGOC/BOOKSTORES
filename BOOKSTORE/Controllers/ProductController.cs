using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOOKSTORE.Data;

namespace BOOKSTORE.Controllers
{
    public class ProductController : Controller
    {
        private readonly QlbhContext _context;

        public ProductController(QlbhContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var qlbhContext = _context.Products.Include(p => p.Category).Include(p => p.Supplier);
            return View(await qlbhContext.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id); // Sử dụng Id lấy details

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

    }
}
