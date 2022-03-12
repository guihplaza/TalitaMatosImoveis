using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
     public class AlienDB_Regiao
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Id_Empresa")]
        public int Id_Empresa { get; set; }


        [Required]
        [Display(Name = "Descrição")]
        [MaxLength(400, ErrorMessage = "Máximo {400} caracteres permitidos")]
        public string Descricao { get; set; }


    }
}
