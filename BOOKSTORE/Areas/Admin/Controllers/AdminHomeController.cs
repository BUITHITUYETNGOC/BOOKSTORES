using Microsoft.AspNetCore.Mvc;

namespace BOOKSTORE.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
