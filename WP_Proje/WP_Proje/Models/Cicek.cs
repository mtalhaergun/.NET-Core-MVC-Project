using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WP_Proje.Models
{
    public class Cicek
    {
        public int CicekId { get; set; }
        public string Isim { get; set; }
        public string Bilgi { get; set; }
        public int Fiyat { get; set; }
        public int Stok { get; set; }
        public string Resim { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
        public int KategoriId { get; set; }
        public virtual Kategori Kategori { get; set; }

    }
}
