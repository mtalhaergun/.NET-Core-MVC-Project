using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WP_Proje.Models
{
    public class SepetDetay
    {
        public int SepetDetayId { get; set; }

        [ForeignKey("SepetId")]
        public int SepetId { get; set; }
        public virtual Sepet Sepet { get; set; }

        [ForeignKey("UrunNo")]
        public int UrunNo { get; set; }
        public virtual Cicek Cicek { get; set; }

        public string Isim { get; set; }
        public string Bilgi { get; set; }
        public int Fiyat { get; set; }
        public int Stok { get; set; }
        public string Resim { get; set; }
    }
}
