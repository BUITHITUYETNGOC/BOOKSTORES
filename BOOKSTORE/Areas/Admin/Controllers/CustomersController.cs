using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOOKSTORE.Data;

namespace BOOKSTORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomersController : Controller
    {
        private readonly QlbhContext _context;

        public CustomersController(QlbhContext context)
        {
            _context = context;
        }

        [Route("Admin/Customers/")]
        [Route("Admin/Customers/index")]
        // GET: Admin/Customers
        public async Task<IActionResult> Index()
        {
            var qlCustomers = _context.Customers
                                      .Include(c => c.AccountNavigation)
                                      .Include(c => c.Histories)
                                      .Include(c => c.Orders);
            return View(await qlCustomers.ToListAsync());
        }

        // GET: Admin/Customers/Details/5
        [Route("Admin/Customers/Details")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                                         .Include(c => c.AccountNavigation)
                                         .Include(c => c.Histories)
                                         .Include(c => c.Orders)
                                         .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Admin/Customers/Create
        [Route("Admin/Customers/Create")]
        public IActionResult Create()
        {
            ViewData["Account"] = new SelectList(_context.Accounts, "Id", "Name");
            return View();
        }

        // POST: Admin/Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Customers/Create")]
        public async Task<IActionResult> Create(Customer customer)
        {
            ViewData["Account"] = new SelectList(_context.Accounts, "Id", "Name", customer.Account);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["error"] = $"Có lỗi xảy ra: {ex.Message}";
                }
            }
            else
            {
                TempData["error"] = "Model có lỗi";
            }

            return View(customer);
        }

        // GET: Admin/Customers/Edit/5
        [Route("Admin/Customers/Edit/5")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            ViewData["Account"] = new SelectList(_context.Accounts, "Id", "Name", customer.Account);
            return View(customer);
        }

        // POST: Admin/Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Customers/Edit/5")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Password,Image,Gender,BirthDay,Cccd,Address,Email,Phone,StartDay,UpdateLast,Account,Role,Bill,Status")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Account"] = new SelectList(_context.Accounts, "Id", "Name", customer.Account);
            return View(customer);
        }

        // GET: Admin/Customers/Delete/5
        [Route("Admin/Customers/Delete/5")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                                         .Include(c => c.AccountNavigation)
                                         .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/Customers/Delete/5")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(string id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
