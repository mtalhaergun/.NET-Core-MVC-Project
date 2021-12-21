using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Proje.Data;
using WebApp_Proje.Models;

namespace WebApp_Proje.Controllers
{
    public class KayitOl : Controller
    {
        private readonly WebApp_ProjeDbContext _context;

        public KayitOl(WebApp_ProjeDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("KullaniciId, Eposta, Sifre, SifreTekrari, RolId")] Kullanici kullanıcı)
        {
            if (ModelState.IsValid)
            {
                kullanıcı.RolId = 1;
                _context.Add(kullanıcı);
                await _context.SaveChangesAsync();
                return Redirect("Giris");
            }
            return View(kullanıcı);
        }
    }
}
