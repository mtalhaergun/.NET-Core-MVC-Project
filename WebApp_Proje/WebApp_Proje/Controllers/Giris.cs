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
using WebApp_Proje.Models;

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

        public async Task<IActionResult> Login(Kullanici k)
        {
            var bilgiler = _context.Kullanicilar.FirstOrDefault(x => x.Eposta == k.Eposta && x.Sifre == k.Sifre);
            if(bilgiler != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, k.Eposta)
                };

                var useridentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            var giriş = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/Giriş");
        }
    }
}
