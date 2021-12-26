using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WP_Proje.Data;
using WP_Proje.Models;

namespace WP_Proje.Controllers
{
    public class SepetDetayController : Controller
    {
        private readonly SiteDbContext _context;

        public SepetDetayController(SiteDbContext context)
        {
            _context = context;
        }

        // GET: SepetDetay
        public async Task<IActionResult> Index(int urunId,int sepetId)
        {
            var sepetdetay = new SepetDetay { SepetId = sepetId, UrunNo = urunId };
            _context.SepetDetay.Add(sepetdetay);
            _context.SaveChanges();
             var urunler  =  _context.SepetDetay.Where(x=>x.SepetId== sepetId).Select(y => y.UrunNo).ToList();
            List<string> list = new();
            foreach (var item in urunler)
            {
                var urunAd = _context.Cicekler.Where(x => x.CicekId == item).Select(y => y.Isim).FirstOrDefault();
                list.Add(urunAd);
            }
            ViewBag.urunad = list; 
            var Sepet = await _context.SepetDetay.Where(x => x.SepetId == sepetId).ToListAsync();
            return View(Sepet);
        }

        // GET: SepetDetay/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepetDetay = await _context.SepetDetay
                .FirstOrDefaultAsync(m => m.SepetDetayId == id);
            if (sepetDetay == null)
            {
                return NotFound();
            }

            return View(sepetDetay);
        }

        // GET: SepetDetay/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SepetDetay/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SepetDetayId,SiparisNo,UrunNo")] SepetDetay sepetDetay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sepetDetay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sepetDetay);
        }

        // GET: SepetDetay/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepetDetay = await _context.SepetDetay.FindAsync(id);
            if (sepetDetay == null)
            {
                return NotFound();
            }
            return View(sepetDetay);
        }

        // POST: SepetDetay/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SepetDetayId,SiparisNo,UrunNo")] SepetDetay sepetDetay)
        {
            if (id != sepetDetay.SepetDetayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sepetDetay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SepetDetayExists(sepetDetay.SepetDetayId))
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
            return View(sepetDetay);
        }

        // GET: SepetDetay/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepetDetay = await _context.SepetDetay
                .FirstOrDefaultAsync(m => m.SepetDetayId == id);
            if (sepetDetay == null)
            {
                return NotFound();
            }

            return View(sepetDetay);
        }

        // POST: SepetDetay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sepetDetay = await _context.SepetDetay.FindAsync(id);
            _context.SepetDetay.Remove(sepetDetay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SepetDetayExists(int id)
        {
            return _context.SepetDetay.Any(e => e.SepetDetayId == id);
        }
    }
}
