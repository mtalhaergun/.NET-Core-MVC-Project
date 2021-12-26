using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private object webHostEnvironment;

        public CicekController(SiteDbContext context, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            this._hostEnvironment = hostEnvironment;
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
             var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.sepetId = _context.Sepet.Where(x => x.KullaniciId == userId).Select(y => y.SepetId).FirstOrDefault();
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
        public async Task<IActionResult> Create([Bind("CicekId,Isim,Bilgi,Fiyat,Stok,Resim,KategoriId,ImageFile")] Cicek cicek)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(cicek.ImageFile.FileName);
                string extension = Path.GetExtension(cicek.ImageFile.FileName);
                cicek.Resim = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/images", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await cicek.ImageFile.CopyToAsync(fileStream);
                }

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
