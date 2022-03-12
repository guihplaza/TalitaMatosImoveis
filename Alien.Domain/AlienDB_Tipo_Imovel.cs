using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AlienDB_Tipo_Imovel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Id_Empresa")]
        public int Id_Empresa { get; set; }



        [Required]
        [Display(Name = "Descrição")]
        [MaxLength(400, ErrorMessage = "Máximo {1} caracteres permitidos")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Tipo do imóvel")]
        [MaxLength(1, ErrorMessage = "Máximo {1} caracteres permitidos")]
        public string FlgTipoImovel { get; set; }

    }
}
