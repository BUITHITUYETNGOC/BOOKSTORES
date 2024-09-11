using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOOKSTORE.Data;
using Microsoft.Extensions.Hosting.Internal;

namespace BOOKSTORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IntroductionsController : Controller
    {
        private readonly QlbhContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public IntroductionsController(QlbhContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Admin/Introductions
        [Route("Admin/Introductions/")]
        [Route("Admin/Introductions/index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Introductions.ToListAsync());
        }

        // GET: Admin/Introductions/Details/5
        [Route("Admin/Introductions/Details/5")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var introduction = await _context.Introductions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (introduction == null)
            {
                return NotFound();
            }

            return View(introduction);
        }

        // GET: Admin/Introductions/Create
        [Route("Admin/Introductions/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Introductions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Introductions/Create")]
        public async Task<IActionResult> Create([Bind("Id,FirstImage,LeftImage,Description,Address,Phone,UpdateLast")] Introduction introduction, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                introduction.UpdateLast = DateTime.Now;

                _context.Add(introduction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (Image != null && Image.Length > 0)
            {
                // Kiểm tra định dạng file (ví dụ: chỉ cho phép jpg, png)
                if (Image.ContentType.Contains("image/"))
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    var filePath = Path.Combine(uploads, uniqueFileName);


                    // Lưu file lên server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(fileStream);
                    }

                    // Lưu đường dẫn vào thuộc tính ImagePath của product
                    introduction.FirstImage = "/images/" + uniqueFileName;
                    introduction.LeftImage = "/images/" + uniqueFileName;
                }
                else
                {
                    ModelState.AddModelError("Image", "Chỉ chấp nhận file ảnh.");
                }
            }
            return View(introduction);
        }

        // GET: Admin/Introductions/Edit/5
        [Route("Admin/Introductions/Edit/5")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var introduction = await _context.Introductions.FindAsync(id);
            if (introduction == null)
            {
                return NotFound();
            }
            return View(introduction);
        }

        // POST: Admin/Introductions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Introductions/Edit/5")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstImage,LeftImage,Description,Address,Phone,UpdateLast")] Introduction introduction)
        {
            if (id != introduction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(introduction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntroductionExists(introduction.Id))
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
            return View(introduction);
        }

        // GET: Admin/Introductions/Delete/5
        [Route("Admin/Introductions/Delete/5")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var introduction = await _context.Introductions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (introduction == null)
            {
                return NotFound();
            }

            return View(introduction);
        }

        // POST: Admin/Introductions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/Introductions/Delete/5")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var introduction = await _context.Introductions.FindAsync(id);
            if (introduction != null)
            {
                _context.Introductions.Remove(introduction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntroductionExists(string id)
        {
            return _context.Introductions.Any(e => e.Id == id);
        }
    }
}
