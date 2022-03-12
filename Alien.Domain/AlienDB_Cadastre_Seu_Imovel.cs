using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class AlienDB_Cadastre_Seu_Imovel
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


        [Display(Name = "Endereço")]
        [MaxLength(400, ErrorMessage = "Máximo {1} caracteres permitidos")]
        public string Endereco { get; set; }


        [Display(Name = "Bairro")]
        [MaxLength(400, ErrorMessage = "Máximo {1} caracteres permitidos")]
        public string Bairro { get; set; }


        [Display(Name = "Cidade")]
        [MaxLength(400, ErrorMessage = "Máximo {1} caracteres permitidos")]
        public string Cidade { get; set; }

        [Required]
        [Display(Name = "Valor Aluguel")]
        [Column(TypeName = "decimal(18, 2)")]
        public Decimal? Valor_aluguel { get; set; }

        [Display(Name = "Telefone Contato")]
        [MaxLength(20, ErrorMessage = "Máximo {1} caracteres permitidos")]
        public string Tel_contato { get; set; }


        [Display(Name = "Nome Proprietário")]
        [MaxLength(200, ErrorMessage = "Máximo {1} caracteres permitidos")]
        public string Nome_proprietario { get; set; }

        public int Id_Regiao { get; set; }

        [Required]
        [ForeignKey("Id_Regiao")]
        public virtual AlienDB_Regiao Regiao { get; set; }

    }
}
