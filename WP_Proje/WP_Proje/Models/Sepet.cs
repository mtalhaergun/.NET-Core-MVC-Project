using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WP_Proje.Models
{
    public class Sepet
    {
        public int SepetId { get; set; }
        public int SepetFiyat { get; set; }

        
        public string KullaniciId { get; set; }
        [ForeignKey("KullaniciId")]
        public virtual ApplicationUser Kullanici { get; set; }
    }
}
