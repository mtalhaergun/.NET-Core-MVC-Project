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
    [Authorize(Roles="Admin")]
    public class CicekController : Controller
    {
        private readonly SiteDbContext _context;

        public CicekController(SiteDbContext context)
        {
            _context = context;
        }

        // GET: Cicek
        public async Task<IActionResult> Index()
        {
            var siteDbContext = _context.Cicekler.Include(c => c.Kategori);
            return View(await siteDbContext.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Urunler()
        {
            var siteDbContext = _context.Cicekler.Include(c => c.Kategori);
            return View(await siteDbContext.ToListAsync());
        }

        // GET: Cicek/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cicek = await _context.Cicekler
                .Include(c => c.Kategori)
                .FirstOrDefaultAsync(m => m.CicekId == id);
            if (cicek == null)
            {
                return NotFound();
            }

            return View(cicek);
        }

        // GET: Cicek/Create
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriId");
            return View();
        }

        // POST: Cicek/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CicekId,Isim,Bilgi,Fiyat,Stok,Resim,KategoriId")] Cicek cicek)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cicek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriId", cicek.KategoriId);
            return View(cicek);
        }

        // GET: Cicek/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cicek = await _context.Cicekler.FindAsync(id);
            if (cicek == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriId", cicek.KategoriId);
            return View(cicek);
        }

        // POST: Cicek/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CicekId,Isim,Bilgi,Fiyat,Stok,Resim,KategoriId")] Cicek cicek)
        {
            if (id != cicek.CicekId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cicek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CicekExists(cicek.CicekId))
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
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriId", cicek.KategoriId);
            return View(cicek);
        }

        // GET: Cicek/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cicek = await _context.Cicekler
                .Include(c => c.Kategori)
                .FirstOrDefaultAsync(m => m.CicekId == id);
            if (cicek == null)
            {
                return NotFound();
            }

            return View(cicek);
        }

        // POST: Cicek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cicek = await _context.Cicekler.FindAsync(id);
            _context.Cicekler.Remove(cicek);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CicekExists(int id)
        {
            return _context.Cicekler.Any(e => e.CicekId == id);
        }
    }
}
