using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WP_Proje.Models
{
    public class Kategori
    {
        public int KategoriId { get; set; }

        [Required(ErrorMessage = "Kategori adı boş olamaz!")]
        public string KategoriAdi { get; set; }
        public List<Cicek> Cicekler { get; set; }
    }
}
