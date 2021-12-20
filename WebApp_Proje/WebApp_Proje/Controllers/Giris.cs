using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp_Proje.Data;

namespace WebApp_Proje.Controllers
{
    public class Giris : Controller
    {

        private readonly WebApp_ProjeDbContext _context;

        public Giris(WebApp_ProjeDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Eposta, string Sifre)
        {
            ClaimsIdentity kimlik = null;
            bool kimlikDoğrulandıMı = false;
            var kullanıcı = await _context.Kullanicilar.Include(k => k.Rol).FirstOrDefaultAsync(m => m.Eposta == Eposta && m.Sifre == Sifre);
            if (kullanıcı == null)
            {
                return NotFound();
            }

            kimlik = new ClaimsIdentity
                (new[]
                        {
                            new Claim(ClaimTypes.Sid,kullanıcı.KullaniciId.ToString()),
                            new Claim(ClaimTypes.Email,kullanıcı.Eposta),
                            new Claim(ClaimTypes.Role,kullanıcı.Rol.RolAdi)
                        }, CookieAuthenticationDefaults.AuthenticationScheme
                );
            kimlikDoğrulandıMı = true;

            if (kimlikDoğrulandıMı)
            {
                var ilkeler = new ClaimsPrincipal(kimlik);
                var giriş = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, ilkeler);
                return Redirect("~/Home");
            }
            return View();

        }
        [HttpGet]
        public IActionResult Logout()
        {
            var giriş = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/Giris");
        }
    }
}
