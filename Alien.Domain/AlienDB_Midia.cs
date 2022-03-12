using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AlienDB_Midia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Id_Empresa")]
        public int Id_Empresa { get; set; }
                
        public int Id_Imovel { get; set; }

        [Required]
        [ForeignKey("Id_Imovel")]
        public virtual AlienDB_Imovel Imovel { get; set; }

        [Required]
        [Display(Name = "Nome imagem")]
        [MaxLength(100, ErrorMessage = "Máximo {1} caracteres permitidos")]
        public string Nome { get; set; }


        //[Display(Name = "Destaque")]
        //[MaxLength(1, ErrorMessage = "Máximo {1} caracteres permitidos")]
        //public string Flg_destaque { get; set; }

        [Display(Name = "Caminho")]
        [MaxLength(300, ErrorMessage = "Máximo {300} caracteres permitidos")]
        public string Caminho { get; set; }


        [Display(Name = "ImagemByte")]
        [MaxLength(16), Column(TypeName = "Binary")]
        public byte[] ImagemByte { get; set; }


        [Required]
        [Display(Name = "Extensão da imagem")]
        [MaxLength(100, ErrorMessage = "Máximo {4} caracteres")]
        public string ContentType { get; set; }

        //[Required]
        //[Display(Name = "Data de inclusão")]
        //[DefaultValue("DateTime.Now")]
        //public DateTime Dat_Inclui { get; set; }

        //[Display(Name = "Data fim")]
        //public DateTime? Data_fim { get; set; }
        [Display(Name = "Imagem principal")]
        [DefaultValue("false")]
        public Boolean ImagemPrincipal { get; set; }

        [NotMapped]
        public string filtroRegiao { get; set; }

        [NotMapped]
        public string filtroTipo { get; set; }

        [NotMapped]
        public string filtroFormato { get; set; }

        [NotMapped]
        public int? pagina { get; set; }
    }
}
