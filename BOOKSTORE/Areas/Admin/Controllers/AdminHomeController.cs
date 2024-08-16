using Microsoft.AspNetCore.Mvc;

namespace BOOKSTORE.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
