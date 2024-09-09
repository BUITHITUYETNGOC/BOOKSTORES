using BOOKSTORE.Data;
using BOOKSTORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

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
            var cart = GetCartItems();
            if (cart.Any())
            {
                // Generate new CartId
                string newCartId = GenerateCartId();

                // Create a new Order
                var order = new Order
                {
                    CartId = newCartId,
                    Quantity = cart.Sum(c => c.Quantity),
                    UnitPrice = cart.Sum(c => c.UnitPrice * c.Quantity),
                    CustomerId = "some_customer_id",  // Replace with actual customer ID
                    // Ensure OrderDetail and Cart are properly set if needed
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

        [HttpPost]
        public IActionResult ConfirmCheckout(string cartId)
        {
            try
            {
                var cart = GetCartItems();
                var order = new Order
                {
                    CartId = cartId,
                    Quantity = cart.Sum(c => c.Quantity),
                    UnitPrice = cart.Sum(c => c.UnitPrice * c.Quantity),
                    CustomerId = "some_customer_id"  // Replace with actual customer ID
                };

                _context.Orders.Add(order);
                _context.SaveChanges();

                var orderDetails = new List<OrderDetail>();
                foreach (var cartItem in cart)
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity
                    };

                    orderDetails.Add(orderDetail);
                }

                _context.OrderDetails.AddRange(orderDetails);
                _context.SaveChanges();
                ClearCart();

                return Ok();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                return BadRequest("Có lỗi xảy ra khi xác nhận thanh toán.");
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

        private string GenerateCartId()
        {
            var lastOrder = _context.Orders.OrderByDescending(o => o.OrderId).FirstOrDefault();
            if (lastOrder != null)
            {
                int lastId = int.Parse(lastOrder.CartId.Substring(2));
                return "HD" + (lastId + 1).ToString("D3");
            }
            return "HD001";
        }

        public IActionResult Delete(string productId)
        {
            var cart = GetCartItems();
            var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
                SaveCartSession(cart);
            }

            // Redirect to the Index page after deletion
            return RedirectToAction("Index");
        }
    }
}
