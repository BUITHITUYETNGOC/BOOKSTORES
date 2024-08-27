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
    public class SuppliersController : Controller
    {
        private readonly QlbhContext _context;

        public SuppliersController(QlbhContext context)
        {
            _context = context;
        }

        // GET: Admin/Suppliers
        [Route("Admin/Suppliers")]
        [Route("Admin/Suppliers/Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Suppliers.ToListAsync());
        }

        // GET: Admin/Suppliers/Details/5
        [Route("Admin/Suppliers/Details/5")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Admin/Suppliers/Create
        [Route("Admin/Suppliers/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Suppliers/Create")]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra nhà cung cấp đã tồn tại trong database chưa
                var Slug = await _context.Products.FirstOrDefaultAsync(p => p.Id == supplier.Id);
                if (Slug != null)
                {
                    ModelState.AddModelError("", "Nhà cung cấp đã tồn tại");
                    return View(supplier);
                }
                else
                {
                    // Nếu sản phẩm chưa tồn tại, tiếp tục xử lý
                    try
                    {
                        
                        // Thêm sản phẩm mới vào database
                        _context.Add(supplier);
                        await _context.SaveChangesAsync();

                        // Chuyển hướng đến trang index hoặc trang quản lý sản phẩm sau khi thêm thành công
                        return RedirectToAction("Index", "Suppliers");
                    }
                    catch (Exception ex)
                    {
                        // Nếu có lỗi trong quá trình thêm dữ liệu, lưu thông tin lỗi trong TempData và trả về view
                        TempData["error"] = $"Có lỗi xảy ra: {ex.Message}";
                    }
                }
            }
            else
            {
                TempData["error"] = "Model có lỗi";
            }

            // Nếu model không hợp lệ hoặc có lỗi, trả về view để người dùng sửa
            return View(supplier);
        }

        // GET: Admin/Suppliers/Edit/5
        [Route("Admin/Suppliers/Edit/5")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Suppliers/Edit/5")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Email,Phone")] Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.Id))
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
            return View(supplier);
        }

        // GET: Admin/Suppliers/Delete/5
        [Route("Admin/Suppliers/Delete/5")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Admin/Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/Suppliers/Delete/5")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(string id)
        {
            return _context.Suppliers.Any(e => e.Id == id);
        }
    }
}
