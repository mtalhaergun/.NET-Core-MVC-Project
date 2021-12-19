using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Proje.Models;

namespace WebApp_Proje.Data
{
    public class WebApp_ProjeDbContext : DbContext
    {
        public WebApp_ProjeDbContext(DbContextOptions<WebApp_ProjeDbContext> options): base(options)
        {

        }

        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Rol> Roller { get; set; }
        public DbSet<Urunler> Urunler { get; set; }

       
    }
}
