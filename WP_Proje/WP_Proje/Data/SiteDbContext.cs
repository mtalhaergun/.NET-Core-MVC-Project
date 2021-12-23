using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP_Proje.Models;

namespace WP_Proje.Data
{
    public class SiteDbContext : IdentityDbContext<ApplicationUser>
    {

        public SiteDbContext(DbContextOptions<SiteDbContext> options) : base(options)
        {

        }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Cicek> Cicekler { get; set; }
    }
}
