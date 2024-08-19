using BOOKSTORE.Data;

namespace BOOKSTORE.Controllers
{
    internal class HomeViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}