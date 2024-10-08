﻿using System;
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
    public class CategoriesController : Controller
    {
        private readonly QlbhContext _context;

        public CategoriesController(QlbhContext context)
        {
            _context = context;
        }

        // GET: Admin/Categories
        [Route("Admin/Categories")]
        [Route("Admin/Categories/Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Admin/Categories/Details/5
        [Route("Admin/Categories/Details/5")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Categories/Create
        [Route("Admin/Categories/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Categories/Create")]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                var Slug = await _context.Categories.FirstOrDefaultAsync(p => p.Id == category.Id);

                // Nếu sản phẩm đã tồn tại, thông báo lỗi
                if (Slug != null)
                {
                    ModelState.AddModelError("", "Thể loại đã tồn tại");
                    return View(category);
                }
                else
                {
                    // Nếu sản phẩm chưa tồn tại, tiếp tục xử lý
                    try
                    {
                        
                        // Thêm sản phẩm mới vào database
                        _context.Add(category);
                        await _context.SaveChangesAsync();

                        // Chuyển hướng đến trang index hoặc trang quản lý sản phẩm sau khi thêm thành công
                        return RedirectToAction("Index", "Categories");
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
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        [Route("Admin/Categories/Edit/5")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Categories/Edit/5")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        [Route("Admin/Categories/Delete/5")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete/5")]
        [ValidateAntiForgeryToken]
        [Route("Admin/Categories/Delete/5")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(string id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
