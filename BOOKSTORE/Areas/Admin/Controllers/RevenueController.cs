using BOOKSTORE.Data;
using BOOKSTORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BOOKSTORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RevenueController : Controller
    {
        private readonly QlbhContext _context;

        public RevenueController(QlbhContext context)
        {
            _context = context;
        }
        [Route("Admin/Revenue/")]
        [Route("Admin/Revenue/Index")]
        // GET: Revenue
        public async Task<IActionResult> Index()
        {
            var revenues = await _context.Orders
                .Select(o => new Revenue
                {
                    OrderId = o.CartId,
                    Total = (double)(o.Quantity * o.UnitPrice),  // Sử dụng o.UnitPrice thay vì o.OrderDetail.UnitPrice
                })
                .ToListAsync();

            return View(revenues);
        }
        [Route("Admin/Revenue/Details/5")]
        // GET: Revenue/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderDetail)  // Nếu bạn cần thông tin chi tiết, bao gồm OrderDetail
                .FirstOrDefaultAsync(o => o.CartId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Admin/Revenue/Delete/5
        [Route("Admin/Revenue/Delete/5")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderDetail)
                .FirstOrDefaultAsync(m => m.CartId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/Revenue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/Revenue/Delete/5")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetail) // Ensuring order details are included for deletion
                .FirstOrDefaultAsync(o => o.CartId == id);

            if (order != null)
            {
                // Remove the related order details before removing the order
                _context.OrderDetails.RemoveRange(order.OrderDetail);
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(string id)
        {
            return _context.Orders.Any(e => e.CartId == id);
        }
    }
}
