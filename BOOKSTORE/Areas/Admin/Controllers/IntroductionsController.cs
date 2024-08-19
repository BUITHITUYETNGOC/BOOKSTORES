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
    public class IntroductionsController : Controller
    {
        private readonly QlbhContext _context;

        public IntroductionsController(QlbhContext context)
        {
            _context = context;
        }

        // GET: Admin/Introductions
        [Route("Admin/Introductions/")]
        [Route("Admin/Introductions/index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Introductions.ToListAsync());
        }

        // GET: Admin/Introductions/Details/5
        [Route("Admin/Introductions/Details")]
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
        public async Task<IActionResult> Create([Bind("Id,FirstImage,LeftImage,Description,Address,Phone,UpdateLast")] Introduction introduction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(introduction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(introduction);
        }

        // GET: Admin/Introductions/Edit/5
        [Route("Admin/Introductions/Edit")]
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
        [Route("Admin/Introductions/Edit")]
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
        [Route("Admin/Introductions/Delete")]
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
        [Route("Admin/Introductions/Delete")]
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
