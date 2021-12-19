using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Proje.Models
{
    public class Kullanici
    {
        [Key]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "E-Posta girmelisiniz!"), StringLength(40, MinimumLength = 7, ErrorMessage = "E-Posta 7 ila 40 karakter arasında olmalıdır!"), DataType(DataType.EmailAddress)]
        public string Eposta { get; set; }

        [Required(ErrorMessage = "Şifre girmelisiniz!"), StringLength(20, MinimumLength = 5, ErrorMessage = "Şifre 5 ila 20 karakter arasında olmalıdır!"), DataType(DataType.Password)]
        public string Sifre { get; set; }

        [NotMapped, Display(Name="Şifreyi tekrar giriniz."),  DataType(DataType.Password), Compare("Sifre", ErrorMessage="Şifreler uyuşmuyor!")]
        public string SifreTekrari { get; set; }

        [Required(ErrorMessage="Rol gerekli!"), Display(Name = "Rol")]
        public int RolId { get; set; }

        public Rol Rol { get; set; }
    }
}
