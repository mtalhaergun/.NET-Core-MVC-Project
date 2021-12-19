using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Proje.Models
{
    public class Rol
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RolId { get; set; }

        [Required, StringLength(30, MinimumLength = 1, ErrorMessage ="1 ila 30 karakterlik bir rol girmelisiniz."), Display(Name ="Rol Adı")]
        public string RolAdi { get; set; }
    }
}
