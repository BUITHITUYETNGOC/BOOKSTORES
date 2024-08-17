using BOOKSTORE.Data;
using Microsoft.AspNetCore.Mvc;

namespace BOOKSTORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminHomeController : Controller
    {
       
        private readonly QlbhContext _context;

        public AdminHomeController(QlbhContext context)
        {
            _context = context;
        }
        [Route("Admin/")]
        [Route("Admin/AdminHome")]
        [Route("Admin/index")]
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
