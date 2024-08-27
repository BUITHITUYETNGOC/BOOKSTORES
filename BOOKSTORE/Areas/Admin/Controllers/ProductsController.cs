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
    public class ProductsController : Controller
    {
        private readonly QlbhContext _context;

        public ProductsController(QlbhContext context)
        {
            _context = context;
        }
        [Route("Admin/Products/")]
        [Route("Admin/Products/index")]
        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var qlProducts = _context.Products.Include(p => p.Category).Include(p => p.Supplier);
            return View(await qlProducts.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        [Route("Admin/Products/Details/5")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        [Route("Admin/Products/Create")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Products/Create")]
        public async Task<IActionResult> Create(Product product)
        {
            // Load lại danh sách Category và Supplier để hiển thị trong view nếu cần nhập lại thông tin
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", product.SupplierId);

            // Kiểm tra tính hợp lệ của Model
            if (ModelState.IsValid && product.Sl > 0 && product.UnitPrice > 1000)
            {
                ModelState.AddModelError("Sl", "Số lượng phải lớn hơn 0");
                ModelState.AddModelError("UnitPrice", "Giá tiền không được nhỏ hơn 1000");
                // Kiểm tra sản phẩm đã tồn tại trong database chưa
                var Slug = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

                // Nếu sản phẩm đã tồn tại, thông báo lỗi
                if (Slug != null)
                {
                    ModelState.AddModelError("", "Sản phẩm đã tồn tại");
                    return View(product);
                }
                else
                {
                    // Nếu sản phẩm chưa tồn tại, tiếp tục xử lý
                    try
                    {
                        // Cập nhật ngày hiện tại lưu vào db
                        product.UpdateLast = DateTime.Now;
                        // Thêm sản phẩm mới vào database
                        _context.Add(product);
                        await _context.SaveChangesAsync();

                        // Chuyển hướng đến trang index hoặc trang quản lý sản phẩm sau khi thêm thành công
                        return RedirectToAction("Index", "Products");
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
            return View(product);
        }


        // GET: Admin/Products/Edit/5
        [Route("Admin/Products/Edit/5")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Products/Edit/5")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,UnitPrice,CategoryId,SupplierId,Sl,Description,Author,Status,UpdateLast,Image")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        [Route("Admin/Products/Delete/5")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/Products/Delete/5")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
