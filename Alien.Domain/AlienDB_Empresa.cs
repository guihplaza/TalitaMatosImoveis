using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class AlienDB_Empresa
    {
        [Key]
        public int Id_Empresa { get; set; }

        [Required]
        public string Des_Empresa { get; set; }

        [Required]
        public string Flg_Ativo { get; set; }

        public string Des_Contato { get; set; }        
    }
}
