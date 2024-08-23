using BOOKSTORE.Data;
using BOOKSTORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BOOKSTORE.Controllers
{
	public class CartController : Controller
	{
		
		 private readonly QlbhContext _context;

		public CartController(QlbhContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var cart = GetCartItems();
			return View(cart);
		}

        [HttpPost]
        public IActionResult AddToCart(string productId, int soluong)
        {
            var product = _context.Products.Find(productId);

            if (product == null)
            {
                return NotFound();
            }

            var cart = GetCartItems();

            var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);
            if (cartItem == null)
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    UnitPrice = product.UnitPrice ?? 0,
                    Quantity = soluong,
                    Image = product.Image
                });
            }
            else
            {
                cartItem.Quantity += soluong;
            }

            SaveCartSession(cart);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateCart(string productId, int quantity)
		{
			var cart = GetCartItems();
			var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);

			if (cartItem != null && quantity > 0)
			{
				cartItem.Quantity = quantity;
			}

			SaveCartSession(cart);
			return RedirectToAction("Index");
		}

        public IActionResult Checkout()
        {
            try
            {
                var cart = GetCartItems();
                if (cart.Any())
                {
                    // Create a new order
                    var order = new Order
                    {
                        CartId = Guid.NewGuid().ToString(),  // Ensure CartId is valid
                        Quantity = cart.Sum(c => c.Quantity),
                        UnitPrice = cart.Sum(c => c.Total),
                        CustomerId = "some_customer_id"  // Replace with actual customer ID
                    };

                    // Add the order to the database
                    _context.Orders.Add(order);
                    _context.SaveChanges();  // Save order first to get the OrderId

                    // Create and add OrderDetails
                    var orderDetails = new List<OrderDetail>();
                    foreach (var cartItem in cart)
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderId = order.OrderId,  // Use the OrderId generated after saving the order
                            ProductId = cartItem.ProductId,
                            Quantity = cartItem.Quantity
                        };

                        orderDetails.Add(orderDetail);
                    }

                    _context.OrderDetails.AddRange(orderDetails);
                    _context.SaveChanges();  // Save OrderDetails

                    ClearCart();
                    return RedirectToAction("OrderSuccess");
                }

                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                // Log the exception and provide a user-friendly message
                Console.WriteLine(ex.InnerException?.Message);
                return View("Error");  // Replace with your error view
            }
        }

        private List<CartItem> GetCartItems()
		{
			var session = HttpContext.Session.GetString("Cart");
			if (session != null)
			{
				return JsonConvert.DeserializeObject<List<CartItem>>(session);
			}
			return new List<CartItem>();
		}

		private void SaveCartSession(List<CartItem> cart)
		{
			var session = JsonConvert.SerializeObject(cart);
			HttpContext.Session.SetString("Cart", session);
		}

		private void ClearCart()
		{
			HttpContext.Session.Remove("Cart");
		}
	}
}
