using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WP_Proje.Data;
using WP_Proje.Models;

namespace WP_Proje.Controllers
{
    [Authorize]
    public class SepetController : Controller
    {
        private readonly SiteDbContext _context;

        public SepetController(SiteDbContext context)
        {
            _context = context;
        }

        // GET: Sepet
        public async Task<IActionResult> Index()
        {
            var siteDbContext = _context.Sepet.Include(s => s.Kullanici);
            return View(await siteDbContext.ToListAsync());
        }

        // GET: Sepet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepet = await _context.Sepet
                .Include(s => s.Kullanici)
                .FirstOrDefaultAsync(m => m.SepetId == id);
            if (sepet == null)
            {
                return NotFound();
            }

            return View(sepet);
        }

        // GET: Sepet/Create
        public IActionResult Create()
        {
            ViewData["KullaniciId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Sepet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SepetId,SepetFiyat,KullaniciId")] Sepet sepet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sepet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KullaniciId"] = new SelectList(_context.Users, "Id", "Id", sepet.KullaniciId);
            return View(sepet);
        }

        // GET: Sepet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepet = await _context.Sepet.FindAsync(id);
            if (sepet == null)
            {
                return NotFound();
            }
            ViewData["KullaniciId"] = new SelectList(_context.Users, "Id", "Id", sepet.KullaniciId);
            return View(sepet);
        }

        // POST: Sepet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SepetId,SepetFiyat,KullaniciId")] Sepet sepet)
        {
            if (id != sepet.SepetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sepet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SepetExists(sepet.SepetId))
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
            ViewData["KullaniciId"] = new SelectList(_context.Users, "Id", "Id", sepet.KullaniciId);
            return View(sepet);
        }

        // GET: Sepet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepet = await _context.Sepet
                .Include(s => s.Kullanici)
                .FirstOrDefaultAsync(m => m.SepetId == id);
            if (sepet == null)
            {
                return NotFound();
            }

            return View(sepet);
        }

        // POST: Sepet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sepet = await _context.Sepet.FindAsync(id);
            _context.Sepet.Remove(sepet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SepetExists(int id)
        {
            return _context.Sepet.Any(e => e.SepetId == id);
        }
    }
}
