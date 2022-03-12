using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class AlienDB_Empreendimentos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Id_Empresa")]
        public int Id_Empresa { get; set; }

        public int Id_Tipo_Imovel { get; set; }

        [Required]
        [ForeignKey("Id_Tipo_Imovel")]
        public virtual AlienDB_Tipo_Imovel TipoImovel { get; set; }


        [Display(Name = "Nome Lançamento")]
        [MaxLength(200, ErrorMessage = "Máximo {1} caracteres permitidos")]
        public string NomeLancamento { get; set; }

        [Display(Name = "Cidade")]
        [MaxLength(200, ErrorMessage = "Máximo {1} caracteres permitidos")]
        public string Cidade { get; set; }

        [Display(Name = "Construtora")]
        [MaxLength(200, ErrorMessage = "Máximo {1} caracteres permitidos")]
        public string Construtura { get; set; }

        public int Id_Regiao { get; set; }

        [Required]
        [ForeignKey("Id_Regiao")]
        public virtual AlienDB_Regiao Regiao { get; set; }
    }
}
